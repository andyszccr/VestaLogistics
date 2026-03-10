namespace VestaLogistics.Web.Options;

/// <summary>
/// Configuración para el API de tipo de cambio de Hacienda Costa Rica.
/// https://api.hacienda.go.cr/indicadores/tc/dolar
/// </summary>
public class HaciendaTipoCambioOptions
{
    public const string SectionName = "HaciendaTipoCambio";

    /// <summary>URL del endpoint (ej. https://api.hacienda.go.cr/indicadores/tc/dolar).</summary>
    public string BaseUrl { get; set; } = "https://api.hacienda.go.cr/indicadores/tc/dolar";

    /// <summary>Timeout en segundos para las llamadas HTTP.</summary>
    public int TimeoutSeconds { get; set; } = 10;
}
