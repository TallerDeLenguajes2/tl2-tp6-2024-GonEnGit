

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
    [HttpGet("ListarProductos")]
    public IActionResult Index()
    {
        List<Productos> lista = repoProducto.ListarProductos();
        return View(lista);
    }

// ----- Pudiste preguntar como solucionar el error 405
// ----- resulta que necesitas usar un GET antes del POST
// ----- de esa forma evitas que te diga que los metodos no coinciden
    [HttpGet]
    public IActionResult RegistrarProducto()
    {
        Productos nuevoProducto = new Productos();
        return View(nuevoProducto);
    }

    [HttpPost]
    public IActionResult RegistrarProducto(Productos producto)
    {
        repoProducto.CargarNuevoProducto(producto);
        return RedirectToAction("Index");
    }
// ------

    [HttpPut]
    public IActionResult ActualizarProducto(Productos producto)
    {
        repoProducto.ActualizarProducto(producto.Id, producto.Descripcion, producto.Precio);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult BorrarProducto(int id)
    {
        repoProducto.BorrarProducto(id);
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
