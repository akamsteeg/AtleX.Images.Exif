using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// String value stored the EXIF data
    /// </summary>
    public class ExifStringValue : ExifValue<string>
    {
        public ExifStringValue(ExifTag tag, string value)
        {
            this.Tag = tag;
            this.Value = value;
        }
    }
}
