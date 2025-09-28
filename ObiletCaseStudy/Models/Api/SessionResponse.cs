using System.Text.Json.Serialization;

public class SessionResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("data")]
    public SessionData Data { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("user-message")]
    public string UserMessage { get; set; }
    
    [JsonPropertyName("api-request-id")]
    public string ApiRequestId { get; set; }
    
    [JsonPropertyName("controller")]
    public string Controller { get; set; }
}

public class SessionData
{
    [JsonPropertyName("session-id")]
    public string SessionId { get; set; }
    
    [JsonPropertyName("device-id")]
    public string DeviceId { get; set; }
}