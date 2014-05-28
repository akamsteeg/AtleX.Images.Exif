using AtleX.Images.Exif;
using AtleX.Images.Exif.Readers;
using AtleX.Images.Exif.Readers.Jpeg;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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
            IExifReader r = new JpegExifReader(this.JpegImageFileName);
            Assert.IsTrue(r is JpegExifReader);
        }

        [Test]
        public void CreateReaderAndLoadCorrectlyLoadJpegWithWrongExtension()
        {
            IExifReader r = new JpegExifReader(this.JpegWithWrongExtension);
            ExifData d = r.GetExifData();

            Assert.IsNotNull(d);
        }

        [Test]
        public void CreateReaderAndLoadInvalidFile()
        {
            Assert.Throws<FileLoadException>(
                () => {  new JpegExifReader(this.InvalidFilePng); }
                );
        }

        [Test]
        public void ReadExifFromJpeg()
        {
            IExifReader r = new JpegExifReader(this.JpegImageFileName);
            ExifData d = r.GetExifData();

            Assert.IsNotNull(d);
        }
    }
}
