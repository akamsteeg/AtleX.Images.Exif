using AtleX.Images.Exif.Readers;
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

        [Test]
        public void CreateJpegReader()
        {
            IExifReader r = Reader.OpenReader(this.JpegImageFileName);

            Assert.That(r is JpegExifReader);
        }
    }
}
