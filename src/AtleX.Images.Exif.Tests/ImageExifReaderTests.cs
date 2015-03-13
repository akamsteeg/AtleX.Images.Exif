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
    public class ImageExifReaderTests
    {
        public string TestImageFileName
        {
            get
            {
                return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6701.jpg";

            }
        }

        public string InvalidFilePng
        {
            get
            {
                return @"..\..\..\..\Testfiles\Invalid\invalid.png";
            }
        }

        public string NonExistantFile
        {
            get
            {
                return @".\image.unknown";
            }
        }

        [Test]
        public void CreateJpegReader_Successful()
        {
            TestImageExifReader r = new TestImageExifReader(this.TestImageFileName);

            Assert.That(r.GetReaderType() == typeof(JpegExifReader));
        }

        [Test]
        public void CreateJpegReaderNonExistantFileThrowsException_Successful()
        {
            Assert.Throws<FileNotFoundException>(
                () => { new TestImageExifReader(this.NonExistantFile); } 
                );
        }

        [Test]
        public void CreateJpegReaderInvalidFileThrowsException_Successful()
        {
            Assert.Throws<InvalidDataException>(
                () => { new TestImageExifReader(this.InvalidFilePng); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreate_Successful()
        {
            ExifReader r = TestImageExifReader.Create(this.TestImageFileName);

            Assert.IsInstanceOf<ImageExifReader>(r);
        }

        [Test]
        public void CreateJpegReaderViaStaticCreateNonExistantFileThrowsException_Successful()
        {
            Assert.Throws<FileNotFoundException>(
                () => { TestImageExifReader.Create(this.NonExistantFile); }
                );
        }

        [Test]
        public void CreateJpegReaderViaStaticCreateInvalidFileThrowsException_Successful()
        {
            Assert.Throws<InvalidDataException>(
                () => { TestImageExifReader.Create(this.InvalidFilePng); }
                );
        }
    }
}
