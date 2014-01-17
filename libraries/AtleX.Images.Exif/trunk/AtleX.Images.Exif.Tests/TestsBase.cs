using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests
{
    public class TestsBase
    {
        public string JpegImageFileName
        {
            get
            {
                return @"..\..\..\Testfiles\Jpeg\Nikon_D3100\mattus82_10709867984.jpg";
            }
        }

        public string UnknownImageFileName
        {
            get
            {
                return @".\image.unknown";
            }
        }

        public static IExifReader CreateReaderAndOpenImage<T>(string imageFileName) where T : new()
        {
            IExifReader reader = new T() as IExifReader;
            reader.Open(imageFileName);

            return reader;
        }
    }
}
