using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VestaLogistics.Data;
using VestaLogistics.Data.Apis;
using VestaLogistics.Data.Apis.Hacienda;

namespace VestaLogistics.Business.Extensions;

/// <summary>
/// Extensiones para registrar servicios de la capa Data (DbContext, repositorios, APIs externas) desde Business.
/// Web solo referencia Business; Business referencia Data y expone el registro. Los valores fluyen Data → Business → Web.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra DbContext y repositorios de VestaLogistics (capa Data).
    /// </summary>
    public static IServiceCollection AddVestaLogisticsData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<VestaLogisticsDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        return services;
    }

    /// <summary>
    /// Registra el cliente del API de tipo de cambio de Hacienda (Data) y su configuración.
    /// La configuración se lee de la sección "HaciendaTipoCambio" (ej. en appsettings.json).
    /// </summary>
    public static IServiceCollection AddHaciendaTipoCambio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HaciendaTipoCambioOptions>(
            configuration.GetSection(HaciendaTipoCambioOptions.SectionName));
        services.AddHttpClient<ITipoCambioExternoClient, HaciendaTipoCambioClient>();
        return services;
    }
}
