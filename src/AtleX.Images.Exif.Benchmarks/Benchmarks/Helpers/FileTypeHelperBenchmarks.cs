using AtleX.Images.Exif.Helpers;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Images.Exif.Benchmarks.Benchmarks.Helpers
{
    public class FileTypeHelperBenchmarks
    {
        private string TestImageFileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Testfiles\Jpeg\Canon_7D\1_LittleEndian.jpg");

            }
        }

        [Benchmark]
        public bool ReadFromFile()
        {
            var fileType = FileTypeHelper.DetermineFileType(this.TestImageFileName);

            var result = (fileType == ImageFileType.Jpeg);

            return result;
        }

        [Benchmark]
        public bool ReadFromStream()
        {
            using (var fs = new FileStream(this.TestImageFileName, FileMode.Open, FileAccess.Read))
            {
                var fileType = FileTypeHelper.DetermineFileType(fs);

                return (fileType == ImageFileType.Jpeg);
            }
        }
    }
}
