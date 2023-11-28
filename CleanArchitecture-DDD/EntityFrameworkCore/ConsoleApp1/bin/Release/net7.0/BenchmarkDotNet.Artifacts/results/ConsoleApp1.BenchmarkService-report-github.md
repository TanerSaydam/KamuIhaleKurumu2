```

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2428/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 7.0.403
  [Host]     : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.13 (7.0.1323.51816), X64 RyuJIT AVX2


```
| Method         | Mean     | Error   | StdDev   | Ratio | RatioSD |
|--------------- |---------:|--------:|---------:|------:|--------:|
| AsSplitQuery   | 434.3 μs | 8.66 μs | 10.31 μs |  1.00 |    0.00 |
| NoAsSplitQuery | 448.3 μs | 8.77 μs | 11.40 μs |  1.03 |    0.04 |
