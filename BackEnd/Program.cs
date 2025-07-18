using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    // Opción para tomar la URL desde el appsettings.json (opcional)
    var kestrelSection = builder.Configuration.GetSection("Kestrel:Endpoints:Http:Url");
    var url = kestrelSection.Value;
    if (!string.IsNullOrEmpty(url))
    {
        options.ListenAnyIP(new Uri(url).Port);  // Usando la URL del appsettings (si está configurada)
    }
    else
    {
        // Puerto fijo si no se encuentra en el archivo de configuración
        options.ListenAnyIP(5273);  // Usar puerto 5273
    }
});


// Agregar servicios a la colección
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BD
// Configuración de la base de datos con SQL Server
builder.Services.AddDbContext<SisComedorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<AuthDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

#endregion

#region CORS
// Configuración de CORS para permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

#region Identity (comentado temporalmente si no lo usas)
//// Configuración de Identity
//builder.Services.AddIdentityCore<IdentityUser>()
//   .AddRoles<IdentityRole>()
//   .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("fide")
//   .AddEntityFrameworkStores<AuthDBContext>()
//   .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//   options.Password.RequiredLength = 5;
//   options.Password.RequireNonAlphanumeric = false;
//   options.Password.RequireDigit = false;
//   options.Password.RequireLowercase = false;
//   options.Password.RequireUppercase = false;
//});

#endregion

#region JWT (comentado si no lo usas)
//// Configuración de JWT
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//  .AddJwtBearer(options =>
//  {
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//    };
//});

#endregion

#region Servicios
// Agregar los servicios que usa el backend
builder.Services.AddDbContext<SisComedorContext>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

builder.Services.AddScoped<IEstadoPedidoDAL, EstadoPedidoDALImpl>();
builder.Services.AddScoped<IEstadoPedidoService, EstadoPedidoService>();

builder.Services.AddScoped<IEscuelaDAL, EscuelaDALImpl>();
builder.Services.AddScoped<IEscuelaService, EscuelaService>();

builder.Services.AddScoped<IEstudianteDAL, EstudianteDALImpl>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();

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

builder.Services.AddScoped<EstudianteService>();
#endregion

var app = builder.Build();

// Aplicar middleware de CORS
app.UseCors("AllowAll"); // Habilita CORS para todas las rutas

// Configuración de Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de rutas y controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();
