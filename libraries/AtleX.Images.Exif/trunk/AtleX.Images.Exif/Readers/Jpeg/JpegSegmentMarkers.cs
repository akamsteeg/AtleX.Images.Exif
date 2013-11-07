using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers.Jpeg
{
    public static class JpegSegmentMarkers
    {
        // Start of image (FF D8 / 255 216)
        public static byte[] Soi = new byte[] { 255, 216 };
        // App1 (FF E1 / 255 225)
        public static byte[] App1 = new byte[] { 255, 225 };
        // App1 (FF E2 / 255 226)
        public static byte[] App2 = new byte[] { 255, 226 };
        // Define Quantization Table (FF DB / 255 219)
        public static byte[] Dqt = new byte[] { 255, 219 };
        // Define Huffman Table (FF C4 / 255 196)
        public static byte[] Dht = new byte[] { 255, 196 };
        // Define Restart Interoperability (FF DD / 255 221)
        public static byte[] Dri = new byte[] { 255, 221 };
        // Start of Frame (FF C0 / 255 192)
        public static byte[] Sof = new byte[] { 255, 192 };
        // Start of Scan (FF C0 / 255 218)
        public static byte[] Sos = new byte[] { 255, 218 };
        // End of image (FF D9 / 255 217)
        public static byte[] Eoi = new byte[] { 255, 217 };
    }
}
