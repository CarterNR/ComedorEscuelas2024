using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

#region
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => x.LoginPath = "/Login/LoginEscuelas");

builder.Services.AddSession();


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

builder.Services.AddScoped<ISecurityHelper, SecurityHelper>();
builder.Services.AddHttpContextAccessor();


#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
 pattern: "{controller=Estudiante}/{action=Index}/{id?}");
//pattern: "{controller=Home}/{action=Index}");

app.Run();
