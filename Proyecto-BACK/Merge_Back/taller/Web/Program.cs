using Business.validaciones.Entities.DocumentInfraction;
using Business.validaciones.Entities.InspectoraReport;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication; // ADD
using Microsoft.AspNetCore.Authentication.JwtBearer; // ADD
using Entity.Domain.Models.Implements.ModelSecurity;
using FluentValidation.AspNetCore;
using Web.Extensions;
using Web.Service;



var builder = WebApplication.CreateBuilder(args);

// --------------------
// Servicios
// --------------------
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();
// Swagger / OpenAPI (lo haremos vía extensión AddSwaggerWithJwt)
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddCustomValidators();

// DI de tu aplicación (incluye reCAPTCHA + sesión + servicios Business)
builder.Services.AddApplicationServices(builder.Configuration);
// Bind de opciones desde secciones recomendadas
//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));      // si tu json tiene "Jwt": { ... }

builder.Services.AddSingleton<ISystemClock, SystemClock>(); // ✅ añade esto
builder.Services.Configure<CookieSettings>(builder.Configuration.GetSection("CookieSettings"));

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
// App services y DI repos/servicios
builder.Services.AddApplicationServices();

builder.Services.AddAuthorization();

// CORS
// Swagger con JWT
builder.Services.AddSwaggerWithJwt();

// DB dinámica (usa tu DbContextFactory)
builder.Services.AddDatabase(builder.Configuration);

// Auth JWT leyendo JwtSettings (options pattern)
builder.Services.AddJwtAuthentication(builder.Configuration);

// CORS
builder.Services.AddCustomCors(builder.Configuration);



var app = builder.Build();

// archivos estáticos si aplica
app.UseStaticFiles();

// Swagger (en Dev/Prod según tu lógica)
// Swagger
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
// HTTPS si lo requieres
// app.UseHttpsRedirection();

app.UseCors("DefaultCors");

app.UseCors();             // usa tu política de CorsService
app.UseAuthentication();   // primero autenticación
app.UseAuthorization();    // luego autorización

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

MigrationManager.MigrateAllDatabases(app.Services, builder.Configuration);

app.Run();
