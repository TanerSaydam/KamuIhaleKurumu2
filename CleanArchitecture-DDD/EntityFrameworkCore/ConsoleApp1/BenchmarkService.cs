using BenchmarkDotNet.Attributes;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public class BenchmarkService
{
    private ApplicationDbContext context = new();

    [Benchmark(Baseline = true)]
    public void AsSplitQuery()
    {
        context.Categories.Include(p => p.Products).AsSplitQuery().ToList();
    }

    [Benchmark]
    public void NoAsSplitQuery()
    {
        context.Categories.Include(p => p.Products).ToList();
    }
}