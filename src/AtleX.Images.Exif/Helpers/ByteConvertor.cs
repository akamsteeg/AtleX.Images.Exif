﻿using System;
using System.Linq;
using System.Text;

namespace AtleX.Images.Exif.Helpers
{
    public static class ByteConvertor
    {
        /// <summary>
        /// Convert a 2 or 4 bytes long array to a 16 or 32 bits integer
        /// </summary>
        /// <param name="value">
        /// A 2 or 4 bytes long array
        /// </param>
        /// <param name="treatAsLittleEndian">
        /// True when the data is little endian, false when big endian.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when there are less than 2 or more than 4 bytes in the parameter
        /// </exception>
        /// <returns>
        /// </returns>
        public static int ConvertBytesToInt(byte[] value, bool treatAsLittleEndian = true)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length != 2 && value.Length != 4)
                throw new ArgumentException(Strings.ExceptionCantConvertBytesToInteger, "value");

            if (!treatAsLittleEndian)
            {
                value = ConvertBigToLittleEndian(value);
            }

            int result;

            if (value.Length == 2) // TODO: Determine unsigned or signed int
            {
                result = value[0] | (value[1] << 8);
            }
            else // Implies 4 bytes
            {
                result = value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
            }

            return result;
        }

        /// <summary>
        /// Converts a byte array to a ASCII string
        /// </summary>
        /// <param name="value">
        /// Byte array to convert to a string
        /// </param>
        /// <returns>
        /// The converted string, or an empty string when no null-terminator was
        /// found in the byte array
        /// </returns>
        /// <remarks>
        /// </remarks>
        public static string ConvertBytesToASCIIString(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string result = "";

            if (value.Length != 0)
            {
                // Find 0-terminator
                int nullTerminatorPosition = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == 0x0)
                    {
                        nullTerminatorPosition = i;
                        break;
                    }
                }

                byte[] realData = new byte[value.Length - (value.Length - nullTerminatorPosition)];
                for (int i = 0; i < realData.Length; i++)
                {
                    realData[i] = value[i];
                }

                result = Encoding.ASCII.GetString(realData);
            }

            return result;
        }

        /// <summary>
        /// Convert a big endian byte array to little endian
        /// </summary>
        /// <param name="value">
        /// The byte array to convert
        /// </param>
        /// <returns>
        /// The converted byte array
        /// </returns>
        private static byte[] ConvertBigToLittleEndian(byte[] value)
        {
            /*
             * We don't want to do this with LINQ (value.Reverse().ToArray())
             * because it's way too slow (~10x) and allocates a lot of memory
             */
            var result = new byte[value.Length];

            for (var i = 0; i < value.Length; i++)
            {
                result[i] = value[value.Length - 1 - i];
            }

            return result;
        }
    }
}