using Microsoft.AspNetCore.Mvc;
using VestaLogistics.Business.Services;

namespace VestaLogistics.Web.Controllers;

public class HomeController : Controller
{
    private readonly ITipoCambioService _tipoCambioService;

    public HomeController(ITipoCambioService tipoCambioService)
    {
        _tipoCambioService = tipoCambioService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewData["TipoCambioCompra"] = await _tipoCambioService.GetTipoCambioCompraAsync(null, cancellationToken);
        ViewData["TipoCambioVenta"] = await _tipoCambioService.GetTipoCambioVentaAsync(null, cancellationToken);
        return View();
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Models.ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
    }
}
