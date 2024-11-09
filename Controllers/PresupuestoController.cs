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
    [HttpGet("ConsultarPresupuesto")]
    public IActionResult Index()
    {
        List<Presupuesto> lista = repoPresupuesto.ConsultarPresupuestos();
        return View(lista);
    }

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

    [HttpDelete("BorrarPresupuesto")]
    public IActionResult BorrarPresupuesto(int id)
    {
        repoPresupuesto.BorrarPresupuesto(id);
        return RedirectToAction("Index");
    }

    [HttpPost("AgregarDetalle")]
    public IActionResult AgregarDetalle(int idPres, int idProd, int cant)
    {
        repoPresupuesto.AgregarDetalle(idPres, idProd, cant);
        return RedirectToAction("Index");
    }

// se puede hacer el ultimo sin view models?

// esto no lo tocas todavia
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}