namespace VestaLogistics.Business.Services;

/// <summary>
/// Implementación del servicio de tipo de cambio.
/// Preparado para integrar con el API del Banco Central de Costa Rica (BCCR).
/// Documentación API BCCR: https://www.bccr.fi.cr/seccion-indicadores-economicos/servicio-web
/// </summary>
public class TipoCambioService : ITipoCambioService
{
    // TODO: Inyectar IHttpClientFactory y configuración (URL API, indicadores BCCR)
    // Indicadores típicos: 317 (compra), 318 (venta)

    public Task<decimal> GetTipoCambioCompraAsync(DateTime? fecha = null, CancellationToken cancellationToken = default)
    {
        // Placeholder: retorno tipo de cambio fijo hasta integrar API BCCR
        return Task.FromResult(535.00m);
    }

    public Task<decimal> GetTipoCambioVentaAsync(DateTime? fecha = null, CancellationToken cancellationToken = default)
    {
        // Placeholder: retorno tipo de cambio fijo hasta integrar API BCCR
        return Task.FromResult(545.00m);
    }
}
