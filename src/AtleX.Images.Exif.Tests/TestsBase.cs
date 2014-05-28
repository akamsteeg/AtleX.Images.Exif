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
                return @"..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6701.jpg";
                //return @"..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6573.jpg";

            }
        }

        public string JpegWithWrongExtension
        {
            get
            {
                return @"..\..\..\Testfiles\Jpeg\Canon_7D\jpegwithwrongextension.gif";
            }
        }

        public string InvalidFilePng
        {
            get
            {
                return @"..\..\..\Testfiles\Invalid\invalid.png";
            }
        }

        public string NonExistantFile
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
