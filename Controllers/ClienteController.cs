

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
		repoCliente = new ClienteRepository();
	}

// ----
	[HttpGet("ListarClientes")]
	public IActionResult Index()
	{
		List<Cliente> lista = repoCliente.ListarClientes();
		return View(lista);
	}
// ----


// ----
	[HttpGet("CargarCliente")]
	public IActionResult CargarCliente()
	{
		return View(new Cliente());
	}

	[HttpPost("CargarCliente")]
	public IActionResult CargarCliente(Cliente cliente)
	{
		repoCliente.CargarCliente(cliente);
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("ActualizarCliente")]
	public IActionResult ActualizarCliente(int id)
	{
		List<Cliente> lista = repoCliente.ListarClientes();
		return View(lista.FirstOrDefault(cliente => cliente.IdCliente == id));
	}

	[HttpPost("ActualizarCliente")]
	public IActionResult ActualizarCliente(Cliente cliente)
	{
		repoCliente.ActualizarCliente(cliente);
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("BorrarCliente")]
	public IActionResult BorrarCliente(int id)
	{
		repoCliente.BorrarCliente(id);
		return RedirectToAction("Index");
	}
// ----

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
