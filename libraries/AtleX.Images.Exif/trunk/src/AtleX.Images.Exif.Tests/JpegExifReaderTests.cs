using AtleX.Images.Exif;
using AtleX.Images.Exif.Data;
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
        public override string TestImageFileName
        {
            get
            {
                //return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6701.jpg";
                return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6573.jpg";
                //return @"..\..\..\..\Testfiles\Jpeg\Nikon_D3100\mattus82_10709867984.jpg";
            }
        }

        public string JpegWithWrongExtension
        {
            get
            {
                return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\jpegwithwrongextension.gif";
            }
        }

        [Test]
        public void CreateJpegReader()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            Assert.IsTrue(r is JpegExifReader);
        }

        [Test]
        public void CreateReaderAndLoadCorrectlyLoadJpegWithWrongExtension()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.JpegWithWrongExtension));
            IEnumerable<ExifValue> d = r.GetExifData();

            Assert.IsNotNull(d);
        }

        [Test]
        public void CreateReaderAndLoadInvalidFile()
        {
            Assert.Throws<InvalidDataException>(
                () => {  new JpegExifReader(OpenAsStream(this.InvalidFilePng)); }
                );
        }

        [Test]
        public void ReadExifFromJpeg()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            Assert.IsNotNull(d);
        }
    }
}
