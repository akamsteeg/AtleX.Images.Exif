using AtleX.Images.Exif.Benchmarks.Benchmarks.Helpers;
using AtleX.Images.Exif.Benchmarks.Benchmarks.Readers.Jpeg;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace AtleX.Images.Exif.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfig();
            var benchmarks = GetBenchmarks();

            for (var i = 0; i < benchmarks.Length; i++)
            {
                var typeToRun = benchmarks[i];
                BenchmarkRunner.Run(typeToRun, config);
            }

            //BenchmarkRunner.Run<JpegExifReaderBenchmarks>(config);
        }

        private static IConfig GetConfig()
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            var gcDiagnoser = new MemoryDiagnoser();
            config.Add(new Job { Mode = Mode.Throughput, LaunchCount = 2, WarmupCount = 2, TargetCount = 10 });
            config.Add(gcDiagnoser);

            return config;
        }

        private static Type[] GetBenchmarks()
        {
            var result = new Type[]
            {
              // Helpers
              typeof(FileTypeHelperBenchmarks),
              typeof(ByteConvertorBenchmarks),
              // Readers
              typeof(JpegExifReaderBenchmarks),
            };

            return result;
        }
    }
}
