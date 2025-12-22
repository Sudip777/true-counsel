using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Domain.Entities;

public class RegisterAuthCommandHandler : ICommandHandler<RegisterAuthCommand, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public RegisterAuthCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    /// <summary> handl regstration logic </summary>
    public async Task<UserDto> HandleAsync(
        RegisterAuthCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(command.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email is already in use.");

        // Map Command to Entity
        var newUser = _mapper.Map<User>(command);
        
        // Manual steps not covered by automapper (or ignored)
        newUser.PasswordHash = _passwordHasher.HashPassword(command.Password);
        newUser.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<UserDto>(newUser);
    }
}