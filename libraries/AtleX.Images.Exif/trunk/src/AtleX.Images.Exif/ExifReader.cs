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
    /// This is the base class for all readers
    /// </remarks>
    public abstract class ExifReader : IDisposable
    {
        protected Stream ImageDataStream
        {
            get;
            set;
        }

        /// <summary>
        /// Is true when the ExifReader can read data from the image, false otherwise
        /// </summary>
        /// <remarks>
        /// GetExifData() must check the value of CanRead. It should throw an error
        /// when CanRead is false
        /// </remarks>
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
