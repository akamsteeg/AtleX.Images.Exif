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
            TestImageExifReader r = new TestImageExifReader(this.JpegImageFileName);

            Assert.That(r.GetReaderType() == typeof(JpegExifReader));
        }

        [Test]
        public void CreateJpegReaderNonExistantFile()
        {
            Assert.Throws<FileNotFoundException>(
                () => { new TestImageExifReader(this.NonExistantFile); } 
                );
        }

        [Test]
        public void CreateJpegReaderInvalidFile()
        {
            Assert.Throws<FileLoadException>(
                () => { new TestImageExifReader(this.InvalidFilePng); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreate()
        {
            IExifReader r = TestImageExifReader.Create(this.JpegImageFileName);
            r.GetExifData();
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
