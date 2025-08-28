using AutoMapper;
using Entity.Domain.Models;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.Domain.Models.Implements.parameters;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Default.parameters;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.DTOs.Select;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.NameSplitter;

namespace Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rol, RolSelectDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();

            CreateMap<User, UserSelectDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


            CreateMap<RolUser, RolUserDto>().ReverseMap();
            CreateMap<RolUser, RolUserSelectDto>().ReverseMap();

            CreateMap<Form, FormSelectDto>().ReverseMap();
            CreateMap<Form, FormDto>().ReverseMap();


            CreateMap<Module, ModuleSelectDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();


            CreateMap<Permission, PermissionSelectDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();


            CreateMap<Person, PersonSelectDto>().ReverseMap();
            // Automapper en tu Profile (asegura nombres)
            CreateMap<PersonDto, Person>()
                .ForMember(d => d.firstName, o => o.MapFrom(s => s.firstName))
                .ForMember(d => d.lastName, o => o.MapFrom(s => s.lastName))
                .ReverseMap();


            CreateMap<RegisterRequestDto, Person>()
                .ForMember(d => d.firstName, o => o.MapFrom(s => NameSplitter.Split(s.NombreCompleto).firstName))
                .ForMember(d => d.lastName, o => o.MapFrom(s => NameSplitter.Split(s.NombreCompleto).lastName));


            CreateMap<RegisterRequestDto, User>()
                .ForMember(d => d.email, o => o.MapFrom(s => s.email))    // Gmail como login
                .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.password));


            CreateMap<FormModule, FormModuleSelectDto>().ReverseMap();
            CreateMap<FormModule, FormModuleDto>().ReverseMap();

            CreateMap<RolFormPermission, RolFormPermissionDto>().ReverseMap();
            CreateMap<RolFormPermission, RolFormPermissionSelectDto>().ReverseMap();


            //Entities

            CreateMap<TypePayment, TypePaymentDto>().ReverseMap();
            CreateMap<TypePayment, TypePaymentSelectDto>().ReverseMap();

            CreateMap<PaymentAgreement, PaymentAgreementDto>().ReverseMap();
            CreateMap<PaymentAgreement, PaymentAgreementSelectDto>().ReverseMap();

            CreateMap<DocumentInfraction, DocumentInfractionDto>().ReverseMap();
            CreateMap<DocumentInfraction, DocumentInfractionSelectDto>().ReverseMap();

            CreateMap<InspectoraReport, InspectoraReportDto>().ReverseMap();
            CreateMap<InspectoraReport, InspectoraReportSelectDto>().ReverseMap();

            CreateMap<FineCalculationDetail, FineCalculationDetailDto>().ReverseMap();
            CreateMap<FineCalculationDetail, FineCalculationDetailSelectDto>().ReverseMap();


            CreateMap<TypeInfraction, TypeInfractionSelectDto>();
            CreateMap<TypeInfractionDto, TypeInfraction>();
            CreateMap<TypeInfraction, TypeInfractionDto>();

            CreateMap<FineCalculationDetailSelectDto, FineCalculationDetail>();

            CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
            CreateMap<UserNotification, UserNotificationSelectDto>().ReverseMap();

            CreateMap<ValueSmldv, ValueSmldvSelectDto>();
            CreateMap<ValueSmldv, ValueSmldvDto>().ReverseMap();

            CreateMap<UserInfraction, UserInfractionSelectDto>();
            CreateMap<UserInfraction, UserInfractionDto>().ReverseMap();


            //parameters 

            CreateMap<department, departmentDto>().ReverseMap();
            CreateMap<department, departmentSelectDto>().ReverseMap();

            CreateMap<municipality, municipalityDto>().ReverseMap();
            CreateMap<municipality, municipalitySelectDto>().ReverseMap();

            CreateMap<PaymentFrequency, PaymentFrequencyDto>().ReverseMap();
            CreateMap<PaymentFrequency, PaymentFrequencySelectDto>().ReverseMap();

            CreateMap<documentType, documentTypeDto>().ReverseMap();
            CreateMap<documentType, documentTypeSelectDto>().ReverseMap();

            //CreateMap<TouristicAttraction, TouristicAttractionApiDto>().ReverseMap();
        }

    }
}
