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
        /// Get the EXIF from an image
        /// </summary>
        /// <returns></returns>
        ExifData ReadExif();
    }
}
