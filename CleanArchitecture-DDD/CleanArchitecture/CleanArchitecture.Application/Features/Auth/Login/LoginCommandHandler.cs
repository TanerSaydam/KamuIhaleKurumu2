using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user =
            await _userRepository
            .GetWhere(p => p.UserName == request.UserName)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            throw new ArgumentException("Kullanıcı kaydı bulunamadı!");
        }

        if (user.Password != request.Password)
        {
            throw new ArgumentException("Şifreniz yanlış!");
        }

        string token = "Token";
        return token;
    }
}