using AutoMapper;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Common.Mappings
{
    /// <summary> automapper profiles congifuration </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<RegisterAuthCommand, User>()
                .ForMember(d => d.PasswordHash, opt => opt.Ignore()); 
            
            CreateMap<User, UserDto>();

            // Lawyer
            CreateMap<RegisterLawyerCommand, TrueCounsel.Domain.Entities.Lawyer>();
            CreateMap<UpdateLawyerCommand, TrueCounsel.Domain.Entities.Lawyer>();

            CreateMap<TrueCounsel.Domain.Entities.Lawyer, LawyerDto>();
        }
    }
}
