using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Helpers
{
    public static class ByteConvertor
    {
        /// <summary>
        /// Convert a 2 or 4 bytes long array to an integer
        /// </summary>
        /// <param name="bytes">A 2 or 4 bytes long array</param>
        /// <exception cref="ArgumentException">Thrown when there are less than 2 or more than 4 bytes in the parameter</exception>
        /// <returns></returns>
        public static int ConvertBytesToInt(byte[] bytes)
        {
            if (bytes.Length < 2 || bytes.Length > 4)
                throw new ArgumentException("An integer is either 2 or 4 bytes long", "bytes");

            int value;

            if (bytes.Length == 2)
                value = BitConverter.ToInt16(bytes, 0);
            else // Implies 4 bytes
                value = BitConverter.ToInt32(bytes, 0);

            return value;
        }

        /// <summary>
        /// Converts a byte array to a ASCII string
        /// </summary>
        /// <param name="bytes">Byte array to convert to a string</param>
        /// <returns></returns>
        public static string ConvertBytesToString(byte[] bytes)
        {
            string value = "";

            // Find 0-terminator
            int nullTerminatorPosition = 0;
            for (nullTerminatorPosition = bytes.Length - 1; nullTerminatorPosition > 0; nullTerminatorPosition--)
            {
                if (bytes[nullTerminatorPosition] == 0x0)
                    break;
            }

            byte[] realData = new byte[bytes.Length - (bytes.Length - nullTerminatorPosition)];
            for (int i = 0; i < realData.Length; i++)
                realData[i] = bytes[i];

            value = Encoding.ASCII.GetString(realData);
            return value;
        }
    }
}
