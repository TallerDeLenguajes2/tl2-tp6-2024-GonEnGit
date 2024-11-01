using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_GonEnGit.Models;

namespace tl2_tp6_2024_GonEnGit.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

// todas la views van a ser archivos dentro de Views,
// aquí pones una metodo que llame al nombre del archivo
// y retorne el metodo View()
    public IActionResult Index()
    {
        return View();
    }

// dentro de estos modesl haces los controllers de cada cosa
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
