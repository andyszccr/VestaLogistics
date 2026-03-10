using VestaLogistics.Data;
using VestaLogistics.Entities.Plataforma;

namespace VestaLogistics.Business.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;

    public EmpresaService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public Task<Empresa?> GetByIdAsync(int empresaId, bool incluirInactivas = false, CancellationToken cancellationToken = default)
    {
        return _empresaRepository.GetByIdAsync(empresaId, incluirInactivas, cancellationToken);
    }

    public Task<IReadOnlyList<Empresa>> GetAllAsync(bool incluirInactivas = false, CancellationToken cancellationToken = default)
    {
        return _empresaRepository.GetAllAsync(incluirInactivas, cancellationToken);
    }

    public Task<int> CreateAsync(string nombreComercial, string cedulaJuridica, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        return _empresaRepository.InsertAsync(nombreComercial, cedulaJuridica, usuarioIdAuditoria, cancellationToken);
    }

    public Task UpdateAsync(int empresaId, string nombreComercial, bool estado, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        return _empresaRepository.UpdateAsync(empresaId, nombreComercial, estado, usuarioIdAuditoria, cancellationToken);
    }

    public Task DeleteAsync(int empresaId, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        return _empresaRepository.DeleteAsync(empresaId, usuarioIdAuditoria, cancellationToken);
    }
}
