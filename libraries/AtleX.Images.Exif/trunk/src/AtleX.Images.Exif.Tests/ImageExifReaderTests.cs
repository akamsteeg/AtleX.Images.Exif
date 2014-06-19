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
        public override string TestImageFileName
        {
            get
            {
                return @"..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6701.jpg";

            }
        }

        [Test]
        public void CreateJpegReader()
        {
            TestImageExifReader r = new TestImageExifReader(this.TestImageFileName);

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
            Assert.Throws<InvalidDataException>(
                () => { new TestImageExifReader(this.InvalidFilePng); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreate()
        {
            ExifReader r = TestImageExifReader.Create(this.TestImageFileName);
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
            Assert.Throws<InvalidDataException>(
                () => { TestImageExifReader.Create(this.InvalidFilePng); }
                );
        }
    }
}
