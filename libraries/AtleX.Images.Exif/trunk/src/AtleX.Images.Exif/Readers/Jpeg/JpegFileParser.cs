using AtleX.Images.Exif.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    internal class JpegFileParser
    {
        internal JpegFileParser()
        {
        }

        /// <summary>
        /// Parse the file header into segments
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public IEnumerable<RawJpegSegment> ParseHeaderIntoSegments(BinaryReader reader)
        {
            /*
             * Reset the stream because we want the full header to extract the App 
             * segments from the whole file
             */
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            List<RawJpegSegment> segments = new List<RawJpegSegment>();

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
                JpegSegmentType segmentType = AdvanceReaderToNextSegment(reader);

                switch(segmentType)
                {
                    case JpegSegmentType.App1:
                    case JpegSegmentType.App2:
                        {
                            long segmentStartPos = reader.BaseStream.Position;

                            // The length of the segment is specified after the segment marker in two bytes
                            byte[] segmentLengthSpecification = reader.ReadBytes(2);
                            int segmentLength = ByteConvertor.ConvertBytesToInt(segmentLengthSpecification);  //segmentLengthSpecification[0] << 8 | segmentLengthSpecification[1];

                            // Read the data
                            byte[] segmentData = reader.ReadBytes(segmentLength);

                            RawJpegSegment segment = new RawJpegSegment()
                            {
                                Data = segmentData,
                                Type = segmentType,
                            };
                            segments.Add(segment);

                            break;
                        }
                    case JpegSegmentType.Dht:
                    case JpegSegmentType.Dqt:
                    case JpegSegmentType.Dri:
                    case JpegSegmentType.Sos:
                        {
                            headerEnd = true;
                            break;
                        }
                }
            }

            return segments;
        }

        /// <summary>
        /// Advance the reader to the start of the next segment
        /// </summary>
        /// <remarks>
        /// While this might seem a very inefficient method (no buffering, reading 
        /// 2 bytes at a time), it's actually very fast when the reader reads from a FileStream
        /// because a FileStream is buffered by default (http://blogs.msdn.com/b/brada/archive/2004/04/15/114329.aspx)
        /// </remarks>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static JpegSegmentType AdvanceReaderToNextSegment(BinaryReader reader)
        {
            bool segmentFound = false;
            JpegSegmentType type = JpegSegmentType.Unknown;
            while (reader.BaseStream.Position != reader.BaseStream.Length && !segmentFound)
            {
                Byte[] markerBytes = reader.ReadBytes(2);

                if (markerBytes[0] == 255) // We propably arrived at a header
                {
                    type = GetTypeFromSegmentCode(markerBytes);
                    if (type != JpegSegmentType.Unknown)
                    {
                        // Reset the reader to the beginning of the marker
                        //reader.BaseStream.Seek(, SeekOrigin.Current);
                        segmentFound = true;
                    }
                }
            }

            return type;
        }

        protected static bool HasApp1(BinaryReader reader)
        {
            bool result = false;
            /*
             * Reset the stream to just after the JPEG magic numbers at 
             * the beginning of the file
             */
            reader.BaseStream.Seek(2, SeekOrigin.Begin);

            byte[] exifMarker = reader.ReadBytes(10);

            // An APP1 segment is indicated by "FF E1 Exif null null" (255 225 69 120 105 102 0 0)
            if (exifMarker == new byte[] { 255, 225, 69, 120, 105, 102, 0, 0 }) // We have a JFIF/EXIF segment
            {
                // Ladies & gentlemen, we have found EXIF info!
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Decode the segment marker into a segment type
        /// </summary>
        /// <param name="segmentCode">The two bytes of the segment marker</param>
        /// <returns></returns>
        protected static JpegSegmentType GetTypeFromSegmentCode(byte[] segmentCode)
        {
            if (segmentCode.Length != 2)
                throw new ArgumentException("A segment code is two bytes", "segmentCode");
            if (segmentCode[0] != 255)
                throw new ArgumentException("The first byte of a segment code should be 0xFF");

            JpegSegmentType result = JpegSegmentType.Unknown;
            switch (segmentCode[1])
            {
                case 216:
                    result = JpegSegmentType.Soi;
                    break;
                case 225:
                    result = JpegSegmentType.App1;
                    break;
                case 226:
                    result = JpegSegmentType.App2;
                    break;
                case 219:
                    result = JpegSegmentType.Dqt;
                    break;
                case 196:
                    result = JpegSegmentType.Dht;
                    break;
                case 221:
                    result = JpegSegmentType.Dri;
                    break;
                case 192:
                    result = JpegSegmentType.Sof;
                    break;
                case 218:
                    result = JpegSegmentType.Sos;
                    break;
                case 217:
                    result = JpegSegmentType.Eoi;
                    break;
            }

            return result;
        }
    }
}
