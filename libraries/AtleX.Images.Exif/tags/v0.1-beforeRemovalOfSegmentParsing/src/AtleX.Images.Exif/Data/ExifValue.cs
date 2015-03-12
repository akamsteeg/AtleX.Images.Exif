
using System.Diagnostics;
namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// Value stored in the EXIF data
    /// </summary>
    [DebuggerDisplay("{Field} - {Value}")]
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

        public ExifValue(ExifFieldType field, Tvalue value)
        {
            this.Field = field;
            this.Value = value;
        }
    }

    /// <summary>
    /// Value stored in the EXIF data
    /// </summary>
    /// <remarks>
    /// The only function of this non-generic class
    /// is to let IExifReaders return multiple different
    /// strong-typed values from the image's EXIF
    /// </remarks>
    public abstract class ExifValue
    {
        /// <summary>
        /// The identifying tag of the data
        /// </summary>
        public ExifFieldType Field
        {
            get;
            protected set;
        }
    }
}
