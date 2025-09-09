using Business.Custom;
using Business.Interfaces.IBusinessImplements;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.IBusinessImplements.parameters;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Interfaces.IJWT;
using Business.Mensajeria;
using Business.Mensajeria.Implements;
using Business.Services.Entities;
using Business.Services.parameters;


//using Business.Mensajeria.Implements;
//using Business.Mensajeria.Interfaces;
//using Business.Messaging.Implements;
using Business.Services.Security;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.Entities;
using Data.Interfaces.IDataImplement.parameters;
using Data.Interfaces.IDataImplement.Security;
using Data.Interfaces.Security;
using Data.Repositoy;
using Data.Services.Entities;
using Data.Services.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Microsoft.AspNetCore.Identity;
using Utilities.Custom;
using Web.AutoMapper;
using Web.Infrastructure;

namespace Web.Service
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Infra
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<EncriptePassword>();
            services.AddMemoryCache();

            // Auditoría / logging
            //services.AddScoped<IAuditService, AuditService>();

            // Persistencia genérica
            services.AddScoped(typeof(IData<>), typeof(DataGeneric<>));

            // Repositorios — PARAMETERS
            services.AddScoped<ImunicipalityRepository, municipalityRepository>();

            // Repositorios — SECURITY
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, personRepository>();
            services.AddScoped<IRolUserRepository, RolUserRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
            services.AddScoped<IRolFormPermissionRepository, RolFormPermissionRepository>();

            // Repositorios — ENTITIES
            services.AddScoped<IDocumentInfractionRepository, DocumentInfractionRepository>();
            services.AddScoped<IPaymentAgreementRepository, PaymentAgreementRepository>();
            services.AddScoped<IInspectoraReportRepository, InspectoraReportRepository>();
            services.AddScoped<IValueSmldvRepository, ValueSmldvRepository>();
            services.AddScoped<IFineCalculationDetailRepository, FineCalculationDetailsRepository>();
            services.AddScoped<ITypeInfractionRepository, TypeInfractionRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IUserInfractionRepository, UserInfractionRepository>();

            // 🔐 Repositorio de refresh tokens (FALTABA)

            // Servicios — PARAMETERS
            services.AddScoped<IdepartmentServices, departmentServices>();
            services.AddScoped<ImunicipalityServices, municipalityServices>();
            services.AddScoped<IdocumentTypeServices, documentTypeServices>();
            services.AddScoped<IPaymentFrequencyServices, PaymentFrequencyServices>();
            services.AddScoped<ITypePaymentServices, TypePaymentServices>();

            // Servicios — SECURITY
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolUserService, RolUserService>();
            services.AddScoped<IFormModuleService, FormModuleService>();
            services.AddScoped<IRolFormPermissionService, RolFormPermissionService>();

            // Servicios — ENTITIES
            services.AddScoped<IDocumentInfractionServices, DocumentInfractionServices>();
            services.AddScoped<IInspectoraReportService, InspectoraReportService>();
            services.AddScoped<IPaymentAgreementServices, PaymentAgreementServices>();
            services.AddScoped<ITypeInfractionService, TypeInfractionService>();
            services.AddScoped<IValueSmldvService, ValueSmldvService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IFineCalculationDetailService, FineCalculationDetailService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IToken, TokenBusiness>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IServiceEmail, ServiceEmail>();
            services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserMeRepository, MeRepository>();
            services.AddScoped<IAuthCookieFactory, AuthCookieFactory>();








            return services;
        }
    }
}
