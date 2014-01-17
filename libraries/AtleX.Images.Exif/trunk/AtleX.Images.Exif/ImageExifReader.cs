using System.Diagnostics;
using System.Net.Mime;
using AtleX.Images.Exif.Readers;
using AtleX.Images.Exif.Readers.Jpeg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Reads EXIF from a supported file
    /// </summary>
    /// <remarks>This reader acts as a factory for file-specific readers</remarks>
    public class ImageExifReader : ExifReader
    {
        protected IExifReader Reader;

        public static IExifReader Create(string imageFileName)
        {
            IExifReader r = new ImageExifReader();
            r.Open(imageFileName);

            return r;
        }

        public override void Open(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException(imageFileName);
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format("Can't find file '{0}'", imageFileName));

            this.ImageFileName = imageFileName;
            /*
             * Lazy determination based on file extension, let the expensive 
             * magic number check happen in the Reader itself
             */
            if (imageFileName.ToLower().EndsWith(".jpeg") ||
                imageFileName.ToLower().EndsWith(".jpg"))
            {
                this.CanRead = true;
                this.Reader = new JpegExifReader();
                this.Reader.Open(imageFileName);
            }
            else
            {
                this.CanRead = false;
                throw new FileLoadException(string.Format("File '{0}' is not a supported file", this.ImageFileName));
            }
        }


        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public override ExifData ReadExif()
        {
            ExifData data = this.Reader.ReadExif();

            return data;
        }
    }
}
