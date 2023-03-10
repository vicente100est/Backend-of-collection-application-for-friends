using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DAL.Services;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Common;
using Pagos.Backend.Services.Auth.IService;
using Pagos.Backend.Services.Auth.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string MiCors = "MyCors";

var appSettingsSection = builder.Configuration.GetSection("AppSettings");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MiCors,
        builder =>
        {
            builder.WithHeaders("*");
            builder.WithOrigins("*");
            builder.WithMethods("*");
        });
});

//DbContext
builder.Services.AddMySql<DeudaContext>(builder.Configuration.GetConnectionString("DeudaDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.37-mariadb"));

//Secret
builder.Services.Configure<AppSettings>(appSettingsSection);

//JWT
var appSettings = appSettingsSection.Get<AppSettings>();
var keyToken = Encoding.ASCII.GetBytes(appSettings.Secreto);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyToken),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Services
builder.Services.AddTransient<DeudaContext>();
builder.Services.AddTransient<GenericDTO>();
builder.Services.AddTransient<AdminDTO>();
builder.Services.AddTransient<UserDTO>();
builder.Services.AddTransient<IStatusProvider, StatusProvider>();
builder.Services.AddTransient<IServicioProvider, ServicioProvider>();
builder.Services.AddTransient<IUsuarioProvider, UsuarioProvider>();
builder.Services.AddTransient<IUsuarioServicioProvider, UsuarioServicioProvider>();
builder.Services.AddTransient<IMensualidadProvider, MensualidadProvider>();
builder.Services.AddTransient<IPagosProvider, PagosProvider>();
builder.Services.AddTransient<IAdminProvider, AdminProvider>();
builder.Services.AddTransient<IAuthAdminProvider, AuthAdminProvider>();
builder.Services.AddTransient<IAuthUsuarioProvider, AuthUsuarioProvider>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MiCors);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
