using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");


builder.Services.AddControllersWithViews();
#region

builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Puedes ajustar el tiempo de inactividad
});
builder.Services.AddDistributedMemoryCache();  // Necesario para usar sesiones


builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IEscuelaHelper, EscuelaHelper>();
builder.Services.AddScoped<IEstudianteHelper, EstudianteHelper>();
builder.Services.AddScoped<IPedidoHelper, PedidoHelper>();
builder.Services.AddScoped<IProductoHelper, ProductoHelper>();
builder.Services.AddScoped<IProductoDiaHelper, ProductoDiaHelper>();
builder.Services.AddScoped<IProveedorHelper, ProveedorHelper>();
builder.Services.AddScoped<IUsuarioHelper, UsuarioHelper>();
builder.Services.AddScoped<IEstadoPedidoHelper, EstadoPedidoHelper>();
builder.Services.AddScoped<IRolHelper, RolHelper>();
builder.Services.AddScoped<ITipoCedulaHelper, TipoCedulaHelper>();


builder.Services.AddScoped<ISecurityHelper, SecurityHelper>();
builder.Services.AddHttpContextAccessor();


#endregion



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",

pattern: "{controller=Login}/{action=Login}");

app.Run();

