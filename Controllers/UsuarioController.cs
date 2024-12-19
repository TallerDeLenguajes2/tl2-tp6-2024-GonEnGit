
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
	private readonly ILogger<UsuarioController> _logger;
	private readonly IUsuarioRepository _UsuarioRepository;

	public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository UsuarioRepository)
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
		if (usuarioEncontrado == null)
		{
			_logger.LogInformation("Intento de acceso invalido - Usuario: {alias}, Clave: {contrasenia}", usuarioBuscado.Alias, usuarioBuscado.Pass);
			return View(usuarioBuscado);	
		}

		HttpContext.Session.SetString("Alias", usuarioEncontrado.Alias);
		HttpContext.Session.SetString("Rol", usuarioEncontrado.Rol);
		usuarioBuscado.Logueado = true;

	// parece que para poder usar un archivo como log
	// encesitas una libreria completamente distinta
	// dejalo para el proyecto
		
		DateTime fecha = DateTime.Now;
		string dia = fecha.ToString("dd/MM/yyyy");
		string hora = fecha.ToString("HH:mm:ss");
		_logger.LogInformation("El usuario {alias} se conectó el {dia} a las {hora} hs.", usuarioEncontrado.Alias, dia, hora);

		return RedirectToAction("Index", "Presupuesto");
	}

	public IActionResult LogOut()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Usuario");
	}
}