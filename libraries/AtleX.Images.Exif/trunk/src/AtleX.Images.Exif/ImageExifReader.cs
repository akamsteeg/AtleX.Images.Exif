using AtleX.Images.Exif.Helpers;
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
    /// <para>
    /// This reader acts as a factory for file-specific readers. It determines
    /// the file type of the image and instantiates the correct type-specific reader.
    /// </para>
    /// <para>
    /// Just like the type-specific readers it implements ExifReader, so its
    /// public signature is the same. This makes it interchangeable with
    /// manually instantiating type-specific readers.
    /// </para>
    /// </remarks>
    public class ImageExifReader : ExifReader
    {
        protected ExifReader InternalReader
        {
            get;
            set;
        }

        /// <summary>
        /// Create the reader and configure it to read from a file
        /// </summary>
        /// <param name="imageFileName">
        /// A valid filename of an image
        /// </param>
        public ImageExifReader(string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName))
                throw new ArgumentNullException("imageFileName");
            if (!File.Exists(imageFileName))
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.ExceptionFileNotFound, imageFileName));

            FileStream fs = new FileStream(imageFileName, FileMode.Open, FileAccess.Read);
            this.Open(fs);
        }

        /// <summary>
        /// Create the reader and configure it to read from a Stream
        /// </summary>
        /// <param name="imageData">
        /// A readable stream with binary image data
        /// </param>
        public ImageExifReader(Stream imageData)
        {
            if (imageData == null)
                throw new ArgumentNullException("imageData");

            this.Open(imageData);
        }

        /// <summary>
        /// Instantiates a reader and loads the image
        /// </summary>
        /// <param name="imageFileName">
        /// The filename of the image to load
        /// </param>
        /// <returns>
        /// </returns>
        public static ExifReader Create(string imageFileName)
        {
            ExifReader r = new ImageExifReader(imageFileName);

            return r;
        }

        /// <summary>
        /// Instantiates a reader and loads the image
        /// </summary>
        /// <param name="imageData">
        /// Stream with the image data
        /// </param>
        /// <returns>
        /// </returns>
        public static ExifReader Create(Stream imageData)
        {
            ExifReader r = new ImageExifReader(imageData);

            return r;
        }

        /// <summary>
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>
        /// A collection with the tags and the values read from the image
        /// </returns>
        public override IEnumerable<ExifValue> GetExifData()
        {
            IEnumerable<ExifValue> data = this.InternalReader.GetExifData();

            return data;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.InternalReader != null)
                {
                    this.InternalReader.Dispose();
                    this.InternalReader = null;
                }
            }
        }

        /// <summary>
        /// Create a reader based on the contents of the Stream
        /// </summary>
        /// <param name="imageData">
        /// </param>
        private void Open(Stream imageData)
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

            this.CanRead = (readerToUse != null);
            this.InternalReader = readerToUse;
            if (!this.CanRead)
                throw new InvalidDataException(Strings.ExceptionUnsupportedImageData);
        }
    }
}