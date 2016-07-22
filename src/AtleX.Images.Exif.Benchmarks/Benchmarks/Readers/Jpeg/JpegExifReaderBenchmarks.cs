using AtleX.Images.Exif.Readers.Jpeg;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace AtleX.Images.Exif.Benchmarks.Benchmarks.Readers.Jpeg
{
    public class JpegExifReaderBenchmarks
    {
        private string TestImageFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Testfiles\Jpeg\Canon_7D\1_LittleEndian.jpg");

            }
        }

        [Benchmark]
        public IEnumerable<ExifValue> ReadExif()
        {
            using (var fs = new FileStream(this.TestImageFileName, FileMode.Open, FileAccess.Read))
            {
                var exifReader = new JpegExifReader(fs);

                var result = exifReader.GetExifData();

                return result;
            }
        }
    }
}
