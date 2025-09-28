using System.Text.Json.Serialization;

public class JourneyResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("data")]
    public List<Journey> Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("user-message")]
    public string UserMessage { get; set; }
}

public class Journey
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("partner-id")]
    public int PartnerId { get; set; }

    [JsonPropertyName("partner-name")]
    public string PartnerName { get; set; }

    [JsonPropertyName("bus-type")]
    public string BusType { get; set; }

    [JsonPropertyName("total-seats")]
    public int TotalSeats { get; set; }

    [JsonPropertyName("available-seats")]
    public int AvailableSeats { get; set; }

    [JsonPropertyName("journey")]
    public JourneyInfo JourneyInfo { get; set; }

    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; }

    [JsonPropertyName("origin-location")]
    public string OriginLocation { get; set; }

    [JsonPropertyName("destination-location")]
    public string DestinationLocation { get; set; }

    [JsonPropertyName("is-active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("partner-rating")]
    public decimal? PartnerRating { get; set; }

    [JsonPropertyName("origin-location-id")]
    public int OriginLocationId { get; set; }

    [JsonPropertyName("destination-location-id")]
    public int DestinationLocationId { get; set; }

    // kolay erişim için computed property
    public decimal InternetPrice => JourneyInfo?.InternetPrice ?? 0;
}

public class JourneyInfo
{
    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    [JsonPropertyName("departure")]
    public DateTime Departure { get; set; }

    [JsonPropertyName("arrival")]
    public DateTime Arrival { get; set; }

    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    [JsonPropertyName("original-price")]
    public decimal OriginalPrice { get; set; }

    [JsonPropertyName("internet-price")]
    public decimal InternetPrice { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}

public class Feature
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}