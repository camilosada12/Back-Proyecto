using Entity.Domain.Models.Implements.ModelSecurity;
using FluentValidation.AspNetCore;
using Web.Extensions;
using Web.Service;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Servicios
// --------------------
builder.Services.AddControllers();

// Swagger / OpenAPI (lo haremos vía extensión AddSwaggerWithJwt)
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddCustomValidators();

// Bind de opciones desde secciones recomendadas
//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));      // si tu json tiene "Jwt": { ... }

builder.Services.Configure<CookieSettings>(builder.Configuration.GetSection("CookieSettings"));

// App services y DI repos/servicios
builder.Services.AddApplicationServices();

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

// HTTPS si lo requieres
// app.UseHttpsRedirection();

app.UseCors("DefaultCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

MigrationManager.MigrateAllDatabases(app.Services, builder.Configuration);

app.Run();
