using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FronEnd.Models;
using FronEnd.Helpers.Interfaces;

namespace FronEnd.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPlatilloHelper _platilloHelper;

    public HomeController(ILogger<HomeController> logger, IPlatilloHelper platilloHelper)
    {
        _logger = logger;
        _platilloHelper = platilloHelper;
    }

    public IActionResult Index()
    {
        var platillos = _platilloHelper.GetPlatillos();
        return View(platillos);
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
