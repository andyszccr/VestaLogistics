using VestaLogistics.Entities.Plataforma;

namespace VestaLogistics.Business.Services;

public interface IEmpresaService
{
    Task<Empresa?> GetByIdAsync(int empresaId, bool incluirInactivas = false, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Empresa>> GetAllAsync(bool incluirInactivas = false, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(string nombreComercial, string cedulaJuridica, int usuarioIdAuditoria, CancellationToken cancellationToken = default);
    Task UpdateAsync(int empresaId, string nombreComercial, bool estado, int usuarioIdAuditoria, CancellationToken cancellationToken = default);
    Task DeleteAsync(int empresaId, int usuarioIdAuditoria, CancellationToken cancellationToken = default);
}
