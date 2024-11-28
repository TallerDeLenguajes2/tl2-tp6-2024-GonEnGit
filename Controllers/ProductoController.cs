

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Metadata;

using tl2_tp6_2024_GonEnGit.Models;

using EspacioModels;
using EspacioInterfaces;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;
    private readonly IProductoRepository _ProductoRepository;

    public ProductoController(ILogger<ProductoController> logger, IProductoRepository productoRepository)
    {
        _logger = logger;
        _ProductoRepository = productoRepository;
    }

// ----
    // todas la views van a ser archivos dentro de Views,
    // aquí pones una metodo que llame al nombre del archivo
    // y retorne el metodo View()
    // dentro de estos metodos usas los controllers de cada cosa
    [HttpGet("ListarProducto")]
    public IActionResult Index()
    {
        List<Producto> lista = _ProductoRepository.ListarProducto();
        return View(lista);
    }
// ----

// ----
    // ----- Pudiste preguntar como solucionar el error 405
    // ----- resulta que necesitas usar un GET antes del POST
    // ----- de esa forma evitas que te diga que los metodos no coinciden

    // ----- Acá lo que tenes es un metodo GET que crea un producto vacio
    // ----- toma los datos del formulario para ponerlos en el objeto nuevo
    [HttpGet("RegistrarProducto")]
    public IActionResult RegistrarProducto()
    {
        // ----- ni siquiera hace falta crear el objero por nombre podes mandar un new
        return View(new Producto());
    }
// ----

// ----
    // ----- NOTA: fijate que los 2 controllers tienen el mismo nombre

    // ----- y un POST que resibe el objeto cargado y lo envia
    // ----- a la db usando el metodo RegistrarProducto
    [HttpPost("RegistrarProducto")]
    public IActionResult RegistrarProducto(Producto producto)
    {
        _ProductoRepository.CargarNuevoProducto(producto);
        return RedirectToAction("Index");
    }
// ----


// ----
    [HttpGet("ActualizarProducto")]
    public IActionResult ActualizarProducto(int id)
    {
        List<Producto> lista = _ProductoRepository.ListarProducto();
        return View(lista.FirstOrDefault(producto => producto.Id == id));
    }

    [HttpPost("ActualizarProducto")]
    public IActionResult ActualizarProducto(Producto producto)
    {
        _ProductoRepository.ActualizarProducto(producto);
        return RedirectToAction("Index");
    }
// ----

// ----
    // algo que te apareció en el quiz, no hace falta explicitar el View
    // por nombre si el View tiene el mismo nombre del metodo
    [HttpGet("BorrarProducto")]
    public IActionResult BorrarProducto(int id)
    {
        _ProductoRepository.BorrarProducto(id);
        return RedirectToAction("Index");
    }
// ----

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
