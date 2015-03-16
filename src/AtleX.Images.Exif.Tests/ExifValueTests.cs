﻿using NUnit.Framework;
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
        public void NullValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => { new ExifValue(ExifFieldType.Artist, null); });
        }

        [Test]
        public void ToStringWithFieldAndValue_Successful([Values("Alex", 0, 0.1, true)] object value)
        {
            ExifValue ev = new ExifValue(ExifFieldType.Artist, value);

            string expected = String.Format("Artist - {0}", value);

            Assert.AreEqual(expected, ev.ToString());
        }

        [Test, Sequential]
        public void StorePrimitiveObjectsAndGetValueAsString_Successful([Values("Alex", 0, 0.1, true)] object value)
        {
            ExifValue ev = new ExifValue(ExifFieldType.Artist, value);

            Assert.AreEqual(value.ToString(), ev.GetValue<string>());
        }

        [Test]
        public void StoreDateTimeAndGetValueAsString_Successful()
        {
            DateTime dt = new DateTime(1986, 7, 8, 19, 30, 0);

            ExifValue ev = new ExifValue(ExifFieldType.DateTime, dt);

            Assert.AreEqual(dt.ToString(), ev.GetValue<string>());
        }

        [Test]
        public void StoreDateTimeAndGetValueAsDateTime_Successful()
        {
            DateTime dt = new DateTime(1986, 7, 8, 19, 30, 0);

            ExifValue ev = new ExifValue(ExifFieldType.DateTime, dt);

            Assert.AreEqual(dt, ev.GetValue<DateTime>());
        }

        [Test]
        public void StoreInvalidStringAndGetValueAsDateTime_Throws()
        {
            ExifValue ev = new ExifValue(ExifFieldType.DateTime, "");

            Assert.Throws<InvalidCastException>(() => { ev.GetValue<DateTime>(); });
        }
    }
}
