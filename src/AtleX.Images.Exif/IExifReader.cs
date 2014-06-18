using AtleX.Images.Exif.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
