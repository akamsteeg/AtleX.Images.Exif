using AtleX.Images.Exif.Helpers;
using BenchmarkDotNet.Attributes;

namespace AtleX.Images.Exif.Benchmarks.Benchmarks.Helpers
{
    public class ByteConvertorBenchmarks
    {
        [Benchmark]
        public int Convert2BytesToIntLittleEndian()
        {
            var result = ByteConvertor.ConvertBytesToInt(new byte[] { 0, 8 }, true);

            return result;
        }

        [Benchmark]
        public int Convert2BytesToIntBigEndian()
        {
            var result = ByteConvertor.ConvertBytesToInt(new byte[] { 8, 0 }, false);

            return result;
        }

        [Benchmark]
        public int Convert4BytesToIntLittleEndian()
        {
            var result = ByteConvertor.ConvertBytesToInt(new byte[] { 8, 0, 0, 0 }, true);

            return result;
        }

        [Benchmark]
        public int Convert4BytesToIntBigEndian()
        {
            var result = ByteConvertor.ConvertBytesToInt(new byte[] { 0, 0, 0, 8 }, false);

            return result;
        }
    }
}
