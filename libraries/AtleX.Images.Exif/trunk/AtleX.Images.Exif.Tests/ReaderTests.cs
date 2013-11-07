using AtleX.Images.Exif;
using AtleX.Images.Exif.Readers;
using AtleX.Images.Exif.Readers.Jpeg;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests
{
    [TestFixture]
    public class ReaderTests
    {
        public string JpegImageFileName
        {
            get
            {
                return @"..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6573.jpg";
            }
        }

        public static IExifReader CreateReader<T>(string imageFileName) where T: new()
        {
            IExifReader reader = new T() as IExifReader;
            reader.Open(imageFileName);

            return reader;
        }

        [Test]
        public void CreateJpegReaderViaFactoryReader()
        {
            IExifReader r = CreateReader<ImageExifReader>(this.JpegImageFileName);

            Assert.That(r is JpegExifReader);
        }

        [Test]
        public void ReadExifFromJpeg()
        {
            IExifReader r = CreateReader<JpegExifReader>(this.JpegImageFileName);

            ExifData ed = r.ReadExif();

            Assert.IsNotNull(ed);
        }
    }
}
