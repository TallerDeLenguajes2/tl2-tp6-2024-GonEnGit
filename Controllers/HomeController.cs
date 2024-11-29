
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

using EspacioViewModels;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        SharedViewModel barra = new SharedViewModel();
        barra.Rol = HttpContext.Session.GetString("Rol");
        return View(barra);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
