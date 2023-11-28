```

BenchmarkDotNet v0.13.7, Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 7.0.400
  [Host]   : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2
  ShortRun : .NET 7.0.10 (7.0.1023.36312), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|               Method |      Mean |       Error |    StdDev |         Ratio | RatioSD |
|--------------------- |----------:|------------:|----------:|--------------:|--------:|
|     NoTrackingToList |  14.52 ms |    11.22 ms |  0.615 ms |      baseline |         |
| ElasticSearchAllList | 538.79 ms | 1,757.17 ms | 96.317 ms | 37.30x slower |   8.03x |
