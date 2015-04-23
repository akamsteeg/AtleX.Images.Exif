using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Helpers
{
    public static class ExifDataTypeConvertor
    {
        /// <summary>
        /// Convert a byte array with one byte to a byte
        /// </summary>
        /// <param name="data">A byte array with one byte</param>
        /// <returns>The data, converted to byte</returns>
        /// <remarks>O yes, I do realize how silly this method is...</remarks>
        public static byte ToByte(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != 1)
                throw new ArgumentOutOfRangeException(string.Format(Strings.ExceptionDataOutOfRange, 1));

            byte result;

            result = data[0];

            return result;
        }

        /// <summary>
        /// Convert a byte-array to an ASCII string
        /// </summary>
        /// <param name="data">The byte array to convert</param>
        /// <returns>The data, converted to string</returns>
        public static string ToASCII(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            string result;

            result = ByteConvertor.ConvertBytesToASCIIString(data);

            return result;
        }

        /// <summary>
        /// Convert a byte-array to a short
        /// </summary>
        /// <param name="data">The byte array to convert</param>
        /// <param name="isLittleEndian">
        /// True when the data should be considered little-endian, false otherwise
        /// </param>
        /// <returns>The data, converted to int</returns>
        public static int ToShort(byte[] data, bool isLittleEndian)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != 2)
                throw new ArgumentOutOfRangeException(string.Format(Strings.ExceptionDataOutOfRange, 2));

            int result;

            result = ByteConvertor.ConvertBytesToInt(data, isLittleEndian);

            return result;
        }

        /// <summary>
        /// Convert a byte-array to a long
        /// </summary>
        /// <param name="data">The byte array to convert</param>
        /// <param name="isLittleEndian">
        /// True when the data should be considered little-endian, false otherwise
        /// </param>
        /// <returns>The data, converted to long</returns>
        public static long ToLong(byte[] data, bool isLittleEndian)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != 4)
                throw new ArgumentOutOfRangeException(string.Format(Strings.ExceptionDataOutOfRange, 4));

            long result;

            result = ByteConvertor.ConvertBytesToInt(data, isLittleEndian);

            return result;
        }

        /// <summary>
        /// Convert a byte-array to a long
        /// </summary>
        /// <param name="data">The byte array to convert</param>
        /// <param name="isLittleEndian">
        /// True when the data should be considered little-endian, false otherwise
        /// </param>
        /// <returns>The data, converted to long</returns>
        public static long ToRational(byte[] data, bool isLittleEndian)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length != 8)
                throw new ArgumentOutOfRangeException(string.Format(Strings.ExceptionDataOutOfRange, 8));

            long result;

            // TODO Fix this to avoid the LINQ stuff
            byte[] numeratorPart = data.Take(4).ToArray();
            byte[] denominatorPart = data.Skip(4).Take(4).ToArray();

            int numerator = ByteConvertor.ConvertBytesToInt(numeratorPart, isLittleEndian);
            int denominator = ByteConvertor.ConvertBytesToInt(denominatorPart, isLittleEndian);

            result = numerator / denominator;

            return result;
        }


        //public static T ConvertTo<T>(ExifDataType type, byte[] data)
        //{
        //    T result;
        //    switch (type)
        //    {
        //        case ExifDataType.Byte: // Byte
        //        case ExifDataType.Undefined: // Undefined (1 byte)
        //            {
        //                result = (T)(object)data[0];
        //            }
        //            break;

        //        case 2: // ASCII
        //            {
        //                result = (T)(object)ByteConvertor.ConvertBytesToASCIIString(data);
        //            }
        //            break;

        //        case 3: // Short (2 bytes, uint16)
        //            {
        //                data = this.ReadBytes(tag, 8, 2);

        //                int value = ByteConvertor.ConvertBytesToInt(data, this._isLittleEndian);
        //                values.Add(new ExifValue(currentTag, value));
        //            }
        //            break;

        //        case 4: // Long (4 bytes, uint32)
        //        case 9: // Slong (4 bytes, int32)
        //            {
        //                data = this.ReadBytes(tag, 8, 4);

        //                int value = ByteConvertor.ConvertBytesToInt(data, this._isLittleEndian);
        //                values.Add(new ExifValue(currentTag, value));
        //            }
        //            break;

        //        case 5: // Rational (two Longs, first one is the nominator, second is the denominator)
        //        case 10: // Srational (two slongs, first one is the nominator, second is the denominator)
        //            {
        //                byte[] numeratorPart = this.ReadBytes(tag, 4, 4);
        //                byte[] denominatorPart = this.ReadBytes(tag, 8, 4);

        //                int numerator = ByteConvertor.ConvertBytesToInt(numeratorPart, this._isLittleEndian);
        //                int denominator = ByteConvertor.ConvertBytesToInt(denominatorPart, this._isLittleEndian);

        //                int value = numerator / denominator;
        //                values.Add(new ExifValue(currentTag, value));
        //            }
        //            break;
        //    }

        //    return result;
        //}
    }
}
