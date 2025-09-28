using Microsoft.AspNetCore.Mvc;
using ObiletCaseStudy.Models.ViewModels;

namespace ObiletCaseStudy.Controllers;

public class JourneyController : Controller
{
    private readonly IObiletService _obiletService;
    private readonly ISessionService _sessionService;

    public JourneyController(IObiletService obiletService, ISessionService sessionService)
    {
        _obiletService = obiletService;
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Index(int originId, int destinationId, DateTime departureDate)
    {
        // Son aramayı kaydet
        var searchModel = new SearchViewModel
        {
            OriginId = originId,
            DestinationId = destinationId,
            DepartureDate = departureDate
        };
        _sessionService.SetLastSearch(searchModel);

        var journeys = await _obiletService.GetBusJourneysAsync(originId, destinationId, departureDate);
        
        // Sıralama: Kalkış saatine göre
        journeys = journeys.OrderBy(j => j.JourneyInfo.Departure).ToList();

        var model = new JourneyIndexViewModel
        {
            Journeys = journeys,
            Search = searchModel
        };

        return View(model);
    }
}