
using System.Security.Cryptography;
using Microsoft.AspNetCore.Session;
using Microsoft.Data.Sqlite;

using EspacioInterfaces;
using EspacioRepositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- Con esta parte, las cookies deberian funcionar bien
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// --- Tuyos
builder.Services.AddSingleton<IProductoRepository, ProductoRepository>();
builder.Services.AddSingleton<IPresupuestoRepository, PresupuestoRepository>();
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

// --- la cadena de conexion es un poco diferente de las interfaces
// --- a esta la ligas con la configuracion de "ConnectionStrings" en appsettings.json
string CadenaDeConexion = builder.Configuration.GetConnectionString("Conexion")!.ToString();
builder.Services.AddSingleton(CadenaDeConexion);        // el nombre "Conexion" no importa siempre que coincida en appsettings


// --- el orden importa en el sentido de que los services son
// --- basicamente herramientas, van antes de correr el builder
// --- para armar la app final
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();   // --- estas son indicaciones de que herramientas usar

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
