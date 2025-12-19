using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Common.Mappings
{
    public static class UserMapaping
    {
        //  command → entity (explicit, injectable deps)
        public static User ToUser(this RegisterAuthCommand command, IPasswordHasher passwordHasher)
        {
            return new()
            {
                Name = command!.Name,
                Email = command.Email,
                PasswordHash = passwordHasher.HashPassword(command.Password),
                Role = Domain.Enums.UserRole.Client,
                IsActive = true,
            };
        }

        //DTO → User
        public static User ToUser(this RegisterAuthRequestDto dto, IPasswordHasher passwordHasher)
        {
            return new()
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHasher.HashPassword(dto.Password),
                Role = Domain.Enums.UserRole.Client,
                IsActive = true
            };
        }
    }
}
