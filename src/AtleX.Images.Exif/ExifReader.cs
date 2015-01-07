using AtleX.Images.Exif.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace AtleX.Images.Exif
{
    /// <summary>
    /// Read exif data from an image
    /// </summary>
    /// <remarks>
    /// This is a base class for all readers
    /// </remarks>
    public abstract class ExifReader : IDisposable
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
        public abstract IEnumerable<ExifValue> GetExifData();
    
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.ImageDataStream != null)
                {
                    this.ImageDataStream.Dispose();
                    this.ImageDataStream = null;
                }
            }
        }
}
}
