using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Data
{
    /// <summary>
    /// Integer value stored in the EXIF data
    /// </summary>
    public class ExifIntegerValue : ExifValue<int>
    {
        public ExifIntegerValue(ExifTag tag, int value)
        {
            throw new NotImplementedException();
        }
    }
}
