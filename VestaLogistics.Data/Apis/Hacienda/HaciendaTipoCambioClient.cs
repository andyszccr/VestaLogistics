using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using VestaLogistics.Data.Apis;

namespace VestaLogistics.Data.Apis.Hacienda;

/// <summary>
/// Cliente para el API de tipo de cambio de Hacienda Costa Rica.
/// Respuesta: { "venta": { "fecha": "...", "valor": 476.82 }, "compra": { "fecha": "...", "valor": 467.59 } }
/// </summary>
public class HaciendaTipoCambioClient : ITipoCambioExternoClient
{
    private readonly HttpClient _httpClient;
    private readonly HaciendaTipoCambioOptions _options;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public HaciendaTipoCambioClient(HttpClient httpClient, IOptions<HaciendaTipoCambioOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    public async Task<(decimal Compra, decimal Venta)> GetTipoCambioAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(_options.BaseUrl, cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<HaciendaTipoCambioDto>(JsonOptions, cancellationToken).ConfigureAwait(false)
            ?? throw new InvalidOperationException("La respuesta del API de Hacienda no contiene datos.");

        return (dto.Compra.Valor, dto.Venta.Valor);
    }

    private sealed class HaciendaTipoCambioDto
    {
        public HaciendaValorDto Compra { get; set; } = null!;
        public HaciendaValorDto Venta { get; set; } = null!;
    }

    private sealed class HaciendaValorDto
    {
        public string Fecha { get; set; } = "";
        public decimal Valor { get; set; }
    }
}
