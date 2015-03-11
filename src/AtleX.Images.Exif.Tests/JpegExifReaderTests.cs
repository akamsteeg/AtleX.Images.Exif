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
                //return @"..\..\..\..\Testfiles\Jpeg\Nikon_D3100\mattus82_10709867984.jpg";
                //return @"..\..\..\..\Testfiles\Jpeg\Motorola_MotoG\2015-03-07 18.36.58.jpg";
            }
        }

        [Test]
        public void CreateJpegReader_Successful()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            Assert.IsTrue(r is JpegExifReader);
        }

        [Test]
        public void ReadExifFromJpeg_Successful()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            Assert.IsNotNull(d);
        }

        [Test]
        public void ReadImageDimensions_Successful()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            ExifIntegerValue width = d.First(v => v.Field == ExifFieldType.ImageWidth) as ExifIntegerValue;
            ExifIntegerValue height = d.First(v => v.Field == ExifFieldType.ImageHeight) as ExifIntegerValue;

            Assert.IsNotNull(width);
            Assert.AreEqual(60, width.Value);

            Assert.IsNotNull(height);
            Assert.AreEqual(40, height.Value);
        }
    }
}
