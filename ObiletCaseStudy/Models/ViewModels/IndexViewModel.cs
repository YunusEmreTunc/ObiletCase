namespace ObiletCaseStudy.Models.ViewModels;

public class IndexViewModel
{
    public int OriginId { get; set; }
    public int DestinationId { get; set; }
    public DateTime DepartureDate { get; set; } = DateTime.Now.AddDays(1);
    public List<BusLocation> Locations { get; set; }
}