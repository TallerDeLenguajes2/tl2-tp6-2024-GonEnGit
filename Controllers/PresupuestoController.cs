
/* este archivo quedaria obsoleto en teoria */

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModels;
using EspacioViewModels;
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

    // ----
    [HttpGet("ConsultarPresupuesto")]
    public IActionResult Index()
    {
        PresupuestoViewModel modelo = repoPresupuesto.ConsultarPresupuestos();
        return View(modelo);
    }
    // ----

    // ----
    [HttpGet("ListarPresupuestos")]
    public IActionResult ListarPresupuestos()
    {
        ListaPresupuestosViewModel modelo = repoPresupuesto.ListarPresupuestos();
        return View(modelo);
    }
    // ----

    // ----
    [HttpGet("CrearPresupuesto")]
    public IActionResult CrearPresupuesto(int id)
    {
        Presupuesto nuevo = new Presupuesto();
        nuevo.IdCliente = id;
        return View(nuevo);
    }

    [HttpPost("CrearPresupuesto")]
    public IActionResult CrearPresupuesto(Presupuesto pres)
    {
        repoPresupuesto.CrearPresupuesto(pres);
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
        return RedirectToAction("ListarPresupuestos");
    }
    // ----

    // ----
    [HttpGet("BorrarDetalle")]
    public IActionResult BorrarDetalle(int idPresupuesto, int idProducto)
    {
        repoPresupuesto.BorrarDetalleUnico(idPresupuesto,idProducto);
        return RedirectToAction("ListarPresupuestos");
    }
    // ----


    // esto no lo tocas todavia
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}