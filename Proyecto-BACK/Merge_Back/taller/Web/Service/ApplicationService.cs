using Business.ExternalServices.Recaptcha;
using Business.Interfaces.IBusinessImplements;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.IBusinessImplements.parameters;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Mensajeria;
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
using Data.Repositoy;
using Data.Services.Entities;
using Data.Services.Security;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Implements.Recaptcha;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Configuration;
using Utilities.Custom;
using Web.AutoMapper;

namespace Web.Service
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 0) Infra básica
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<EncriptePassword>(); // si es stateless; si no, Scoped

            // 1) Auditoría / logging
            //services.AddScoped<IAuditService, AuditService>();

            // 2) Persistencia genérica
            //services.AddDbContext<AppDbContext>(...); // (si aplica)
            services.AddScoped(typeof(IData<>), typeof(DataGeneric<>)); // ojo: namespace "Data.Repository" (sin typo)

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

            services.AddScoped<IFineCalculationDetailService, FineCalculationDetailService>();
            services.AddScoped<IFineCalculationDetailRepository, FineCalculationDetailsRepository>();

            services.AddScoped<ITypeInfractionRepository, TypeInfractionRepository>();
            services.AddScoped<ITypeInfractionService, TypeInfractionService>();

            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();

            services.AddScoped<IValueSmldvService, ValueSmldvService>();
            services.AddScoped<IValueSmldvRepository, ValueSmldvRepository>();

            services.AddScoped<IUserInfractionServices, UserInfractionServices>();
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

            //Recaptchat

            services.Configure<RecaptchaOptions>(configuration.GetSection("Recaptcha"));
            services.AddHttpClient<IRecaptchaVerifier, RecaptchaVerifier>();

            //cookies
            services.AddScoped<IAuthSessionRepository, AuthSessionRepository>();   // ADD
            services.AddScoped<IAuthSessionService, AuthSessionService>();

            services.AddDistributedMemoryCache();


            //services.AddScoped<IFineCalculationDetailServices, FineCalculationDetailServices>();

            // 5) Integraciones externas
            // services.AddHttpClient<IApiColombiaGatewayService, ApiColombiaGatewayService>();

            return services;
        }
    }
}
