// RegisterAuthCommandHandler.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Domain.Entities;

public class RegisterAuthCommandHandler : ICommandHandler<RegisterAuthCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterAuthCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> HandleAsync(
        RegisterAuthCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email is already in use.");

        var newUser = new User
        {
            Name = command.Name,
            Email = command.Email,
            PasswordHash = _passwordHasher.HashPassword(command.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();

        return newUser;
    }
}