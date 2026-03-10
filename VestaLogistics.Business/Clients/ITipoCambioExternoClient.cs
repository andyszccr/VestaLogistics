namespace VestaLogistics.Business.Clients;

/// <summary>
/// Cliente para obtener tipo de cambio desde una API externa (ej. Hacienda Costa Rica).
/// </summary>
public interface ITipoCambioExternoClient
{
    /// <summary>
    /// Obtiene el tipo de cambio compra y venta vigente.
    /// </summary>
    Task<(decimal Compra, decimal Venta)> GetTipoCambioAsync(CancellationToken cancellationToken = default);
}
