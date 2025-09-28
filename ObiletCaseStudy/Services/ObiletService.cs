using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ObiletService : IObiletService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ISessionService _sessionService;

    public ObiletService(HttpClient httpClient, IConfiguration configuration, ISessionService sessionService)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _sessionService = sessionService;
        
        var apiToken = _configuration["ObiletApi:ApiClientToken"];
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiToken);
        _httpClient.BaseAddress = new Uri(_configuration["ObiletApi:BaseUrl"]);
        
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<DeviceSession> GetSessionAsync()
{
    var existingSession = _sessionService.GetDeviceSession();
    if (existingSession != null)
        return existingSession;

    var realIpAddress = await GetRealIpAddressAsync();
    Console.WriteLine($"IP: {realIpAddress}");

    var request = new SessionRequest
    {
        Type = 7,
        Connection = new SessionRequest.ConnectionInfo 
        { 
            IpAddress = realIpAddress,
            Port = "80"
        },
        Browser = new SessionRequest.BrowserInfo
        {
            Name = "Chrome",
            Version = "120.0.0.0"
        },
        Application = new SessionRequest.ApplicationInfo 
        { 
            Version = "1.0.0.0",
            EquipmentId = "case"
        }
    };

    try
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var jsonContent = JsonSerializer.Serialize(request, jsonOptions);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        Console.WriteLine($"Request JSON: {jsonContent}");

        var response = await _httpClient.PostAsync("client/getsession", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine($"Response Status: {(int)response.StatusCode}");
        Console.WriteLine($"Response Content: {responseContent}");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API Hata {response.StatusCode}: {responseContent}");
        }

        var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(responseContent, jsonOptions);
        
        if (sessionResponse?.Data == null)
        {
            throw new Exception($"Geçersiz response: {responseContent}");
        }

        var deviceSession = new DeviceSession
        {
            SessionId = sessionResponse.Data.SessionId,
            DeviceId = sessionResponse.Data.DeviceId
        };

        _sessionService.SetDeviceSession(deviceSession);
        return deviceSession;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
        throw;
    }
}

    public async Task<List<BusLocation>> GetBusLocationsAsync(string searchText = null)
    {
        var deviceSession = await GetSessionAsync();
        
        var request = new BusLocationRequest
        {
            Data = searchText,
            DeviceSession = deviceSession,
            Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            Language = "tr-TR"
        };

        var response = await _httpClient.PostAsJsonAsync("location/getbuslocations", request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API Hata: {response.StatusCode} - {errorContent}");
        }

        var locationResponse = await response.Content.ReadFromJsonAsync<BusLocationResponse>();
        return locationResponse.Data ?? new List<BusLocation>();
    }

    public async Task<List<Journey>> GetBusJourneysAsync(int originId, int destinationId, DateTime departureDate)
    {
        var deviceSession = await GetSessionAsync();
        
        var request = new JourneyRequest
        {
            DeviceSession = deviceSession,
            Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            Language = "tr-TR",
            Data = new JourneyData
            {
                OriginId = originId,
                DestinationId = destinationId,
                DepartureDate = departureDate.ToString("yyyy-MM-dd")
            }
        };

        var response = await _httpClient.PostAsJsonAsync("journey/getbusjourneys", request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API Hata: {response.StatusCode} - {errorContent}");
        }

        var journeyResponse = await response.Content.ReadFromJsonAsync<JourneyResponse>();
        return journeyResponse.Data ?? new List<Journey>();
    }
    
    private async Task<string> GetRealIpAddressAsync()
    {
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(5);
        
            var response = await client.GetStringAsync("https://checkip.amazonaws.com");
            return response.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"IP adresi alınamadı: {ex.Message}");
            return "unknown";
        }
    }
}