using AtleX.Images.Exif.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests.Helpers
{
    [TestFixture]
    public class ExifDataTypeConvertorTests
    {
        #region ToByte()

        [Test]
        public void ToByte_Successful()
        {
            byte[] data = new byte[1] { 100 };

            byte result = ExifDataTypeConvertor.ToByte(data);

            Assert.AreEqual(100, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayToByte_Throws()
        {
            byte[] data = null;

            byte result = ExifDataTypeConvertor.ToByte(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyArrayToByte_Throws()
        {
            byte[] data = new byte[0] { };

            byte result = ExifDataTypeConvertor.ToByte(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLargeArrayToByte_Throws()
        {
            byte[] data = new byte[2] { 100, 100 };

            byte result = ExifDataTypeConvertor.ToByte(data);
        } 

        #endregion

        #region ToASCII()
        [Test]
        public void ToASCII_Successful()
        {
            byte[] data = new byte[5] { 0x45, 0x78, 0x69, 0x66, 0 }; //EXIF\0

            string result = ExifDataTypeConvertor.ToASCII(data);

            Assert.AreEqual("Exif", result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayToASCII_Throws()
        {
            byte[] data = null;

            string result = ExifDataTypeConvertor.ToASCII(data);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyArrayToASCII_Throws()
        {
            byte[] data = new byte[0] { };

            string result = ExifDataTypeConvertor.ToASCII(data);
        }

        #endregion

        #region ToShort()

        [Test]
        public void ToShort_Successful()
        {
            byte[] data = new byte[2] { 50, 100 };

            int resultLittleEndian = ExifDataTypeConvertor.ToShort(data, true);
            int resultBigEndian = ExifDataTypeConvertor.ToShort(data, false);

            Assert.AreEqual(25650, resultLittleEndian);
            Assert.AreEqual(12900, resultBigEndian);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArrayToShort_Throws()
        {
            byte[] data = null;

            int result = ExifDataTypeConvertor.ToShort(data, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyArrayToShort_Throws()
        {
            byte[] data = new byte[0] { };

            int result = ExifDataTypeConvertor.ToShort(data, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToShortArrayToShort_Throws()
        {
            byte[] data = new byte[1] { 0 };

            int result = ExifDataTypeConvertor.ToShort(data, false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLargeArrayToShort_Throws()
        {
            byte[] data = new byte[3] { 0, 0, 0 };

            int result = ExifDataTypeConvertor.ToShort(data, false);
        }

        #endregion

        #region ToRational



        #endregion
    }
}
