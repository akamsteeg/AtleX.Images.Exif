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

namespace AtleX.Images.Exif.Tests.Readers
{
    [TestFixture]
    public class JpegExifReaderTests : ExifReaderTestsBase
    {
        public override string TestImageFileName
        {
            get
            {
                //return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\1_LittleEndian.jpg";
                //return @"..\..\..\..\Testfiles\Jpeg\Canon_7D\1_BigEndian.jpg";
                return @"..\..\..\..\Testfiles\Jpeg\Nikon_D3100\mattus82_10709867984.jpg";
                //return @"..\..\..\..\Testfiles\Jpeg\Motorola_MotoG\2015-03-07 18.36.58.jpg";
            }
        }

        [Test]
        public void CreateJpegReader_Successful()
        {
            ExifReader r = new JpegExifReader(OpenAsStream(this.TestImageFileName));
            Assert.IsTrue(r is JpegExifReader);
        }
    }
}
