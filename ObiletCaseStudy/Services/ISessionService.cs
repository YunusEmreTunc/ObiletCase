public interface ISessionService
{
    DeviceSession GetDeviceSession();
    void SetDeviceSession(DeviceSession deviceSession);
    SearchViewModel GetLastSearch();
    void SetLastSearch(SearchViewModel search);
}