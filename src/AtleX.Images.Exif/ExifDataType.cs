using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    public enum ExifDataType
    {
        Byte = 1,
        ASCII = 2,
        Short = 3, // 2 bytes, uint16
        Long = 4, // 4 bytes, uint32
        Rational = 5, // Two Longs, first one is the nominator, second is the denominator
        SignedByte = 6, // 1 byte
        Undefined = 7, // 1 byte
        SignedShort = 8, // 2 bytes, int16
        SignedLong = 9, // 4 bytes, int32
        SignedRational = 10, // Two signed longs, first one is the numerator, second is the denominator
        SingleFloat = 11, // 4 bytes
        DoubleFloat = 12, // 8 bytes
    }
}
