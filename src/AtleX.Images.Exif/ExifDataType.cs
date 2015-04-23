using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// The possible data types in the EXIF data
    /// </summary>
    /// <remarks>
    /// Enum names are taken directly from the EXIF standard v.2
    /// </remarks>
    public enum ExifDataType
    {
        /// <summary>
        /// The data is a byte
        /// </summary>
        /// <remarks>
        /// The length is always 1
        /// </remarks>
        Byte = 1,

        /// <summary>
        /// The data is a string
        /// </summary>
        /// <remarks>
        /// The length is of variable length
        /// </remarks>
        ASCII = 2,

        /// <summary>
        /// The data is a short, expressed as an int
        /// </summary>
        /// <remarks>
        /// The length is always 2
        /// </remarks>
        Short = 3, // 2 bytes, uint16

        /// <summary>
        /// The data is a long, expressed as a long
        /// </summary>
        /// <remarks>
        /// The length is always 4
        /// </remarks>
        Long = 4, // 4 bytes, uint32

        /// <summary>
        /// The data is two longs, a nominator and a denominator, and is
        /// expressed as a long
        /// </summary>
        /// <remarks>The length is always 8</remarks>
        Rational = 5, // Two Longs, first one is the nominator, second is the denominator

        /// <summary>
        /// The data is a signed byte, expressed as an byte
        /// </summary>
        /// <remarks>
        /// The length is always 1
        /// </remarks>
        SignedByte = 6, // 1 byte

        /// <summary>
        /// The data is undefined
        /// </summary>
        /// <remarks>
        /// The length is always 1
        /// </remarks>
        Undefined = 7, // 1 byte

        /// <summary>
        /// The data is a signed short, expressed as an int
        /// </summary>
        /// <remarks>
        /// The length is always 2
        /// </remarks>
        SignedShort = 8, // 2 bytes, int16

        /// <summary>
        /// The data is a signed long, expressed as a long
        /// </summary>
        /// <remarks>
        /// The length is always 4
        /// </remarks>
        SignedLong = 9, // 4 bytes, int32

        /// <summary>
        /// The data is two signed longs, a nominator and a denominator, and is
        /// expressed as a long
        /// </summary>
        /// <remarks>The length is always 8</remarks>
        SignedRational = 10, // Two signed longs, first one is the numerator, second is the denominator

        /// <summary>
        /// The data is a float, expressed as a float
        /// </summary>
        /// <remarks>The length is always 4</remarks>
        SingleFloat = 11, // 4 bytes

        // TODO WTF is a double float??
        /// <summary>
        /// The data is a double float
        /// </summary>
        /// <remarks>The length is always 8</remarks>
        DoubleFloat = 12, // 8 bytes
    }
}
