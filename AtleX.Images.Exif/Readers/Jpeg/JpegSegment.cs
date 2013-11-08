using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public enum JpegSegmentType
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
    public class JpegSegment
    {
        public JpegSegmentType Type { get; set; }
        public byte[] Data { get; set; }
    }
}
