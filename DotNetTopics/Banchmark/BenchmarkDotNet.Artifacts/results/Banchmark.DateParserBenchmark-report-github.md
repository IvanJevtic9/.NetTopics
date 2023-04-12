``` ini

BenchmarkDotNet=v0.13.4, OS=Windows 10 (10.0.19045.2486)
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.301
  [Host]     : .NET 6.0.6 (6.0.622.26707), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.6 (6.0.622.26707), X64 RyuJIT AVX2


```
|               Method |      Mean |    Error |   StdDev | Rank |   Gen0 | Allocated |
|--------------------- |----------:|---------:|---------:|-----:|-------:|----------:|
| GetYearFromDateTime2 |  81.21 ns | 1.378 ns | 1.289 ns |    1 | 0.0191 |     160 B |
|  GetYearFromDateTime | 348.29 ns | 6.854 ns | 7.334 ns |    2 |      - |         - |
