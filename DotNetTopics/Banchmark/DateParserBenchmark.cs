using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Banchmark
{
    [RPlotExporter, RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class DateParserBenchmark
    {
        private const string DateTime = "2019-12-13T16:33:06Z";

        private static readonly DataParser Parser = new DataParser();

        [Benchmark]
        public void GetYearFromDateTime()
        {
            Parser.GetYearFromDateTime(DateTime);
        }

        [Benchmark]
        public void GetYearFromDateTime2()
        {
            Parser.GetYearFromDateTime2(DateTime);
        }
    }
}
