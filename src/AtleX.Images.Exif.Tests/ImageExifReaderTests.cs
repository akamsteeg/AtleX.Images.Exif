using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtleX.Images.Exif.Readers.Jpeg;
using AtleX.Images.Exif.Tests.Readers;
using NUnit.Framework;
using System.IO;

namespace AtleX.Images.Exif.Tests
{
    [TestFixture]
    public class ImageExifReaderTests : TestsBase
    {
        [Test]
        public void CreateJpegReader()
        {
            TestImageExifReader r = new TestImageExifReader();

            r.Open(this.JpegImageFileName);

            Assert.That(r.GetReaderType() == typeof(JpegExifReader));
        }

        [Test]
        public void CreateJpegReaderNonExistantFile()
        {
            TestImageExifReader r = new TestImageExifReader();

            Assert.Throws<FileNotFoundException>(
                () => { r.Open(this.NonExistantFile); } 
                );
        }

        [Test]
        public void CreateJpegReaderInvalidFile()
        {
            TestImageExifReader r = new TestImageExifReader();

            Assert.Throws<FileLoadException>(
                () => { r.Open(this.InvalidFilePng); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreate()
        {
            IExifReader r = TestImageExifReader.Create(this.JpegImageFileName);
            r.ReadExif();
        }

        [Test]
        public void CreateJpegReaderViaStaticCreateNonExistantFile()
        {
            Assert.Throws<FileNotFoundException>(
                () => { TestImageExifReader.Create(this.NonExistantFile); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreateInvalidFile()
        {
            Assert.Throws<FileLoadException>(
                () => { TestImageExifReader.Create(this.InvalidFilePng); }
                );
        }
    }
}
