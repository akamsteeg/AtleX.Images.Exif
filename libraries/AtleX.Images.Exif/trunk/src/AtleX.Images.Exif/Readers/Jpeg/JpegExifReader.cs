using AtleX.Images.Exif.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    /// <summary>
    /// Read EXIF data from JPEG images
    /// </summary>
    public class JpegExifReader : ExifReader
    {
        private bool _isLittleEndian;

        protected Stream ImageDataStream
        {
            get;
            set;
        }

        public JpegExifReader(Stream imageDataStream)
        {
            if (imageDataStream == null)
                throw new ArgumentNullException("imageDataStream");

            if (FileTypeHelper.DetermineFileType(imageDataStream) == ImageFileType.Jpeg)
            {
                this.ImageDataStream = imageDataStream;
                this.CanRead = true;
            }
            else
            {
                this.CanRead = false;
                throw new InvalidDataException(Strings.ExceptionImageInvalidJpeg);
            }
        }
        /// <summary>
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>
        /// A collection with the tags and the values read from the image
        /// </returns>
        /// <remarks>
        /// Every time this gets called the data is read again from the image.
        /// </remarks>
        public override IEnumerable<ExifValue> GetExifData()
        {
            if (!this.CanRead)
                throw new InvalidOperationException(Strings.ExceptionReaderCanNotRead);

            IEnumerable<ExifValue> values = null;

            /*
             * Reset the stream to the beginning to avoid exceptions
             * when reading data from the same reader twice.
             */
            this.ImageDataStream.Seek(0, SeekOrigin.Begin);

            /*
             * Use a BinaryReader to read from the Stream with image data
             * and dispose it when we're done, but leave the Stream open.
             *
             * Closing the Stream would cause problems when reading data
             * from the same image twice, because you can't read from a
             * closed/disposed Stream.
             *
             * The Stream gets closed and disposed when the reader is disposed
             * to clean-up all used resources.
             */
            using (BinaryReader bReader = new BinaryReader(this.ImageDataStream, new ASCIIEncoding(), true))
            {
                byte[] tiffData = this.GetRawIptcData(bReader);

                values = this.ParseExif(tiffData);
            }

            return values;
        }

        /// <summary>
        /// Parse the file header into segments
        /// </summary>
        /// <param name="reader">
        /// A <see cref="BinaryReader"/> object raw image data
        /// </param>
        /// <returns>
        /// A byte-array with the raw Exif/IPTC data when the image has this,
        /// null otherwise
        /// </returns>
        protected virtual byte[] GetRawIptcData(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            byte[] result = null;

            /*
             * Reset the stream because we want the full header to extract the App1
             * segment from the whole file.
             */
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            /*
             * When we have found and read the App1 segment we can stop looking for
             * more data. There's no point in reading the whole image when we only
             * need a tiny fraction of it.
             */
            bool app1Found = false;

            /*
             * The header with App segments ends when the DHT,
             * DQT, DRI or SOF starts. To quote from the Exif specs:
             *
             * "DQT, DHT, DRI and SOF may line up in any order, but
             * shall be recorded after APP1 (or APP2 if any) and
             * before SOS"
             *
             * So, we need to find the start of App1, and read until another segment
             * starts. The data between the App1 marker and another segment contains
             * the Exif/IPTC data.
             */
            while (reader.BaseStream.Position != reader.BaseStream.Length && !app1Found)
            {
                /*
                 * While this might seem a very inefficient method (no buffering, reading
                 * 2 bytes at a time), it's actually very fast when the reader reads from a FileStream
                 * because a FileStream is buffered by default (http://blogs.msdn.com/b/brada/archive/2004/04/15/114329.aspx)
                 *
                 * With something like a MemoryStream we benefit from reading from hardware
                 * that easily reaches multiple gigabytes per second. That might look like
                 * an easy way out ("loads of bandwidth, let's be lazy!") but I actually
                 * measured several solutions including in-library caching and reading larger
                 * chunks from the Stream. The current solution is a trade-off between
                 * memory usage (low, lower, lowest!) and speed (fast, faster, fastest!).
                 */
                byte[] markerBytes = reader.ReadBytes(2);

                if (markerBytes[0] == 255 // We propably arrived at a header
                    && markerBytes[1] == 225) // APP1
                {
                    app1Found = true;

                    long segmentStartPos = reader.BaseStream.Position;

                    // The length of the segment is specified after the segment
                    // marker in two bytes
                    byte[] segmentLengthSpecification = reader.ReadBytes(2);
                    int segmentLength = ByteConvertor.ConvertBytesToInt(segmentLengthSpecification);

                    if (segmentLength > 0) // We'll silently discard invalid or empty segments
                    {
                        // Read the data for the whole segment
                        byte[] segmentData = reader.ReadBytes(segmentLength);

                        /*
                         * APP1 starts with 4 bytes (0×45, 0×78, 0×69, 0×66) followed
                         * by two -bytes and then the TIFF header
                         *
                         * The TIFF header is 0×4949 (7373) for Intel (II, little endian)
                         * or 0x4D4D (7777) for Motorola (MM, big endian)
                         */
                        if ((segmentData[6] == 73 && segmentData[7] == 73) || // Intel
                            (segmentData[6] == 77 && segmentData[7] == 77)) // Motorola
                        {
                            this._isLittleEndian = (segmentData[6] == 73 && segmentData[7] == 73);

                            // Get IFD offset, it's 0x00 00 00 08 if the IFD is
                            // located directly after the TIFF header
                            int ifdOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(segmentData, 10, 4), this._isLittleEndian);

                            /*
                             * Why '6 + ifdOffset'? Because it's counting from the start of the TIFF header at
                             * positions 6 & 7 in the segment data.
                             */
                            byte[] tiffData = this.ReadBytes(segmentData, 6 + ifdOffset, segmentData.Length - ifdOffset - 6);

                            result = tiffData;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Read the TIFF (EXIF) values from the data
        /// </summary>
        /// <param name="app1Data">
        /// The raw contents of the App1 segment
        /// </param>
        /// <returns>
        /// A collection with the tags and the values read from the image
        /// </returns>
        protected virtual IEnumerable<ExifValue> ParseExif(byte[] app1Data)
        {
            /*
             * The two first bytes of the App1 segment are indicating
             * how many Exif/IPTC entries there are.
             */
            int numberOfEntries = ByteConvertor.ConvertBytesToInt(this.ReadBytes(app1Data, 0, 2), this._isLittleEndian);

            /*
             * Typically the number of Exif values will be the same or more
             * than the total number of Exif/IPTC entries in the segment. The
             * documentation(https://msdn.microsoft.com/en-us/library/4kf43ys3.aspx)
             * states that when a new list is instantiated with the parameterless
             * constructor the default capacity is 0.
             *
             * Because we'll almost always store data in the list there's no
             * point in starting with a zero-capacity list. Adding data would
             * case growth, and growth causes copying data from the internal
             * array of the list.
             */
            List<ExifValue> values = new List<ExifValue>(numberOfEntries);

            for (int i = 0; i < numberOfEntries; i++)
            {
                /*
                 * Tags are 12 bytes long. The first two indicate the tag, the next
                 * two the content type and the final four the data, or a pointer
                 * to the data
                 */
                // TODO: Why '2+...'?
                byte[] tag = this.ReadBytes(app1Data, 2 + (i * 12), 12);

                int tagType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 0, 2), this._isLittleEndian);
                int contentType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 2, 2), this._isLittleEndian);
                int count = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 4, 4), this._isLittleEndian);

                ExifFieldType currentTag = (ExifFieldType)tagType;
                byte[] data;

                switch (contentType)
                {
                    case 1: // Byte
                    case 7: // Undefined (1 byte)
                        {
                            data = this.ReadBytes(tag, 8, 4);

                            byte value = data[0];
                            values.Add(new ExifValue(currentTag, value));
                        }
                        break;

                    case 2: // ASCII
                        {
                            int dataOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 8, 4), this._isLittleEndian);
                            // TODO: Find out and document why the -8 has to happen?
                            data = this.ReadBytes(app1Data, dataOffset - 8, count);
                            string value = ByteConvertor.ConvertBytesToASCIIString(data);

                            values.Add(new ExifValue(currentTag, value));
                        }
                        break;

                    case 3: // Short (2 bytes, uint16)
                        {
                            data = this.ReadBytes(tag, 8, 2);

                            int value = ByteConvertor.ConvertBytesToInt(data, this._isLittleEndian);
                            values.Add(new ExifValue(currentTag, value));
                        }
                        break;

                    case 4: // Long (4 bytes, uint32)
                    case 9: // Slong (4 bytes, int32)
                        {
                            data = this.ReadBytes(tag, 8, 4);

                            int value = ByteConvertor.ConvertBytesToInt(data, this._isLittleEndian);
                            values.Add(new ExifValue(currentTag, value));
                        }
                        break;

                    case 5: // Rational (two Longs, first one is the nominator, second is the denominator)
                    case 10: // Srational (two slongs, first one is the nominator, second is the denominator)
                        {
                            byte[] numeratorPart = this.ReadBytes(tag, 4, 4);
                            byte[] denominatorPart = this.ReadBytes(tag, 8, 4);

                            int numerator = ByteConvertor.ConvertBytesToInt(numeratorPart, this._isLittleEndian);
                            int denominator = ByteConvertor.ConvertBytesToInt(denominatorPart, this._isLittleEndian);

                            int value = numerator / denominator;
                            values.Add(new ExifValue(currentTag, value));
                        }
                        break;
                }
            }

            return values;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ImageDataStream != null)
                {
                    this.ImageDataStream.Dispose();
                    this.ImageDataStream = null;
                }
            }
        }

        /// <summary>
        /// Read a specified number of bytes from a byte array
        /// </summary>
        /// <param name="source">
        /// The byte array to read from
        /// </param>
        /// <param name="start">
        /// The starting position in the source
        /// </param>
        /// <param name="length">
        /// The number of bytes to read
        /// </param>
        /// <returns>
        /// The read number of bytes
        /// </returns>
        /// <remarks>
        /// This can probably be done a lot more efficiently instead of copying
        /// values in a loop.
        /// </remarks>
        private byte[] ReadBytes(byte[] source, int start, int length)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException("start", Strings.ExceptionValueCanNotBeLessThanZero);
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", Strings.ExceptionValueCanNotBeLessThanZero);
            if (source.Length < start + length)
                throw new ArgumentOutOfRangeException("length", Strings.ExceptionSourceInvalidLength);

            byte[] result = new byte[length];

            int j = 0;
            for (int i = start; i < start + length; i++)
            {
                result[j] = source[i];
                j++;
            }

            return result;
        }
    }
}