using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cmsSystem.Models;

namespace cmsSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult News()
    {
        return View();
    }
/*
    [HttpPost]
    public ActionResult Index(ViewModel model)
    {
        return View("Index");
    } */

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
