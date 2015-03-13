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
    public class ByteConvertorTests
    {
        [Test]
        public void ConvertLittleAndBigEndianBytesToIntSameResult_Successful()
        {
            int testOutput1_1 = ByteConvertor.ConvertBytesToInt(new byte[] { 0, 8}, true);
            int testOutput1_2 = ByteConvertor.ConvertBytesToInt(new byte[] { 8, 0 }, false);

            int testOutput2_1 = ByteConvertor.ConvertBytesToInt(new byte[] { 1, 0 }, true);
            int testOutput2_2 = ByteConvertor.ConvertBytesToInt(new byte[] { 0, 1 }, false);

            int testOutput3_1 = ByteConvertor.ConvertBytesToInt(new byte[] { 255, 255 }, true);
            int testOutput3_2 = ByteConvertor.ConvertBytesToInt(new byte[] { 255, 255 }, false);

            int testOutput4_1 = ByteConvertor.ConvertBytesToInt(new byte[] { 8, 0, 0, 0 }, true);
            int testOutput4_2 = ByteConvertor.ConvertBytesToInt(new byte[] { 0, 0, 0, 8 }, false);

            Assert.AreEqual(2048, testOutput1_1);
            Assert.AreEqual(1, testOutput2_1);
            Assert.AreEqual(65535, testOutput3_1);
            Assert.AreEqual(8, testOutput4_1);

            Assert.AreEqual(testOutput1_1, testOutput1_2);
            Assert.AreEqual(testOutput2_1, testOutput2_2);
            Assert.AreEqual(testOutput3_1, testOutput3_2);
            Assert.AreEqual(testOutput4_1, testOutput4_2);
        }

        [Test]
        public void ConvertByteArrayToASCIIString_Successful()
        {
            byte[] input = new byte[] { 69, 120, 105, 102, 0 };

            string output = ByteConvertor.ConvertBytesToASCIIString(input);

            Assert.AreEqual("Exif", output);
        }
    }
}
