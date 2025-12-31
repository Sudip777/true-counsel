using AutoMapper;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Application.Features.CaseCategory.Commands;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using TrueCounsel.Application.Features.CaseType.Commands;
using TrueCounsel.Application.Features.CaseType.Dtos;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Dtos;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Dtos;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Dtos;
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
            CreateMap<RegisterLawyerCommand, Lawyer>();
            CreateMap<UpdateLawyerCommand, Lawyer>();
            CreateMap<Lawyer, LawyerDto>();

            // Client
            CreateMap<CreateClientCommand, Client>();
            CreateMap<UpdateClientCommand, Client>();
            CreateMap<Client, ClientDto>();

            // CaseType
            CreateMap<CreateCaseTypeCommand, CaseType>();
            CreateMap<CaseType, CaseTypeDto>();

            // CaseCategory
            CreateMap<CreateCaseCategoryCommand, CaseCategory>();
            CreateMap<CaseCategory, CaseCategoryDto>();

            // CaseParty
            CreateMap<CreateCasePartyCommand, CaseParty>();
            CreateMap<UpdateCasePartyCommand, CaseParty>();
            CreateMap<CaseParty, CasePartyDto>();

            // Court
            CreateMap<Court, CourtDto>();

            // CaseParty
            CreateMap<CreateCasePartyCommand, CaseParty>();
            CreateMap<UpdateCasePartyCommand, CaseParty>();
            CreateMap<CaseParty, CasePartyDto>();
            CreateMap<CaseParty, CasePartyDto>();

            // LegalCase
            CreateMap<CreateLegalCaseCommand, LegalCase>();
            CreateMap<UpdateLegalCaseCommand, LegalCase>();
            CreateMap<LegalCase, LegalCaseDto>();
        }
    }
}
