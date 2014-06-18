﻿using AtleX.Images.Exif.Data;
using AtleX.Images.Exif.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    internal enum JpegSegmentType
    {
        Soi, // Start of Image  (FF D8 / 255 216)
        App1, // Application Segment 1 (FF E1 / 255 225)
        App2, // Application Segment 2 (FF E2 / 255 226)
        Dqt, // Define Quantization Table (FF DB / 255 219)
        Dht, // Define Huffman Table (FF C4 / 255 196)
        Dri, // Define Restart Interoperability (FF DD / 255 221)
        Sof, // Start of Frame (FF C0 / 255 192)
        Sos, // Start of Scan (FF C0 / 255 218)
        Eoi, // End of Image (FF D9 / 255 217)
        Unknown,
    }

    [DebuggerDisplay("{Type} ({Data.Length} bytes)")]
    internal class RawJpegSegment
    {
        public JpegSegmentType Type { get; set; }
        public byte[] Data { get; set; }
    }

    internal abstract class JpegSegmentParser
    {
        protected bool _isLittleEndian;

        public abstract Dictionary<ExifTag, ExifValue> Parse(RawJpegSegment segment);

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

            if (!this._isLittleEndian)
                readBytes.Reverse();

            return readBytes;

        }       
    }

    internal class JpegSegmentParserApp1 : JpegSegmentParser
    {
        private bool _hasTiff;

        public override Dictionary<ExifTag, ExifValue> Parse(RawJpegSegment segment)
        {
            Dictionary<ExifTag, ExifValue> values = null;
            this._hasTiff = false;

            /* 
             * APP1 starts with 4 bytes (0×45, 0×78, 0×69, 0×66) followed
             * by two -bytes and then the TIFF header
             * 
             * The TIFF header is 0×4949 (7373) for Intel (II, little endian)
             * or 0x4D4D (7777) for Motorola (MM, big endian)
             */

            byte[] header = this.ReadBytes(segment.Data, 0, 14);

            // Has TIFF header?
            if ((header[6] == 73 && header[7] == 73) || // Intel
                (header[6] == 77 && header[7] == 77)) // Motorola
            {
                this._hasTiff = true;
                /*
                 * After the TIFF header comes an extra indicator for the TIFF 
                 * header. It's 0x2A00 (4200) for little endian or 0x002A (0042)
                 * for big endian so we can use it to determine endiannes
                 */
                _isLittleEndian = (header[8] == 42 && header[9] == 0);
            }

            if (this._hasTiff)
            {
                // Get IFD offset, it's 0x00 00 00 08 if the IFD is directly after the TIFF header
                int ifdOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(header, 10, 4));

                /* 
                 * Why '6 + ifdOffset'? Because it's counting from the start of the TIFF header at 
                 * positions 6 & 7
                 */
                byte[] tiffData = this.ReadBytes(segment.Data, 6 + ifdOffset, segment.Data.Length - ifdOffset - 6);
                
                values = this.ReadTiff(tiffData);
            }

            return values;
        }

        private Dictionary<ExifTag, ExifValue> ReadTiff(byte[] tiffData)
        {
            Dictionary<ExifTag, ExifValue> values = new Dictionary<ExifTag, ExifValue>();
            int numberOfEntries = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tiffData, 0, 2));

            for (int i = 0; i < numberOfEntries; i++)
            {
                // TODO: Why '2+...'?
                // Segments are 12 bytes long
                byte[] tag = this.ReadBytes(tiffData, 2 + (i * 12) , 12);

                int tagType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 0, 2));
                int contentType = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 2, 2));
                int count = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 4, 4));
                int dataOffset = ByteConvertor.ConvertBytesToInt(this.ReadBytes(tag, 8, 4));

                switch (contentType)
                {
                    case 1: // Byte
                    case 2: // ASCII
                        {
                            ExifTag currentTag = (ExifTag)tagType;

                            // TODO: Find out and document why the -8 has to happen?
                            byte[] data = this.ReadBytes(tiffData, dataOffset-8, count);

                            string value = ByteConvertor.ConvertBytesToString(data);

                            values.Add(currentTag, new ExifStringValue(currentTag, value));
                        }
                        break;
                    case 3: // Short (2 bytes, uint16)
                    case 4: // Long (4 bytes, uint32)
                    case 5: // Rational (two Longs, first one is the nominator, second is the denominator)
                    case 7: // Undefined (1 byte)
                    case 9: // Slong (4 bytes, int32)
                    case 10: // Srational (two slongs, first one is the nominator, second is the denominator)
                        break;
                }
            }

            return values;
        }
    }

}
