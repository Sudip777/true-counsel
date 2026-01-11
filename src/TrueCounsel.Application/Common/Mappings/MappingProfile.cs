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
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Common.Mappings
{
    /// <summary> automapper profiles congifuration </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<RegisterAuthCommand, User>(MemberList.Source)
                .ForMember(d => d.PasswordHash, opt => opt.Ignore())
                .ForSourceMember(s => s.Password, opt => opt.DoNotValidate()); 
            
            CreateMap<User, UserDto>();

            // Lawyer
            CreateMap<RegisterLawyerCommand, Lawyer>(MemberList.Source);
            CreateMap<UpdateLawyerCommand, Lawyer>(MemberList.Source);
            CreateMap<Lawyer, LawyerDto>();

            // Client
            CreateMap<CreateClientCommand, Client>(MemberList.Source);
            CreateMap<UpdateClientCommand, Client>(MemberList.Source);
            CreateMap<Client, ClientDto>();

            // CaseType
            CreateMap<CreateCaseTypeCommand, CaseType>(MemberList.Source);
            CreateMap<CaseType, CaseTypeDto>();

            // CaseCategory
            CreateMap<CreateCaseCategoryCommand, CaseCategory>(MemberList.Source);
            CreateMap<CaseCategory, CaseCategoryDto>();

            // CaseParty
            CreateMap<CreateCasePartyCommand, CaseParty>(MemberList.Source);
            CreateMap<UpdateCasePartyCommand, CaseParty>(MemberList.Source);
            CreateMap<CaseParty, CasePartyDto>();

            // Court
            CreateMap<Court, CourtDto>()
                .ForMember(d => d.CourtTypeId, opt => opt.MapFrom(s => s.CourtTypeField.Id))
                .ForMember(d => d.CourtTypeName, opt => opt.MapFrom(s => s.CourtTypeField.Name));


            // LegalCase
            CreateMap<CreateLegalCaseCommand, LegalCase>(MemberList.Source);
            CreateMap<UpdateLegalCaseCommand, LegalCase>(MemberList.Source);
            CreateMap<LegalCase, LegalCaseDto>();

            // CaseNote
            CreateMap<CreateCaseNoteCommand, CaseNote>(MemberList.Source);
            CreateMap<CaseNote, CaseNoteDto>();
        }
    }
}
