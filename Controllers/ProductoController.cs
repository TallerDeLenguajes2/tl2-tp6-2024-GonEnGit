

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModelos;
using EspacioRepositorios;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;
    private ProductoRepository repoProducto;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
        repoProducto = new ProductoRepository();
    }

// todas la views van a ser archivos dentro de Views,
// aquí pones una metodo que llame al nombre del archivo
// y retorne el metodo View()
// dentro de estos metodos usas los controllers de cada cosa
    [HttpGet("ListarProducto")]
    public IActionResult Index()
    {
        List<Producto> lista = repoProducto.ListarProducto();
        return View(lista);
    }

// ----- Pudiste preguntar como solucionar el error 405
// ----- resulta que necesitas usar un GET antes del POST
// ----- de esa forma evitas que te diga que los metodos no coinciden

    // ----- Acá lo que tenes es un metodo GET que crea un producto vacio
    // ----- toma los datos del formulario para ponerlos en el objeto nuevo
    [HttpGet]
    public IActionResult RegistrarProducto()
    {
    // ----- ni siquiera hace falta crear el objero por nombre podes mandar un new
        return View(new Producto());
    }

    // ----- NOTA: fijate que los 2 controllers tienen el mismo nombre

    // ----- y un POST que resive el objeto cargado y lo envia
    // ----- a la db usando el metodo RegistrarProducto
    [HttpPost]
    public IActionResult RegistrarProducto(Producto producto)
    {
        repoProducto.CargarNuevoProducto(producto);
        return RedirectToAction("Index");
    }
// ------

// ----
    [HttpGet]
    public IActionResult ActualizarProducto(int id)
    {
        List<Producto> lista = repoProducto.ListarProducto();
        return View(lista.FirstOrDefault(producto => producto.Id == id));
    }

    [HttpPost]
    public IActionResult ActualizarProducto(Producto producto)
    {
        repoProducto.ActualizarProducto(producto);
        return RedirectToAction("Index");
    }
// ----

// algo que te apareció en el quiz, no hace falta explicitar el View
// po nombre si el View tiene el mismo nombre del metodo
   [HttpGet]
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
