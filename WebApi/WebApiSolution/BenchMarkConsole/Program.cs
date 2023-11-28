using BenchmarkDotNet.Running;

namespace BenchMarkConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        BenchmarkRunner.Run<BenchMarkService>();
    }
}