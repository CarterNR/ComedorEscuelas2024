using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DI
builder.Services.AddDbContext<SisComedorContext>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

builder.Services.AddScoped<IEstadoPedidoDAL, EstadoPedidoDALImpl>();
builder.Services.AddScoped<IEstadoPedidoService, EstadoPedidoService>();

builder.Services.AddScoped<IEscuelaDAL, EscuelaDALImpl>();
builder.Services.AddScoped<IEscuelaService, EscuelaService>();

builder.Services.AddScoped<IRolDAL, RolDALImpl>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<ITipoCedulaDAL, TipoCedulaDALImpl>();
builder.Services.AddScoped<ITipoCedulaService, TipoCedulaService>();

builder.Services.AddScoped<IProveedorDAL, ProveedorDALImpl>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

builder.Services.AddScoped<IProductoDAL, ProductoDALImpl>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<IProductoDiaDAL, ProductoDiaDALImpl>();
builder.Services.AddScoped<IProductoDiaService, ProductoDiaService>();

builder.Services.AddScoped<IBitacoraDAL, BitacoraDALImpl>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();

builder.Services.AddScoped<IUsuarioDAL, UsuarioDALImpl>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IPedidoDAL, PedidoDALImpl>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
