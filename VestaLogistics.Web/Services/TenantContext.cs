using System.Security.Claims;
using VestaLogistics.Data;

namespace VestaLogistics.Web.Services;

/// <summary>
/// Resuelve el EmpresaID del tenant actual desde el contexto HTTP (claims, cookie, header, etc.).
/// Por defecto lee el claim "EmpresaID"; puede extenderse a subdominio o cabecera X-Tenant-ID.
/// </summary>
public class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? EmpresaId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var claim = user?.FindFirst("EmpresaID")?.Value;
            if (int.TryParse(claim, out var id))
                return id;
            // Opcional: leer de header, subdominio o sesión
            var header = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-ID"].FirstOrDefault();
            if (int.TryParse(header, out var headerId))
                return headerId;
            return null;
        }
    }
}
