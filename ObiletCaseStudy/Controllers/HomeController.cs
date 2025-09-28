using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ObiletCaseStudy.Models;
using ObiletCaseStudy.Models.ViewModels;

namespace ObiletCaseStudy.Controllers;

public class HomeController : Controller
{
    private readonly IObiletService _obiletService;
    private readonly ISessionService _sessionService;

    public HomeController(IObiletService obiletService, ISessionService sessionService)
    {
        _obiletService = obiletService;
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Index()
    {
        var lastSearch = _sessionService.GetLastSearch();
        var locations = await _obiletService.GetBusLocationsAsync();

        var model = new IndexViewModel
        {
            OriginId = lastSearch.OriginId,
            DestinationId = lastSearch.DestinationId,
            DepartureDate = lastSearch.DepartureDate,
            Locations = locations
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult SetLastSearch([FromBody] SearchViewModel model)
    {
        _sessionService.SetLastSearch(model);
        return Json(new { success = true });
    }

    [HttpGet]
    public async Task<JsonResult> GetBusLocations(string searchText)
    {
        var locations = await _obiletService.GetBusLocationsAsync(searchText);
        return Json(locations);
    }
}