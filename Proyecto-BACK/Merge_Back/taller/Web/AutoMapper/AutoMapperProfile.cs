using AutoMapper;
using Entity.Domain.Models;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.Domain.Models.Implements.parameters;
using Entity.DTOs.Default.Auth;
using Entity.DTOs.Default.Me;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Default.parameters;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.DTOs.Select;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using FormDto = Entity.DTOs.Default.ModelSecurityDto.FormDto;
using RolUserDto = Entity.DTOs.Default.ModelSecurityDto.RolUserDto;

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


            //CreateMap<RegisterRequestDto, Person>()
            //    .ForMember(d => d.firstName, o => o.MapFrom(s => NameSplitter.Split(s.NombreCompleto).firstName))
            //    .ForMember(d => d.lastName, o => o.MapFrom(s => NameSplitter.Split(s.NombreCompleto).lastName));


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

            CreateMap<FineCalculationDetail, FineCalculationDetailSelectDto>()
                .ForMember(dest => dest.valueSmldvValue, opt => opt.MapFrom(src => src.valueSmldv.value_smldv))
                .ForMember(dest => dest.currentYear, opt => opt.MapFrom(src => src.valueSmldv.Current_Year))
                .ForMember(dest => dest.minimunWage, opt => opt.MapFrom(src => src.valueSmldv.minimunWage))

                .ForMember(dest => dest.TypeInfractionName, opt => opt.MapFrom(src => src.typeInfraction.type_Infraction))
                .ForMember(dest => dest.numerSmldv, opt => opt.MapFrom(src => src.typeInfraction.numer_smldv))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.typeInfraction.description))
                .ReverseMap();



            CreateMap<TypeInfraction, TypeInfractionSelectDto>();
            CreateMap<TypeInfraction, TypeInfractionDto>().ReverseMap();


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

            CreateMap<User, UserMeDto>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.id))
                .ForMember(d => d.email, o => o.MapFrom(s => s.email))
                .ForMember(d => d.firstName, o => o.MapFrom(s => s.Person.firstName))
                .ForMember(d => d.lastName, o => o.MapFrom(s => s.Person.lastName))
                .ForMember(d => d.fullName, o => o.MapFrom(s => s.Person.firstName + " " + s.Person.lastName))
                .ForMember(d => d.roles, o => o.MapFrom(s => s.rolUsers.Select(r => r.Rol.name)))
                .ForMember(d => d.permissions, o => o.Ignore()) 
                .ForMember(d => d.Menu, o => o.Ignore());        

            // Registro
            CreateMap<RegisterDto, User>()
                .ForMember(d => d.email, o => o.MapFrom(s => s.email))
                .ForMember(d => d.PasswordHash, o => o.Ignore())
                //.ForMember(d => d.name, o => o.MapFrom(s => s.email))
                .ForMember(d => d.Person, o => o.Ignore());

            CreateMap<RegisterDto, Person>()
                .ForMember(d => d.firstName, o => o.MapFrom(s => s.firstName))
                .ForMember(d => d.lastName, o => o.MapFrom(s => s.lastName))
                .ForMember(d => d.address, o => o.MapFrom(s => s.address))
                .ForMember(d => d.phoneNumber, o => o.MapFrom(s => s.phone))
                //.ForMember(d => d.documentTypeId, o => o.MapFrom(s => s.documentTypeId))
                .ForMember(d => d.municipalityId, o => o.MapFrom(s => s.municipalityId));





        }

    }
}
