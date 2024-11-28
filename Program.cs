using System.Security.Cryptography;
using Microsoft.AspNetCore.Session;

using EspacioInterfaces;
using EspacioRepositorios;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Tuyos
builder.Services.AddSingleton<IProductoRepository, ProductoRepository>();

// la cadena de conexion es un poco diferente de las interfaces
// a esta la ligas con la configuracion de "ConnectionStrings" en appsettings.json
string CadenaDeConexion = builder.Configuration.GetConnectionString("Conexion")!.ToString();
builder.Services.AddSingleton(CadenaDeConexion);        // el nombre "Conexion" no importa siempre que coincida en appsettings

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
