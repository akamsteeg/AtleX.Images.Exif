using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public class JpegExifReader : ExifDataReader
    {
        /// <summary>
        /// Open the image
        /// </summary>
        /// <param name="imageFileName"></param>
        public override void Open(string imageFileName)
        {
            base.Open(imageFileName);

            // Lazy extension check first, before doing the expensive Magic Numbers check
            if (!imageFileName.ToLower().EndsWith("jpeg") 
                && !imageFileName.ToLower().EndsWith("jpg")
                && FileTypeHelper.DetermineFileType(imageFileName) != FileType.Jpeg
                )
            {
                this._canRead = false;
                throw new FileLoadException(string.Format("File '{0}' is not a JPEG file", this._imageFileName));
            }
        }

        /// <summary>
        /// Read the data from the file
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override ExifData ReadExifFromBinaryReader(BinaryReader reader)
        {
            ExifData ed = new ExifData();

            IEnumerable<JpegSegment> segments = this.ParseHeader(reader);


            return ed;
        }

        protected IEnumerable<JpegSegment> ParseHeader(BinaryReader reader)
        {
            // Read file for App blocks

            /*
             * Reset the stream because we want the full header to extract the App segments from
             */
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            List<JpegSegment> segments = new List<JpegSegment>();

            bool headerEnd = false;
            /*
             * The header with App segments ends when the DHT,
             * DQT, DRI or SOF starts. To quote from the Exif specs:
             * 
             * "DQT, DHT, DRI and SOF may line up in any order, but 
             * shall be recorded after APP1 (or APP2 if any) and 
             * before SOS"
             */
            while (reader.BaseStream.Position != reader.BaseStream.Length && headerEnd == false)
            {

                this.AdvanceReaderToNextSegment(reader);

                byte[] markerBytes = reader.ReadBytes(2);

                // TODO: Read the segment

                if (markerBytes[0] == 255) // We propably arrived at a header
                {
                    // App1 and App2 are the ones we are interested in
                    if (markerBytes[1] == JpegSegmentMarkers.App1[1]
                        || markerBytes[1] == JpegSegmentMarkers.App2[1]
                        )
                    {
                        /*
                         * The first two bytes after a segment header are
                         * indicating the length of the segment
                         */
                        byte[] segmentLengthDefinition = reader.ReadBytes(2);
                    }
                    // DHT, DQT, DRI & SOF mark the end of the header
                    else if (markerBytes[1] == JpegSegmentMarkers.Dht[1]
                        || markerBytes[1] == JpegSegmentMarkers.Dqt[1]
                        || markerBytes[1] == JpegSegmentMarkers.Dri[1]
                        || markerBytes[1] == JpegSegmentMarkers.Sof[1]
                    )
                    {
                        headerEnd = true;
                    }
                }
            }

            return segments;
        }

        protected void AdvanceReaderToNextSegment(BinaryReader reader)
        {
            Byte[] markerBytes = reader.ReadBytes(2);

            if (markerBytes[0] == 255) // We propably arrived at a header
            {
                // App1 and App2 are the ones we are interested in
                if (markerBytes[1] == JpegSegmentMarkers.App1[1]
                    || markerBytes[1] == JpegSegmentMarkers.App2[1]
                    || markerBytes[1] == JpegSegmentMarkers.Dht[1]
                    || markerBytes[1] == JpegSegmentMarkers.Dqt[1]
                    || markerBytes[1] == JpegSegmentMarkers.Dri[1]
                    || markerBytes[1] == JpegSegmentMarkers.Sof[1]
                )
                {
                    // Reset the reader to the beginning of the marker
                    reader.BaseStream.Seek(-2, SeekOrigin.Current);
                }
            }
        }

        protected bool HasApp1(BinaryReader reader)
        {
            bool result = false;
            /*
             * Reset the stream to just after the JPEG magic numbers at 
             * the beginning of the file
             */
            reader.BaseStream.Seek(2, SeekOrigin.Begin);

            byte[] exifMarker = reader.ReadBytes(10);

            // An APP1 segment is indicated by "FF E1 Exif null null" (255 225 69 120 105 102 0 0)
            if (exifMarker == new byte[] {255, 225, 69, 120, 105, 102, 0, 0}) // We have a JFIF/EXIF segment
            {
                // Ladies & gentlemen, we have found EXIF info!
                result = true;
            }

            return result;
        }
    }
}
