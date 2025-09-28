using System.Text.Json.Serialization;

public class BusLocationRequest
{
        public string Data { get; set; }
    
        [JsonPropertyName("device-session")]
        public DeviceSession DeviceSession { get; set; }
    
        public string Date { get; set; }
        public string Language { get; set; } = "tr-TR";
}