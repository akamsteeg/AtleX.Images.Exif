﻿using AtleX.Images.Exif.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Tests
{
    public abstract class TestsBase
    {
        public abstract string TestImageFileName
        {
            get;
        }        

        public static Stream OpenAsStream(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return fs;
        }

        [Test]
        public void ReadExifFromImage_Successful()
        {
            ExifReader r = new ImageExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            Assert.IsNotNull(d);
        }

        [Test]
        public void ReadImageDimensions_Successful()
        {
            ExifReader r = new ImageExifReader(OpenAsStream(this.TestImageFileName));
            IEnumerable<ExifValue> d = r.GetExifData();

            ExifIntegerValue width = d.First(v => v.Field == ExifFieldType.ImageWidth) as ExifIntegerValue;
            ExifIntegerValue height = d.First(v => v.Field == ExifFieldType.ImageHeight) as ExifIntegerValue;

            Assert.IsNotNull(width);
            Assert.AreEqual(60, width.Value);

            Assert.IsNotNull(height);
            Assert.AreEqual(40, height.Value);
        }

        [Test]
        public void ReadExifTwiceFromTheSameImage_Successful()
        {
            ExifReader r = new ImageExifReader(OpenAsStream(this.TestImageFileName));
            
            IEnumerable<ExifValue> d1 = r.GetExifData();
            IEnumerable<ExifValue> d2 = r.GetExifData();

            Assert.AreEqual(d1.Count(), d2.Count());
        }
    }
}
