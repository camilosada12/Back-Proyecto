using Entity.Domain.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    public static class SwaggerService
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Ejemplo: mapear enums por nombre
                c.MapType<DeleteType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(DeleteType))
                        .Select(name => new OpenApiString(name) as IOpenApiAny)
                        .ToList()
                });

                c.MapType<GetAllType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(GetAllType))
                        .Select(name => new OpenApiString(name) as IOpenApiAny)
                        .ToList()
                });

                // 🔐 Bearer Auth
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Ingrese el token JWT con **Bearer {token}**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
            });

            return services;
        }
    }
}
