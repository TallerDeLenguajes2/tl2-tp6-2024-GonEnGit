

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModelos;
using EspacioRepositorios;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private ProductoRepository repoProducto;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        repoProducto = new ProductoRepository();
    }

// todas la views van a ser archivos dentro de Views,
// aquí pones una metodo que llame al nombre del archivo
// y retorne el metodo View()
// dentro de estos metodos usas los controllers de cada cosa
    public IActionResult Index()
    {
        List<Productos> lista = repoProducto.ListarProductos();
        return View(lista);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
