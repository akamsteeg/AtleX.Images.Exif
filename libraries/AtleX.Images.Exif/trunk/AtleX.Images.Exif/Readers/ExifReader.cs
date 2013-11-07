using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Readers
{
    public abstract class ExifReader
    {
        protected virtual ExifData ReadExifFromStream(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
