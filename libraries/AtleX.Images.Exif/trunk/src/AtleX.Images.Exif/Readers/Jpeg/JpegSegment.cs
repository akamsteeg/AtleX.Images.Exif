using AtleX.Images.Exif.Data;
using AtleX.Images.Exif.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    internal enum JpegSegmentType
    {
        Unknown,
        Soi, // Start of Image  (FF D8 / 255 216)
        Jfif, // JFIF (FF E0 / 255 224)
        App1, // Application Segment 1 (FF E1 / 255 225)
        App2, // Application Segment 2 (FF E2 / 255 226)
        Dqt, // Define Quantization Table (FF DB / 255 219)
        Dht, // Define Huffman Table (FF C4 / 255 196)
        Dri, // Define Restart Interoperability (FF DD / 255 221)
        Sof, // Start of Frame (FF C0 / 255 192)
        Sos, // Start of Scan (FF C0 / 255 218)
        Eoi, // End of Image (FF D9 / 255 217)
    }

    [DebuggerDisplay("{Type} ({Data.Length} bytes)")]
    internal class RawJpegSegment
    {
        public JpegSegmentType Type { get; set; }
        public byte[] Data { get; set; }
    }

    internal abstract class JpegSegmentParser
    {
        protected bool _isLittleEndian; // TODO This doesn't work when we load Big-Endian files :/

        public abstract IEnumerable<ExifValue> Parse(RawJpegSegment segment);

        /// <summary>
        /// Read a specified number of bytes from a byte array
        /// </summary>
        /// <param name="source">The byte array to read from</param>
        /// <param name="start">The starting position in the source</param>
        /// <param name="length">The number of bytes to read</param>
        /// <returns>The read number of bytes</returns>
        protected byte[] ReadBytes(byte[] source, int start, int length)
        {
            if (source.Length < start + length)
                throw new ArgumentOutOfRangeException("length", Strings.ExceptionSourceInvalidLength);

            byte[] readBytes = new byte[length];
            int j = 0;
            for (int i = start; i < start + length; i++)
            {
                readBytes[j] = source[i];
                j++;
            }

            // We're working with little endiannes only
            if (!this._isLittleEndian)
                readBytes = readBytes.Reverse().ToArray();

            return readBytes;
        }       
    }

    internal class JpegSegmentParserApp1 : JpegSegmentParser
    {
        public override IEnumerable<ExifValue> Parse(RawJpegSegment segment)
        {
            IEnumerable<ExifValue> values = null;

            /* 
             * APP1 starts with 4 bytes (0×45, 0×78, 0×69, 0×66) followed
             * by two -bytes and then the TIFF header
             * 
             * The TIFF header is 0×4949 (7373) for Intel (II, little endian)
             * or 0x4D4D (7777) for Motorola (MM, big endian)
             */

            // Has TIFF header?
            if ((segment.Data[6] == 73 && segment.Data[7] == 73) || // Intel
                (segment.Data[6] == 77 && segment.Data[7] == 77)) // Motorola
            {
                this._isLittleEndian = (segment.Data[6] == 73 && segment.Data[7] == 73);
                // Get IFD offset, it's 0x00 00 00 08 if the IFD is located directly after the TIFF header
                int ifdOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(segment.Data, 10, 4));

                /* 
                 * Why '6 + ifdOffset'? Because it's counting from the start of the TIFF header at 
                 * positions 6 & 7
                 */
                byte[] tiffData = this.ReadBytes(segment.Data, 6 + ifdOffset, segment.Data.Length - ifdOffset - 6);
                
                values = this.ReadTiff(tiffData);
            }

            return values;
        }

        /// <summary>
        /// Read the TIFF (EXIF) values from the data
        /// </summary>
        /// <param name="tiffData"></param>
        /// <returns></returns>
        private IEnumerable<ExifValue> ReadTiff(byte[] tiffData)
        {
            List<ExifValue> values = new List<ExifValue>();
            int numberOfEntries = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tiffData, 0, 2));

            for (int i = 0; i < numberOfEntries; i++)
            {
                // TODO: Why '2+...'?
                // Segments are 12 bytes long
                byte[] tag = this.ReadBytes(tiffData, 2 + (i * 12) , 12);

                int tagType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 0, 2));
                int contentType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 2, 2));
                int count = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 4, 4));                

                ExifFieldType currentTag = (ExifFieldType)tagType;
                byte[] data;

                switch (contentType)
                {
                    case 1: // Byte
                        {
                            data = this.ReadBytes(tag, 8, 4);

                            byte value = data[0];
                            values.Add(new ExifByteValue(currentTag, value));
                        }
                        break;
                    case 2: // ASCII
                        {
                            int dataOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 8, 4));
                            // TODO: Find out and document why the -8 has to happen?
                            data = this.ReadBytes(tiffData, dataOffset - 8, count);
                            string value = ByteConvertor.ConvertBytesToASCIIString(data);

                            values.Add(new ExifStringValue(currentTag, value));
                        }
                        break;
                    case 3: // Short (2 bytes, uint16)
                    case 4: // Long (4 bytes, uint32)
                    case 9: // Slong (4 bytes, int32)
                        {
                            data = this.ReadBytes(tag, 8, 4);

                            int value = ByteConvertor.ConvertBytesToInt(data);
                            values.Add(new ExifIntegerValue(currentTag, value));
                        }
                        break;
                    case 5: // Rational (two Longs, first one is the nominator, second is the denominator)
                    case 10: // Srational (two slongs, first one is the nominator, second is the denominator)
                        {
                        }
                        break;
                    case 7: // Undefined (1 byte)
                        break;
                }
            }

            return values;
        }
    }

}
