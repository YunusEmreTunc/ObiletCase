using System.Text.Json.Serialization;

public class SessionRequest
{
    [JsonPropertyName("type")]
    public int Type { get; set; } = 7;
    
    [JsonPropertyName("connection")]
    public ConnectionInfo Connection { get; set; }
    
    [JsonPropertyName("browser")]
    public BrowserInfo Browser { get; set; }
    
    [JsonPropertyName("application")]
    public ApplicationInfo Application { get; set; }

    public class ConnectionInfo
    {
        [JsonPropertyName("ip-address")]
        public string IpAddress { get; set; }
        
        [JsonPropertyName("port")]
        public string Port { get; set; }
    }
    
    public class BrowserInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
    
    public class ApplicationInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0.0.0";
        
        [JsonPropertyName("equipment-id")]
        public string EquipmentId { get; set; } = "distribusion";
    }
}