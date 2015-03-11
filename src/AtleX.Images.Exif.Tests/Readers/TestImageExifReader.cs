using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests.Readers
{
    public class TestImageExifReader : ImageExifReader
    {
        public TestImageExifReader(string imageFileName)
            : base(imageFileName)
        {

        }

        public Type GetReaderType()
        {
            return this.Reader.GetType();
        }
    }
}
