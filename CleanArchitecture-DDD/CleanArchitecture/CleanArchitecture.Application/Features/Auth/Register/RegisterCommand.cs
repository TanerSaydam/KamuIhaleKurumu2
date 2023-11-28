using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Register;
public sealed record RegisterCommand(
    string Name,
    string UserName,
    string Password) : IRequest;

//public sealed class RegisterCommand
//{
//    public RegisterCommand(string name, string userName, string password)
//    {
//        Name = name;
//        UserName = userName;
//        Password = password;
//    }

//    public string Name { get; init; }
//    public string UserName { get; init; }
//    public string Password { get; init; }
//}