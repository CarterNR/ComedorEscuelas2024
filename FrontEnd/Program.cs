using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IEscuelaHelper, EscuelaHelper>();
builder.Services.AddScoped<IPedidoHelper, PedidoHelper>();
builder.Services.AddScoped<IProductoHelper, ProductoHelper>();
builder.Services.AddScoped<IProductoDiaHelper, ProductoDiaHelper>();
builder.Services.AddScoped<IProveedorHelper, ProveedorHelper>();
builder.Services.AddScoped<IUsuarioHelper, UsuarioHelper>();
builder.Services.AddScoped<IEstadoPedidoHelper, EstadoPedidoHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


