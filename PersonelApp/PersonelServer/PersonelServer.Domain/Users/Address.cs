namespace PersonelServer.Domain.Users;

public sealed record Address(
    string Country,
    string City,
    string PostalCode,
    string FullAddress);
