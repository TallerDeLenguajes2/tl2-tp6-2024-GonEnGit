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
    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuesto> lista = repoPresupuesto.ConsultarPresupuestos();
        return View(lista);
    }

    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        repoPresupuesto.CrearPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }

    [HttpPut]
    public IActionResult ActualizarPresupuesto(Presupuesto presupuesto)
    {
        repoPresupuesto.ActualizarPresupuesto(presupuesto);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult BorrarPresupuesto(int id)
    {
        repoPresupuesto.BorrarPresupuesto(id);
        return RedirectToAction("Index");
    }



// esto no lo tocas todavia
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}