namespace PersonelServer.Domain.Users;
public sealed record UserResponseDto(
    string Name,
    string Lastname,
    string Email,
    string PhoneNumber,
    string Country,
    string City,
    string PostalCode,
    string FullAddress);
