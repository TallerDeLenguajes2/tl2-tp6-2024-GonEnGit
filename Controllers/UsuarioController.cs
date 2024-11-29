
using tl2_tp6_2024_GonEnGit.Models;

using EspacioModels;
using EspacioInterfaces;
using Microsoft.AspNetCore.Mvc;
using EspacioViewModels;

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


	[HttpGet("BuscarUsuario")]
	public IActionResult Index()
	{
		return View();
	}
/*
	[HttpPost("BuscarUsuario")]
	public IActionResult Index(UsuarioViewModel usuarioBuscado)
	{
		_UsuarioRepository.BuscarUsuario(usuarioBuscado.Alias, usuarioBuscado.Pass);
		return 
	}*/
}