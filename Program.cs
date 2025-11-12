using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MySql.EntityFrameworkCore.Extensions;
using rrhh_backend.Data;
using rrhh_backend.Services.Administracion;
using rrhh_backend.Services.Rrhh;
using rrhh_backend.Utilidades;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RrHhContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")

    )
);

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<RrhhColaboradorService, RrhhColaboradorService>();
builder.Services.AddScoped<RrhhEstadoCivilService, RrhhEstadoCivilService>();
builder.Services.AddScoped<RrhhEstadoColaboradorService, RrhhEstadoColaboradorService>();
builder.Services.AddScoped<RrhhHistorialColaboradorService, RrhhHistorialColaboradorService>();
builder.Services.AddScoped<RrhhHistorialLicenciaService, RrhhHistorialLicenciaService>();
builder.Services.AddScoped<UserUserRolesService>();
builder.Services.AddScoped<UserPermisosService>();
builder.Services.AddScoped<AdminDepartamentoService>();
builder.Services.AddScoped<AdminUserRolService>();
builder.Services.AddScoped<AdminUserService>();
builder.Services.AddScoped<AdminUserUserService>();
builder.Services.AddScoped<RrhhLicenciasService>();
builder.Services.AddScoped<AdminPermisosService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
// Configurar autenticaci�n JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? string.Empty))
    };
});
builder.Services.AddTransient<IAlmacenadorAzure, AlmacenadorAzure>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                              "https://rhopage.web.app",
                              "https://pyrho-73828.web.app")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

// Agregar tarea programada como servicio alojado
builder.Services.AddHostedService<rrhh_backend.Services.Hosted.TareaProgramadaService>();

var app = builder.Build();


// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{

    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// Habilitar CORS
app.UseCors("MyAllowSpecificOrigins");

// Redireccionamiento HTTPS
app.UseHttpsRedirection();

// Autenticaci�n
app.UseAuthentication();

// Autorizaci�n
app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(app.Environment.ContentRootPath, "Imagenes")),
//    RequestPath = "/imagenes" // Ruta desde la cual se servir�n los archivos est�ticos
//});

app.MapControllers();

app.Run();
