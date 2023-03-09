using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DAL.Services;
using Pagos.Backend.Data;
using Pagos.Backend.DTO;
using Pagos.Backend.Services.Auth.IService;
using Pagos.Backend.Services.Auth.Service;

var builder = WebApplication.CreateBuilder(args);
string MiCors = "MyCors";

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
builder.Services.AddTransient<DeudaContext>();
builder.Services.AddTransient<GenericDTO>();
builder.Services.AddTransient<IStatusProvider, StatusProvider>();
builder.Services.AddTransient<IServicioProvider, ServicioProvider>();
builder.Services.AddTransient<IUsuarioProvider, UsuarioProvider>();
builder.Services.AddTransient<IUsuarioServicioProvider, UsuarioServicioProvider>();
builder.Services.AddTransient<IMensualidadProvider, MensualidadProvider>();
builder.Services.AddTransient<IPagosProvider, PagosProvider>();
builder.Services.AddTransient<IAdminProvider, AdminProvider>();
builder.Services.AddTransient<IAuthAdminProvider, AuthAdminProvider>();
builder.Services.AddScoped<IAdminService, AdminService>();

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
