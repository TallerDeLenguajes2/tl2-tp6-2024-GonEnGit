
using tl2_tp6_2024_GonEnGit.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;	// estas dos incluciones van simepre que queras usar cookies
using Microsoft.AspNetCore.Http;

using EspacioModels;
using EspacioInterfaces;
using EspacioViewModels;
using System.Runtime.InteropServices;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class UsuarioController : Controller
{
	private readonly ILogger<ProductoController> _logger;
	private readonly IUsuarioRepository _UsuarioRepository;

	public UsuarioController(ILogger<ProductoController> logger, IUsuarioRepository UsuarioRepository)
	{
		_logger = logger;
		_UsuarioRepository = UsuarioRepository;
	}


	[HttpGet("Index")]
	public IActionResult Index()
	{
		return View(new UsuarioViewModel{Rol = HttpContext.Session.GetString("Rol")});
	}

	[HttpPost("Index")]
	public IActionResult Index(UsuarioViewModel usuarioBuscado)
	{
		Usuario usuarioEncontrado = _UsuarioRepository.BuscarUsuario(usuarioBuscado.Alias, usuarioBuscado.Pass);
		if (usuarioEncontrado == null) return View(usuarioEncontrado);

		HttpContext.Session.SetString("Alias", usuarioEncontrado.Alias);
		HttpContext.Session.SetString("Rol", usuarioEncontrado.Rol);

		usuarioBuscado.Logeado = true;

		//Console.WriteLine(HttpContext.Session.GetString("Alias"));

		return RedirectToAction("Index", "Presupuesto");
	}

	public IActionResult LogOut()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Usuario");
	}
}