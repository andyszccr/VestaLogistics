namespace VestaLogistics.Data;

/// <summary>
/// Proporciona el identificador del tenant (Empresa) actual en el contexto de la petición.
/// La implementación en Web puede leer de HttpContext, claims o sesión.
/// </summary>
public interface ITenantContext
{
    int? EmpresaId { get; }
}
