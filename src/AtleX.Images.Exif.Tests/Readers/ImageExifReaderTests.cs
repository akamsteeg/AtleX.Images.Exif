﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtleX.Images.Exif.Readers.Jpeg;
using AtleX.Images.Exif.Tests.Readers;
using NUnit.Framework;
using System.IO;

namespace AtleX.Images.Exif.Tests.Readers
{
    [TestFixture]
    public class ImageExifReaderTests
    {
        public string TestImageFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Testfiles\Jpeg\Canon_7D\1_LittleEndian.jpg");

            }
        }

        public string InvalidFilePng
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Testfiles\Invalid\invalid.png");
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
        public void CreateJpegReaderNonExistantFile_Throws()
        {
            Assert.Throws<FileNotFoundException>(
                () => { new TestImageExifReader(this.NonExistantFile); } 
                );
        }

        [Test]
        public void CreateJpegReaderInvalidFile_Throws()
        {
            Assert.Throws<InvalidDataException>(
                () => { new TestImageExifReader(this.InvalidFilePng); }
                );
        }
    }
}
