// Web.Extensions/SwaggerService.cs
using Entity.Domain.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    public static class SwaggerService
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            // Puedes dejarlo aquí (no duplica aunque también esté en Program, pero mejor en un solo sitio)
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                // ✅ Un único documento
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Proyecto Hacienda API",
                    Version = "v1",
                    Description = "API para Carnetización/Multas"
                });

                // ✅ Tus enums como string
                c.MapType<DeleteType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(DeleteType))
                               .Select(n => (IOpenApiAny)new OpenApiString(n)).ToList()
                });

                c.MapType<GetAllType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(GetAllType))
                               .Select(n => (IOpenApiAny)new OpenApiString(n)).ToList()
                });

                // (Opcional) Seguridad JWT – si lo usarás en Swagger UI
                // var securityScheme = new OpenApiSecurityScheme
                // {
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.Http,
                //     Scheme = "bearer",
                //     BearerFormat = "JWT",
                //     In = ParameterLocation.Header,
                //     Description = "Ingresa: Bearer {token}"
                // };
                // c.AddSecurityDefinition("Bearer", securityScheme);
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     { securityScheme, Array.Empty<string>() }
                // });

                // (Opcional) Evitar conflictos si tienes acciones con misma ruta/verb
                // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            return services;
        }
    }
}
