

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using tl2_tp6_2024_GonEnGit.Models;

using EspacioModels;
using EspacioViewModels;
using EspacioInterfaces;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class PresupuestoController : Controller
{
    private readonly ILogger<PresupuestoController> _logger;
    private readonly IProductoRepository _ProductoRepository;
    private readonly IPresupuestoRepository _PresupuestoRepository;

    public PresupuestoController(ILogger<PresupuestoController> logger, IProductoRepository productoRepository, IPresupuestoRepository PresupuestoRepository)
    {
        _logger = logger;
        _ProductoRepository = productoRepository;
        _PresupuestoRepository = PresupuestoRepository;
    }

    // ----
    [HttpGet("ConsultarPresupuesto")]
    public IActionResult Index()
    {
        PresupuestoViewModel modelo = _PresupuestoRepository.ConsultarPresupuestos();
        return View(modelo);
    }
    // ----

    // ----
    [HttpGet("ListarPresupuestos")]
    public IActionResult ListarPresupuestos()
    {
        ListaPresupuestosViewModel modelo = _PresupuestoRepository.ListarPresupuestos();
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
        _PresupuestoRepository.CrearPresupuesto(pres);
        return RedirectToAction("Index");
    }
    // ----

    // ----
    [HttpGet("BorrarPresupuesto")]
    public IActionResult BorrarPresupuesto(int id)
    {
        _PresupuestoRepository.BorrarDetalles(id);
        _PresupuestoRepository.BorrarPresupuesto(id);
        return RedirectToAction("Index");
    }
    // ----


    // ----
    [HttpGet("AgregarDetalle")]
    public IActionResult AgregarDetalle(int id)
    {
        PresupuestoDetalle detalle = new PresupuestoDetalle();

        detalle.IdPresupuesto = id;
        detalle.Productos = _ProductoRepository.ListarProducto();

        return View(detalle);
    }

    [HttpPost("AgregarDetalle")]
    public IActionResult AgregarDetalle(PresupuestoDetalle detalle)
    {
        _PresupuestoRepository.AgregarDetalle(detalle);
        return RedirectToAction("ListarPresupuestos");
    }
    // ----

    // ----
    [HttpGet("BorrarDetalle")]
    public IActionResult BorrarDetalle(int idPresupuesto, int idProducto)
    {
        _PresupuestoRepository.BorrarDetalleUnico(idPresupuesto,idProducto);
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