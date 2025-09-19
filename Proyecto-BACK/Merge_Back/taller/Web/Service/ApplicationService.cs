using Business.Custom;
using Business.ExternalServices.Recaptcha;
using Business.Interfaces.IBusinessImplements;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.IBusinessImplements.parameters;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Interfaces.IJWT;
using Business.Interfaces.PDF;
using Business.Mensajeria.Email.implements;
using Business.Mensajeria.Email.@interface;
using Business.Mensajeria.Implements;
using Business.Services;
using Business.Services.Entities;
using Business.Services.parameters;
using Business.Services.PDF;
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
using Entity.Domain.Models.Implements.Recaptcha;
using Microsoft.AspNetCore.Authentication; // 👈 para ISystemClock
using Microsoft.AspNetCore.Identity;
using Utilities.Custom;
using Web.AutoMapper;
using Web.Configurations;
using Web.Infrastructure;
using ServiceEmail = Business.Mensajeria.Email.implements;
using Web.WebBackgroundService;

namespace Web.Service
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Infra
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<Utilities.Custom.EncriptePassword>();
            services.AddSingleton<EncriptePassword>();
            services.AddMemoryCache();

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

            services.AddScoped<IPdfGeneratorService, PdfService>();


            
            //services.AddHostedService<InfractionDiscountBackgroundService>();
            //services.AddScoped<DiscountService>();



            services.AddScoped<IValueSmldvRepository, ValueSmldvRepository>();
            services.AddScoped<IFineCalculationDetailRepository, FineCalculationDetailsRepository>();
            services.AddScoped<ITypeInfractionRepository, TypeInfractionRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IUserInfractionRepository, UserInfractionRepository>();

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

            // 🔐 Sesiones
            services.AddScoped<IAuthSessionRepository, AuthSessionRepository>();
            services.AddScoped<IAuthSessionService, AuthSessionService>();
            services.AddSingleton<ISystemClock, SystemClock>();

            // Servicios — ENTITIES
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

            // Identity y Tokens
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IToken, TokenBusiness>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordResetCodeRepository, PasswordResetCodeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserMeRepository, MeRepository>();
            services.AddScoped<IAuthCookieFactory, AuthCookieFactory>();

            services.AddScoped<IServiceEmail, ServiceEmails>();

            services.AddScoped<EmailBackgroundQueue>();


            //backGround services
            // Configuración de intereses (appsettings.json -> PaymentAgreementInterestOptions)
            services.Configure<PaymentAgreementInterestOptions>(
            configuration.GetSection("PaymentAgreementInterestOptions"));

            // Background job de intereses/coactivo
            services.AddHostedService<PaymentAgreementBackgroundService>();

            return services;
        }
    }
}
