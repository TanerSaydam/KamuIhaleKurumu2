using DDD.Domain.Abstractions;

namespace DDD.Domain.Products;

public sealed class Product : Entity
{
    private Product(Guid id) : base(id)
    {
    }

    public Product(
        Guid id,
        string name,
        int quantity,
        Money price) : base(id)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
}

public sealed record Currency
{
    public static readonly Currency Usd = new("Usd");
    public static readonly Currency TRY = new("TRY");
    public string Code { get; init; }

    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        //işlemleri yap geriye currency
        return All.FirstOrDefault(x => x.Code == code) ??
            throw new ArgumentException("Kullandığınız para birimi desteklenmiyor!");
    }

    public static readonly IReadOnlyCollection<Currency> All =
        new[] { Usd, TRY };
}

public sealed record Money
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public Money(decimal amount, Currency currency)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Rakam 1 dan düşük olamaz!");
        }

        Amount = amount;
        Currency = currency;
    }
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new ArgumentException("Para formatları aynı olmayan değeler toplanamaz!");
        }

        return new Money(first.Amount + second.Amount, first.Currency);
    }
}