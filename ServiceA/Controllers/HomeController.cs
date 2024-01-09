namespace ServiceB;

using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class HomeController : ControllerBase
{
    #region Fields
    private ILogger<HomeController> _logger;
    private IServicebClient _servicebClient;
    #endregion

    #region Constructors/Finalizers

    public HomeController(IServicebClient servicebClient, ILogger<HomeController> logger)
    {
        _logger = logger;
        _servicebClient = servicebClient;
    }

    #endregion

    #region Methods


    [HttpGet("a")]
    public async Task<IActionResult> GetPathA(CancellationToken cancellationToken)
    {
        _logger.LogDebug("a");
        return Ok(await _servicebClient.GetPathA(cancellationToken));
    }

    [HttpGet("b")]
    public async Task<IActionResult> GetPathB(CancellationToken cancellationToken)
    {
        _logger.LogDebug("b");
        return Ok(await _servicebClient.GetPathB(cancellationToken));
    }

    #endregion
}