using VestaLogistics.Data.Apis;

namespace VestaLogistics.Business.Services;

/// <summary>
/// Implementación del servicio de tipo de cambio usando el API de Hacienda Costa Rica.
/// https://api.hacienda.go.cr/indicadores/tc/dolar
/// </summary>
public class TipoCambioService : ITipoCambioService
{
    private readonly ITipoCambioExternoClient _tipoCambioClient;
    private (decimal Compra, decimal Venta)? _cache;

    private const decimal DefaultCompra = 535.00m;
    private const decimal DefaultVenta = 545.00m;

    public TipoCambioService(ITipoCambioExternoClient tipoCambioClient)
    {
        _tipoCambioClient = tipoCambioClient;
    }

    public async Task<decimal> GetTipoCambioCompraAsync(DateTime? fecha = null, CancellationToken cancellationToken = default)
    {
        var (compra, _) = await ObtenerTipoCambioAsync(cancellationToken).ConfigureAwait(false);
        return compra;
    }

    public async Task<decimal> GetTipoCambioVentaAsync(DateTime? fecha = null, CancellationToken cancellationToken = default)
    {
        var (_, venta) = await ObtenerTipoCambioAsync(cancellationToken).ConfigureAwait(false);
        return venta;
    }

    private async Task<(decimal Compra, decimal Venta)> ObtenerTipoCambioAsync(CancellationToken cancellationToken)
    {
        if (_cache.HasValue)
            return _cache.Value;

        try
        {
            _cache = await _tipoCambioClient.GetTipoCambioAsync(cancellationToken).ConfigureAwait(false);
            return _cache.Value;
        }
        catch
        {
            return (DefaultCompra, DefaultVenta);
        }
    }
}
