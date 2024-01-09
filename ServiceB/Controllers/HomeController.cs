namespace ServiceB;

using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class HomeController : ControllerBase
{
    #region Fields
    private ILogger<HomeController> _logger;
    #endregion

    #region Constructors/Finalizers

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Methods


    [HttpGet("a")]
    public async Task<IActionResult> GetA(CancellationToken cancellationToken)
    {
        _logger.LogDebug("a");
        return Ok("a");
    }

    [HttpGet("b")]
    public async Task<IActionResult> GetB(CancellationToken cancellationToken)
    {
        _logger.LogDebug("b");
        return Ok("b");
    }


    #endregion
}