using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Utilities.SystemConstants.cs;

namespace AdminApp.Controllers;

[Authorize]
public class HomeController : BaseController
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Language(NavigationViewModel viewModel)
    {
        HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, viewModel.CurrentLanguageId);
        return RedirectToAction("Index");
    }
}
