using Microsoft.Extensions.DependencyInjection;
using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DAL.Services;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Common;
using Pagos.Backend.Services.Auth.IService;
using Pagos.Backend.Services.Auth.Service;

var builder = WebApplication.CreateBuilder(args);
string MiCors = "MyCors";

var appSettings = builder.Configuration.GetSection("AppSettings");
//var dbContext = builder.Configuration["ConnectionStrings:DeudaDB"];

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

builder.Services.AddMySql<DeudaContext>(builder.Configuration.GetConnectionString("DeudaDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.37-mariadb"));

//Sevret
builder.Services.Configure<AppSettings>(appSettings);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
