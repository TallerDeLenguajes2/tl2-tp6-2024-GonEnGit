

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModels;
using EspacioViewModels;
using EspacioRepositorios;
using EspacioInterfaces;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class ClienteController : Controller
{
	private readonly ILogger<ClienteController> _logger;
	private readonly IClienteRepository _ClienteRepository;

	public ClienteController(ILogger<ClienteController> logger, IClienteRepository ClienteRepository)
	{
		_logger = logger;
		_ClienteRepository = ClienteRepository;
	}

// ----
	[HttpGet("ListarClientes")]
	public IActionResult Index()
	{
		ClienteViewModel nuevo = new ClienteViewModel(_ClienteRepository.ListarClientes());
		nuevo.Rol = HttpContext.Session.GetString("Rol");
		return View(nuevo);
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
		_ClienteRepository.CargarCliente(cliente);
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("ActualizarCliente")]
	public IActionResult ActualizarCliente(int id)
	{
		List<Cliente> lista = _ClienteRepository.ListarClientes();
		return View(lista.FirstOrDefault(cliente => cliente.IdCliente == id));
	}

	[HttpPost("ActualizarCliente")]
	public IActionResult ActualizarCliente(Cliente cliente)
	{
		_ClienteRepository.ActualizarCliente(cliente);
		return RedirectToAction("Index");
	}
// ----


// ----
	[HttpGet("BorrarCliente")]
	public IActionResult BorrarCliente(int id)
	{
		_ClienteRepository.BorrarCliente(id);
		return RedirectToAction("Index");
	}
// ----

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
