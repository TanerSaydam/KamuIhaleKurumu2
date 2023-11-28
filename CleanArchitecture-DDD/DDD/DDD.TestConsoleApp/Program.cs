using BenchmarkDotNet.Running;
using DDD.TestConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        //Console.WriteLine("Hello!");

        //Product product1 = new() { Id = 1, Name = "Domates" };
        //Product product2 = new() { Id = 1, Name = "Salatalık" };

        //Console.WriteLine(product1.Equals(product2));

        BenchmarkRunner.Run<BenchMarkService>();
        Console.ReadLine();
    }
}

public class Entity
{
    public int Id { get; set; }
}

public class Product : Entity
{
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }
}