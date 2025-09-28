public class BusLocationResponse
{
    public string Status { get; set; }
    public List<BusLocation> Data { get; set; }
}

public class BusLocation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CityName { get; set; }
    public int? Rank { get; set; }
}