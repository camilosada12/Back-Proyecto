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

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserSelectDto>()
                .ForMember(p => p.TypeDocument,
                o => o.MapFrom(S => S.documentType != null ? S.documentType.name : null));


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
            CreateMap<PaymentAgreement, PaymentAgreementSelectDto>()
                .ForMember(p => p.userInfractionName,
                o => o.MapFrom(S => S.userInfraction != null ? S.userInfraction.observations : null))
                .ForMember(p => p.paymentFrequencyName,
                o => o.MapFrom(S => S.paymentFrequency != null ? S.paymentFrequency.intervalPage : null))
                  .ForMember(d => d.TypeInfractionName,
                 o => o.MapFrom(s => s.userInfraction != null ? s.userInfraction.typeInfraction.type_Infraction : null));

            CreateMap<DocumentInfraction, DocumentInfractionDto>().ReverseMap();
            CreateMap<DocumentInfraction, DocumentInfractionSelectDto>()
             .ForMember(d => d.inspectoraReportName,
                 o => o.MapFrom(s => s.inspectoraReport != null ? s.inspectoraReport.message : null))
             .ForMember(d => d.PaymentAgreementName,
                 o => o.MapFrom(s => s.paymentAgreement != null ? s.paymentAgreement.AgreementDescription : null));


            CreateMap<InspectoraReport, InspectoraReportDto>().ReverseMap();
            CreateMap<InspectoraReport, InspectoraReportSelectDto>().ReverseMap();

            CreateMap<FineCalculationDetail, FineCalculationDetailDto>().ReverseMap();
            CreateMap<FineCalculationDetail, FineCalculationDetailSelectDto>()
                .ForMember(d => d.valueSmldvCalculation,
                    o => o.MapFrom(s => (double?)s.valueSmldv.value_smldv))
                  .ForMember(d => d.typeInfractionName,
                 o => o.MapFrom(s => s.typeInfraction != null ? s.typeInfraction.type_Infraction : null));


            CreateMap<TypeInfraction, TypeInfractionSelectDto>();
            CreateMap<TypeInfractionDto, TypeInfraction>();
            CreateMap<TypeInfraction, TypeInfractionDto>();

            CreateMap<FineCalculationDetailSelectDto, FineCalculationDetail>();

            CreateMap<UserNotification, UserNotificationDto>().ReverseMap();
            CreateMap<UserNotification, UserNotificationSelectDto>().ReverseMap();

            CreateMap<ValueSmldv, ValueSmldvSelectDto>();
            CreateMap<ValueSmldv, ValueSmldvDto>().ReverseMap();

            CreateMap<UserInfraction, UserInfractionSelectDto>()
                .ForMember(p => p.typeInfractionName,
                o => o.MapFrom(S => S.typeInfraction != null ? S.typeInfraction.type_Infraction : null))
                  .ForMember(d => d.firstName,
                 o => o.MapFrom(s => s.user != null ? s.user.Person.firstName : null))
                 .ForMember(d => d.lastName,
                     o => o.MapFrom(s => s.user != null ? s.user.Person.lastName : null));
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
