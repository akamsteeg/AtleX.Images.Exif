using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers
{
    public abstract class ExifReader : IExifReader
    {
        protected string _imageFileName;

        /// <summary>
        /// Open the image
        /// </summary>
        /// <param name="imageFileName"></param>
        public virtual void Open(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException(imageFileName);
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format("Can't find file '{0}'", imageFileName));

            this._imageFileName = imageFileName;
        }

        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public abstract ExifData ReadExif();
    }
}
