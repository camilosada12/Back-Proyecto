using Business.validaciones.Entities.DocumentInfraction;
using Business.validaciones.Entities.InspectoraReport;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication; // ADD
using Microsoft.AspNetCore.Authentication.JwtBearer; // ADD
using Web.Extensions;
using Web.Infrastructure;
using Web.Service;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Servicios
// --------------------
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

// FluentValidation (validaciones en Business, ejecutadas en Web)
builder.Services
    .AddFluentValidationAutoValidation()          // activa la auto-validación con [ApiController]
    .AddFluentValidationClientsideAdapters();     // opcional (si usas clientes MVC/Razor)

builder.Services.AddCustomValidators();

// DI de tu aplicación (incluye reCAPTCHA + sesión + servicios Business)
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSingleton<ISystemClock, SystemClock>(); // ✅ añade esto

// JWT (para login con Email)
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// Sesión DocSession como default
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "DocSession";
        options.DefaultChallengeScheme = "DocSession";
    })
    .AddScheme<DocSessionAuthenticationOptions, DocSessionAuthenticationHandler>(
        "DocSession",
        o => { o.CookieName = "ph_session"; }
    );

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCustomCors(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

// Swagger (en Dev/Prod según tu lógica)
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger";
    });
}

//app.UseHttpsRedirection();

app.UseCors();             // usa tu política de CorsService
app.UseAuthentication();   // primero autenticación
app.UseAuthorization();    // luego autorización

app.MapControllers();

app.Run();
