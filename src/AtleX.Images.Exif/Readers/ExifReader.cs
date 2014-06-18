using AtleX.Images.Exif.Data;
using System.Collections.Generic;
using System.IO;

namespace AtleX.Images.Exif.Readers
{
    public abstract class ExifReader : IExifReader
    {
        protected Stream ImageDataStream
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
        /// Read and returns the EXIF info (if any) from the image
        /// </summary>
        /// <returns>A Dictionary with the tags and the values read from the image</returns>
        public abstract Dictionary<ExifTag, ExifValue> GetExifData();
    }
}
