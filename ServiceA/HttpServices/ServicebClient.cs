namespace HttpServices;
public class ServicebClient : IServicebClient
{
    #region Fields
    private readonly HttpClient _httpClient;
    private readonly ILogger<ServicebClient> _logger;
    #endregion

    public ServicebClient(HttpClient httpClient, ILogger<ServicebClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetPathA(CancellationToken cancellationToken)
    {
        try
        {
            return await _httpClient.GetStringAsync("a", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ServiceA call failed");
            throw;
        }
    }

    public async Task<string> GetPathB(CancellationToken cancellationToken)
    {

        try
        {
            return await _httpClient.GetStringAsync("b", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ServiceB call failed");
            throw;
        }
    }
}