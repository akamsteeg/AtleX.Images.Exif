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
                return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\IMG_6701.jpg";
            }
        }

        [Test]
        public void CreateJpegReader()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            Assert.IsTrue(r is JpegExifReader);
        }

        [Test]
        public void ReadExifFromJpeg()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            Assert.IsNotNull(d);
        }

        public void ReadImageWidth()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            ExifIntegerValue width = d.First(v => v.Field == ExifFieldType.ImageWidth) as ExifIntegerValue;

            Assert.IsNotNull(width);
            Assert.AreEqual(60, width.Value);
        }
    }
}
