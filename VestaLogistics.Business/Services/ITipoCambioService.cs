namespace VestaLogistics.Business.Services;

/// <summary>
/// Servicio para la obtención del tipo de cambio.
/// Esqueleto preparado para integración futura con el API del Banco Central de Costa Rica (BCCR).
/// </summary>
public interface ITipoCambioService
{
    /// <summary>
    /// Obtiene el tipo de cambio compra del dólar para la fecha indicada (o el vigente).
    /// </summary>
    Task<decimal> GetTipoCambioCompraAsync(DateTime? fecha = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el tipo de cambio venta del dólar para la fecha indicada (o el vigente).
    /// </summary>
    Task<decimal> GetTipoCambioVentaAsync(DateTime? fecha = null, CancellationToken cancellationToken = default);
}
