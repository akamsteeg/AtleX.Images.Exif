using System;
using System.Text;

namespace AtleX.Images.Exif.Helpers
{
    public static class ByteConvertor
    {
        /// <summary>
        /// Convert a 2 or 4 bytes long array to an integer
        /// </summary>
        /// <param name="value">A 2 or 4 bytes long array</param>
        /// <exception cref="ArgumentException">Thrown when there are less than 2 or more than 4 bytes in the parameter</exception>
        /// <returns></returns>
        public static int ConvertBytesToInt(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length < 2 || value.Length > 4)
                throw new ArgumentException(Strings.ExceptionCantConvertBytesToInteger, "value");

            int result;

            if (value.Length == 2)
                result = BitConverter.ToInt16(value, 0);
            else // Implies 4 bytes
                result = BitConverter.ToInt32(value, 0);

            return result;
        }

        /// <summary>
        /// Converts a byte array to a ASCII string
        /// </summary>
        /// <param name="value">Byte array to convert to a string</param>
        /// <returns></returns>
        public static string ConvertBytesToString(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string result = "";

            // Find 0-terminator
            int nullTerminatorPosition = 0;
            for (nullTerminatorPosition = value.Length - 1; nullTerminatorPosition > 0; nullTerminatorPosition--)
            {
                if (value[nullTerminatorPosition] == 0x0)
                    break;
            }

            byte[] realData = new byte[value.Length - (value.Length - nullTerminatorPosition)];
            for (int i = 0; i < realData.Length; i++)
                realData[i] = value[i];

            result = Encoding.ASCII.GetString(realData);
            return result;
        }
    }
}
