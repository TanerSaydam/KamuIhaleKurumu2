using BenchmarkDotNet.Attributes;

namespace DDD.TestConsoleApp;

public class BenchMarkService
{
    public A obj1;
    public A obj2;

    public B objB1;
    public B objB2;

    [Benchmark(Baseline = true)]
    public void EqualsMethod()
    {
        obj1 = new();
        obj2 = new();

        obj1.Id = 1;
        obj2.Id = 1;

        obj1.Equals(obj2);
    }

    [Benchmark]
    public void IEquatableEquals()
    {
        objB1 = new();
        objB2 = new();

        objB1.Id = 1;
        objB2.Id = 1;

        objB1.Equals(objB2);
    }
}

public class A : Entity
{
    public override bool Equals(object? obj)
    {
        if (obj is not null && obj is Entity entity)
        {
            return entity.Id == Id;
        }

        return false;
    }
}

public class B : Entity, IEquatable<Entity>
{
    public override bool Equals(object? obj)
    {
        if (obj is not null && obj is Entity entity)
        {
            return entity.Id == Id;
        }

        return false;
    }

    public bool Equals(Entity? other)
    {
        if (other is not null && other is Entity entity)
        {
            return entity.Id == Id;
        }

        return false;
    }
}