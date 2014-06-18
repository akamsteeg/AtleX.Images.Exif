using AtleX.Images.Exif.Data;
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
        /// Read the EXIF info (if any) from the image
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<ExifTag, ExifValue> GetExifData();
    }
}
