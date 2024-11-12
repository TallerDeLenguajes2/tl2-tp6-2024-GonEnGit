using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModelos;
using EspacioRepositorios;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class PresupuestoController : Controller
{
    private readonly ILogger<PresupuestoController> _logger;
    private PresupuestoRepository repoPresupuesto;

    public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        _logger = logger;
        repoPresupuesto = new PresupuestoRepository();
    }

// todas la views van a ser archivos dentro de Views,
// aquí pones una metodo que llame al nombre del archivo
// y retorne el metodo View()
// dentro de estos metodos usas los controllers de cada cosa
// ----
    [HttpGet("ConsultarPresupuesto")]
    public IActionResult Index()
    {
        List<Presupuesto> lista = repoPresupuesto.ConsultarPresupuestos();
        return View(lista);
    }
// ----

// ----
    [HttpGet("CrearPresupuesto")]
    public IActionResult CrearPresupuesto()
    {
        return View(new Presupuesto());
    }

    [HttpPost("CrearPresupuesto")]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        repoPresupuesto.CrearPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }
// ----


// ----
    [HttpGet("ActualizarPresupuesto")]
    public IActionResult ActualizarPresupuesto(int id)
    {
        List<Presupuesto> lista = repoPresupuesto.ConsultarPresupuestos();
        return View(lista.FirstOrDefault(presupuesto => presupuesto.IdPresupuesto == id));
    }

    [HttpPost("ActualizarPresupuesto")]
    public IActionResult ActualizarPresupuesto(Presupuesto presupuesto)
    {
        repoPresupuesto.ActualizarPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }
// ----

// ----
    [HttpGet("BorrarPresupuesto")]
    public IActionResult BorrarPresupuesto(int id)
    {
        repoPresupuesto.BorrarDetalles(id);
        repoPresupuesto.BorrarPresupuesto(id);
        return RedirectToAction("Index");
    }
// ----


// ----
    [HttpGet("AgregarDetalle")]
    public IActionResult AgregarDetalle(int id)
    {
        ProductoRepository repoProducto = new ProductoRepository();
        PresupuestoDetalle detalle = new PresupuestoDetalle();

        detalle.IdPresupuesto = id;
        detalle.Productos = repoProducto.ListarProducto();

        return View(detalle);
    }

    [HttpPost("AgregarDetalle")]
    public IActionResult AgregarDetalle(PresupuestoDetalle detalle)
    {
        repoPresupuesto.AgregarDetalle(detalle);
        return RedirectToAction("Index");
    }
// ----


// ----
    // no creo que se pueda borra los detalles de a 1 todavia...
    // como identificas un detalle en particular? necesitas si o si
    // los 3 valores, no tiene un idDetalle...
// ----


// se puede hacer el ultimo sin view models?, Presupuesto tiene que tener una lista de productos
// traes todo con inner join y de ahí podes mostrar cada presupuesto con sus productos

// esto no lo tocas todavia
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}