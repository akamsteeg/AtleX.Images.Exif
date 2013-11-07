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
        /// Open the image file
        /// </summary>
        /// <param name="imageFileName"></param>
        void Open(string imageFileName);

        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        ExifData ReadExif();
    }
}
