public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext.Session;

    public DeviceSession GetDeviceSession()
    {
        return Session.GetObject<DeviceSession>("DeviceSession");
    }

    public void SetDeviceSession(DeviceSession deviceSession)
    {
        Session.SetObject("DeviceSession", deviceSession);
    }

    public SearchViewModel GetLastSearch()
    {
        return Session.GetObject<SearchViewModel>("LastSearch") ?? new SearchViewModel();
    }

    public void SetLastSearch(SearchViewModel search)
    {
        Session.SetObject("LastSearch", search);
    }
}