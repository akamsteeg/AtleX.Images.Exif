using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Data
{
    public abstract class ExifValue<Tvalue> : ExifValue
    {
        public Tvalue Value
        {
            get;
            protected set;
        }

        public ExifTag Tag
        {
            get;
            protected set;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    public abstract class ExifValue
    {
        // To facilitate the non-generic references in IExifReader
    }
}
