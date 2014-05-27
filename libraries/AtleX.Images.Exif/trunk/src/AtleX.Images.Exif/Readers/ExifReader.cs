using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers
{
    public abstract class ExifReader : IExifReader
    {
        protected string ImageFileName
        {
            get;
            set;
        }

        protected bool CanRead
        {
            get;
            set;
        }

        /// <summary>
        /// Open the image
        /// </summary>
        /// <param name="imageFileName"></param>
        public abstract void Open(string imageFileName);

        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public abstract ExifData ReadExif();
    }
}
