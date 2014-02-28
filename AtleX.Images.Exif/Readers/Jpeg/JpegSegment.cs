using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [DebuggerDisplay("{Type} segment ({Data.Length} bytes)")]
    internal class RawJpegSegment
    {
        public JpegSegmentType Type { get; set; }
        public byte[] Data { get; set; }
    }

    internal abstract class JpegSegmentParser
    {
        protected bool _isLittleEndian;
        protected readonly byte[] _data;

        public JpegSegmentParser(byte[] data)
        {
            this._data = data;
        }

        public abstract Dictionary<string, string> Parse(RawJpegSegment segment);

        public long GetByte(int startPos)
        {
            if (!this._data.Length <= startPos)
                throw new ArgumentOutOfRangeException("Cannot read past the end of the array");

            return this._data[startPos];
        }

        public string GetAscii(int startPos, bool isLittleEndian)
        {
            if (!this._data.Length+8 <= startPos)
                throw new ArgumentOutOfRangeException("Cannot read past the end of the array");

            byte[] data = new byte[8] 
            { 
                this._data[startPos],
                this._data[startPos+1],
                this._data[startPos+2],
                this._data[startPos+3],
                this._data[startPos+4],
                this._data[startPos+5],
                this._data[startPos+6],
                this._data[startPos+8],
            };

            if (!isLittleEndian)
                Array.Reverse(data);


            return
        }
    }

    internal class JpegSegmentParserApp1 : JpegSegmentParser
    {
        private bool _hasTiff;

        public override Dictionary<string, string> Parse(RawJpegSegment segment)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            this._hasTiff = false;

            /* 
             * APP1 starts with 4 bytes (0×45, 0×78, 0×69, 0×66) followed
             * by two -bytes and then the TIFF header
             * 
             * The TIFF header is 0×4949 (7373) for Intel (II, little endian)
             * or 0x4D4D (7777) for Motorola (MM, big endian)
             */

            byte[] header = this.ReadBytes(segment.Data, 0, 14);

            //Has TIFF header?
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
                // Get start offset of the TIFF header

                this.ReadTiff();
            }

            return values;
        }

        private void ReadTiff()
        {

            byte[] header = this.ReadBytes(segment.Data, 14, 2);

            if (
        }

        private byte[] ReadBytes(byte[] source, int start, int length)
        {
            if (source.Length >= start + length)
            {
                byte[] readBytes = new byte[length];
                int j = 0;
                for (int i = start; i < start + length; i++)
                {
                    readBytes[j] = source[i];
                    j++;
                }

                return readBytes;
            }
            else
                throw new IndexOutOfRangeException("Not enough bytes to read");
        }
    }

}
