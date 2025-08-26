using Web.Extensions;
using Web.Middleware; // ApiExceptionMiddleware, DbContextMiddleware
using Web.Service;

using FluentValidation.AspNetCore;
using Entity.Domain.Models.Implements.Entities;
using Business.validaciones.InspectoraReport;
using Business.validaciones.DocumentInfraction;
using Business.validaciones.FineCalculationDetail;
using Business.validaciones.TypeInfraction;
using Business.validaciones.ValueSmldv;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Servicios
// --------------------
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation (validaciones en Business, ejecutadas en Web)
builder.Services
    .AddFluentValidationAutoValidation()          // activa la auto-validación con [ApiController]
    .AddFluentValidationClientsideAdapters();     // opcional (si usas clientes MVC/Razor)

builder.Services.AddCustomValidators();
builder.Services.AddValidatorsFromAssemblyContaining<InspectoraReportCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DocumentInfractionCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FineCalculationDetailValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TypeInfractionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ValueSmldvValidator>();




builder.Services.AddApplicationServices();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomCors(builder.Configuration);

var app = builder.Build();

// 2) Contexto por request (si aplica a tu proyecto)
builder.Services.AddCustomValidators();
app.UseStaticFiles();

// 4) Swagger (en Dev/Prod según tu lógica)
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger";
    });
}

// 5) Redirección HTTPS (habilítalo si sirve en tu hosting)
//app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
