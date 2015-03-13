using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests
{
    [TestFixture]
    public class ExifValueTests
    {
        [Test]
        public void NullValueCausesException()
        {
            Assert.Throws<ArgumentNullException>(() => { new ExifValue(ExifFieldType.Artist, null); });
        }

        [Test]
        public void ToStringWithFieldAndValue([Values("Alex", 0, 0.1, true)] object value)
        {
            ExifValue ev = new ExifValue(ExifFieldType.Artist, value);

            string expected = String.Format("Artist - {0}", value);

            Assert.AreEqual(expected, ev.ToString());
        }

        [Test, Sequential]
        public void StorePrimitiveObjectsAndGetValueAsString([Values("Alex", 0, 0.1, true)] object value)
        {
            ExifValue ev = new ExifValue(ExifFieldType.Artist, value);

            Assert.AreEqual(value.ToString(), ev.GetValue<string>());
        }
    }
}
