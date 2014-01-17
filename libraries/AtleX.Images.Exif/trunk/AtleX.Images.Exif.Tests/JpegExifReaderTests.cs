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
    public class JpegExifReaderTests : TestsBase
    {
        [Test]
        public void CreateJpegReader()
        {
            IExifReader r = new JpegExifReader();
            Assert.IsTrue(r is JpegExifReader);
        }

        [Test]
        public void ReadExifFromJpeg()
        {
            IExifReader r = CreateReaderAndOpenImage<JpegExifReader>(this.JpegImageFileName);

            ExifData d = r.ReadExif();

            Assert.IsNotNull(d);
        }
    }
}
