using Business.ExternalServices.Recaptcha;
using Business.Interfaces.IBusinessImplements;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.IBusinessImplements.parameters;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Mensajeria.Email.@interface;
using Business.Mensajeria.Email.implements;
using Business.Services.Entities;
using Business.Services.parameters;
using Business.Services.Security;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.Entities;
using Data.Interfaces.IDataImplement.parameters;
using Data.Interfaces.IDataImplement.Security;
using Data.Repositoy;
using Data.Services.Entities;
using Data.Services.Security;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Implements.Recaptcha;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.AutoMapper;
using Web.Workers;

namespace Web.Service
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 0) Infra básica
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<Utilities.Custom.EncriptePassword>();

            // 2) Persistencia genérica
            services.AddScoped(typeof(IData<>), typeof(DataGeneric<>));

            // 3) Repositorios — PARAMETERS
            services.AddScoped<ImunicipalityRepository, municipalityRepository>();

            // 3) Repositorios — SECURITY
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, personRepository>();
            services.AddScoped<IRolUserRepository, RolUserRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
            services.AddScoped<IRolFormPermissionRepository, RolFormPermissionRepository>();

            // 3) Repositorios — ENTITIES
            services.AddScoped<IDocumentInfractionRepository, DocumentInfractionRepository>();
            services.AddScoped<IPaymentAgreementRepository, PaymentAgreementRepository>();
            services.AddScoped<IInspectoraReportRepository, InspectoraReportRepository>();
            services.AddScoped<IValueSmldvRepository, ValueSmldvRepository>();
            services.AddScoped<IFineCalculationDetailRepository, FineCalculationDetailsRepository>();
            services.AddScoped<ITypeInfractionRepository, TypeInfractionRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IValueSmldvRepository, ValueSmldvRepository>();
            services.AddScoped<IUserInfractionRepository, UserInfractionRepository>();

            // 4) Servicios — PARAMETERS
            services.AddScoped<IdepartmentServices, departmentServices>();
            services.AddScoped<ImunicipalityServices, municipalityServices>();
            services.AddScoped<IdocumentTypeServices, documentTypeServices>();
            services.AddScoped<IPaymentFrequencyServices, PaymentFrequencyServices>();
            services.AddScoped<ITypePaymentServices, TypePaymentServices>();

            // 4) Servicios — SECURITY
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolUserService, RolUserService>();
            services.AddScoped<IFormModuleService, FormModuleService>();
            services.AddScoped<IRolFormPermissionService, RolFormPermissionService>();

            // 4) Servicios — ENTITIES
            services.AddScoped<IDocumentInfractionServices, DocumentInfractionServices>();
            services.AddScoped<IInspectoraReportService, InspectoraReportService>();
            services.AddScoped<IPaymentAgreementServices, PaymentAgreementServices>();
            services.AddScoped<ITypeInfractionService, TypeInfractionService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IUserInfractionServices, UserInfractionServices>();
            services.AddScoped<IFineCalculationDetailService, FineCalculationDetailService>();
            services.AddScoped<IValueSmldvService, ValueSmldvService>();

            // Recaptcha
            services.Configure<RecaptchaOptions>(configuration.GetSection("Recaptcha"));
            services.AddHttpClient<IRecaptchaVerifier, RecaptchaVerifier>();

            // Cookies / sesiones (si aplica)
            services.AddScoped<IAuthSessionRepository, AuthSessionRepository>();
            services.AddScoped<IAuthSessionService, AuthSessionService>();
            services.AddDistributedMemoryCache();

            // Email + Job mensual
            services.AddScoped<IServiceEmail, ServiceEmail>();
            services.AddScoped<MonthlyEmailAppService>();
            services.AddHostedService<MonthlyEmailWorker>(); // <- HOSTED SERVICE (clave)

            return services;
        }
    }
}
