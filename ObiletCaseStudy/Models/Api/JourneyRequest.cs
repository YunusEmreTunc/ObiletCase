using System.Text.Json.Serialization;

public class JourneyRequest
{
    [JsonPropertyName("device-session")]
    public DeviceSession DeviceSession { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; } = "tr-TR";

    [JsonPropertyName("data")]
    public JourneyData Data { get; set; }
}

public class JourneyData
{
    [JsonPropertyName("origin-id")]
    public int OriginId { get; set; }

    [JsonPropertyName("destination-id")]
    public int DestinationId { get; set; }

    [JsonPropertyName("departure-date")]
    public string DepartureDate { get; set; }
}

public class DeviceSession
{
    [JsonPropertyName("session-id")]
    public string SessionId { get; set; }

    [JsonPropertyName("device-id")]
    public string DeviceId { get; set; }
}