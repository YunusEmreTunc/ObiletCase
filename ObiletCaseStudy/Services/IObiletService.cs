public interface IObiletService
{
    Task<DeviceSession> GetSessionAsync();
    Task<List<BusLocation>> GetBusLocationsAsync(string searchText = null);
    Task<List<Journey>> GetBusJourneysAsync(int originId, int destinationId, DateTime departureDate);
}