using System;

namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// Byte value stored in the EXIF data
    /// </summary>
    public class ExifByteValue : ExifValue<byte>
    {
        public ExifByteValue(ExifFieldType field, byte value)
            : base(field, value)
        {
        }
    }
}
