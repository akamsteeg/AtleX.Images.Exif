using AtleX.Images.Exif.Data;
using System.Collections.Generic;

namespace AtleX.Images.Exif
{
    public interface IExifReader
    {
        /// <summary>
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>A Dictionary with the tags and the values read from the image</returns>
        Dictionary<ExifTag, ExifValue> GetExifData();
    }
}
