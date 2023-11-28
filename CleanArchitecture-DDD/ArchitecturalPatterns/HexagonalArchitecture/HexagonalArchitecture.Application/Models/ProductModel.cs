namespace HexagonalArchitecture.Application.Models;

public sealed class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public void UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price 0 dan küçük olamaz!");
        }
        Price = price;
    }
}