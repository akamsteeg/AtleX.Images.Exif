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
            IExifReader r = new ImageExifReader(imageFileName);

            return r;
        }

        public ImageExifReader(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException(imageFileName);
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, "Can't find file '{0}'", imageFileName));         

            FileStream fs = new FileStream(imageFileName, FileMode.Open, FileAccess.Read);
            this.Open(fs);
        }

        public ImageExifReader(Stream imageData)
        {
            if (imageData == null)
                throw new ArgumentNullException("imageData");

            this.Open(imageData);
        }


        /// <summary>
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public override ExifData GetExifData()
        {
            ExifData data = this.Reader.GetExifData();

            return data;
        }

        private void Open(Stream imageData)
        {
            this.Reader = CreateReader(imageData);
            this.CanRead = (this.Reader != null);
            if (!this.CanRead)
                throw new InvalidDataException("Data is not from a supported image");
        }

        private static IExifReader CreateReader(Stream imageData)
        {   
            IExifReader readerToUse = null;

            ImageFileType fileType = FileTypeHelper.DetermineFileType(imageData);
            switch (fileType)
            {
                case ImageFileType.Jpeg:
                    {
                        readerToUse = new JpegExifReader(imageData);
                        break;
                    }
                case ImageFileType.Unknown:
                default:
                    {
                        readerToUse = null;
                        break;
                    }
            }

            return readerToUse;
        }
    }
}
