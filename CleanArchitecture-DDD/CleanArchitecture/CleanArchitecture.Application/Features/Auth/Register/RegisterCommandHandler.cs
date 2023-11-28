using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.AnyAsync(p => p.UserName == request.UserName, cancellationToken))
        {
            throw new ArgumentException("Bu kullanıcı daha önce kullanılmış!");
        }

        User user = new()
        {
            Name = request.Name,
            Password = request.Password,
            UserName = request.UserName
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}