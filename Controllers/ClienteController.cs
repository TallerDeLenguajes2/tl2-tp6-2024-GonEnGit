

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModelos;
using EspacioRepositorios;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class ClienteController : Controller
{
	private readonly ILogger<ClienteController> _logger;
	private ClienteRepository repoCliente;

	public ClienteController(ILogger<ClienteController> logger)
	{
		_logger = logger;
		repoCliente = new ClienteRepository(); // Falta -----------------------------------------
	}

// ----
	[HttpGet("ListarClientes")]
	public IActionResult Index()
	{
		List<Producto> lista = repoCliente.ListarClientes(); // Falta -----------------------------------------
		return View(lista);
	}
// ----


// ----
	[HttpGet("RegistrarCliente")]
	public IActionResult RegistrarCliente()
	{
		return View(new Cliente());
	}

	[HttpPost("RegistrarCliente")]
	public IActionResult RegistrarCliente(Cliente cliente)
	{
		repoCliente.CargarNuevoCliente(cliente); // Falta -----------------------------------------
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("ActualizarCliente")]
	public IActionResult ActualizarCliente(int id)
	{
		List<Cliente> lista = repoCliente.ListarCliente(); // Falta -----------------------------------------
		return View(lista.FirstOrDefault(cliente => cliente.IdCliente == id));
	}

	[HttpPost("ActualizarCliente")]
	public IActionResult ActualizarCliente(Cliente cliente)
	{
		repoCliente.ActualizarCliente(cliente); // Falta -----------------------------------------
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("BorrarCliente")]
	public IActionResult BorrarCliente(int id)
	{
		repoCliente.BorrarCliente(id); // Falta -----------------------------------------
		return RedirectToAction("Index");
	}
// ----

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
