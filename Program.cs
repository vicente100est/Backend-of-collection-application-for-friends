using Pagos.Backend.DAL.IServices;
using Pagos.Backend.DAL.Services;
using Pagos.Backend.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<GenericDTO>();
builder.Services.AddTransient<IStatusProvider, StatusProvider>();
builder.Services.AddTransient<IServicioProvider, ServicioProvider>();
builder.Services.AddTransient<IUsuarioProvider, UsuarioProvider>();
builder.Services.AddTransient<IUsuarioServicioProvider, UsuarioServicioProvider>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
