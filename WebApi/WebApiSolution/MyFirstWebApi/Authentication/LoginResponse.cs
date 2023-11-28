using MyFirstWebApi.Entities;

namespace MyFirstWebApi.Authentication;

public sealed record LoginResponse(
    string Token,
    string UserId,
    List<Role> Roles,
    string RefreshToken,
    DateTime RefreshTokenExpires);
