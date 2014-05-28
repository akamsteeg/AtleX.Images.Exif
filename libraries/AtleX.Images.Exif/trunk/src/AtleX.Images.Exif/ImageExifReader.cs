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
using System.Globalization;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Reads EXIF from a supported file
    /// </summary>
    /// <remarks>This reader acts as a factory for file-specific readers</remarks>
    public class ImageExifReader : ExifReader
    {           
        protected IExifReader Reader
        {
            get;
            set;
        }

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
            this.CanRead = true; // Positive scenario, we'll set this to False if we try to load an unknown file.

            IExifReader newReader;

            FileType fileType = FileTypeHelper.DetermineFileType(imageFileName);
            switch (fileType)
            {
                case FileType.Jpeg:
                    {
                        newReader = new JpegExifReader();
                        break;
                    }
                case FileType.Unknown:
                default:
                    this.CanRead = false;
                    throw new FileLoadException(string.Format("File '{0}' is not a supported file", this.ImageFileName));
            }

            try
            {
                newReader.Open(imageFileName);
                this.Reader = newReader;
            }
            finally
            {
                this.CanRead = false;
            }
        }


        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public override ExifData GetExif()
        {
            ExifData data = this.Reader.GetExif();

            return data;
        }
    }
}
