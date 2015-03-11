using AtleX.Images.Exif.Data;
using AtleX.Images.Exif.Helpers;
using AtleX.Images.Exif.Readers;
using AtleX.Images.Exif.Readers.Jpeg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Reads EXIF data from a supported file
    /// </summary>
    /// <remarks>
    /// This reader acts as a factory for file-specific readers. It determines
    /// the file type of the image and instantiates the correct type-specific
    /// reader. 
    /// 
    /// Just like the type-specific readers it implements ExifReader, so its
    /// public signature is the same. This makes it interchangeable with 
    /// manually instantiating type-specific readers.
    /// </remarks>
    public class ImageExifReader : ExifReader
    {           
        protected ExifReader InternalReader
        {
            get;
            set;
        }

        /// <summary>
        /// Instantiates a reader and loads the image
        /// </summary>
        /// <param name="imageFileName"></param>
        /// <returns></returns>
        public static ExifReader Create(string imageFileName)
        {
            ExifReader r = new ImageExifReader(imageFileName);

            return r;
        }

        /// <summary>
        /// Create the reader and configure it to read from a file
        /// </summary>
        /// <param name="imageFileName">A valid filename of an image</param>
        public ImageExifReader(string imageFileName)
        {
            if (string.IsNullOrEmpty(imageFileName))
                throw new ArgumentNullException("imageFileName");
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.ExceptionFileNotFound, imageFileName));

            FileStream fs = new FileStream(imageFileName, FileMode.Open, FileAccess.Read);
            this.Open(fs);
        }

        /// <summary>
        /// Create the reader and configure it to read from a Stream
        /// </summary>
        /// <param name="imageData">A readable stream with binary image data</param>
        public ImageExifReader(Stream imageData)
        {
            if (imageData == null)
                throw new ArgumentNullException("imageData");

            this.Open(imageData);
        }


        /// <summary>
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>A Dictionary with the tags and the values read from the image</returns>
        public override IEnumerable<ExifValue> GetExifData()
        {
            IEnumerable<ExifValue> data = this.InternalReader.GetExifData();

            return data;
        }

        /// <summary>
        /// Create a reader based on the contents of the Stream
        /// </summary>
        /// <param name="imageData"></param>
        protected virtual void Open(Stream imageData)
        {
            this.InternalReader = this.CreateReader(imageData);
            this.CanRead = (this.InternalReader != null);
            if (!this.CanRead)
                throw new InvalidDataException(Strings.ExceptionUnsupportedImageData);
        }

        /// <summary>
        /// Detect the image type from the passed Stream and instantiate
        /// the approriate reader for it.
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        protected virtual ExifReader CreateReader(Stream imageData)
        {
            ExifReader readerToUse = null;

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
