using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// Value stored in the EXIF data
    /// </summary>
    public abstract class ExifValue<Tvalue> : ExifValue
    {
        public Tvalue Value
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    /// <summary>
    /// Value stored in the EXIF data
    /// </summary>
    /// <remarks>
    /// This only function of this non-generic class
    /// is to let IExifReaders return multiple different
    /// strong-typed values from the image's EXIF
    /// </remarks>
    public abstract class ExifValue
    {
        /// <summary>
        /// The identifying tag of the data
        /// </summary>
        public ExifTag Tag
        {
            get;
            protected set;
        }
    }
}
