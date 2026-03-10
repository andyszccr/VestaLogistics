using Microsoft.EntityFrameworkCore;
using VestaLogistics.Entities.Plataforma;

namespace VestaLogistics.Data;

/// <summary>
/// Implementación de IEmpresaRepository usando los Stored Procedures Plataforma.sp_Empresas_*.
/// </summary>
public class EmpresaRepository : IEmpresaRepository
{
    private readonly VestaLogisticsDbContext _context;

    public EmpresaRepository(VestaLogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Empresa?> GetByIdAsync(int empresaId, bool incluirInactivas = false, CancellationToken cancellationToken = default)
    {
        var list = await _context.Empresas
            .FromSqlRaw(
                "EXEC Plataforma.sp_Empresas_Select @EmpresaID = {0}, @IncluirInactivas = {1}",
                empresaId,
                incluirInactivas ? 1 : 0)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return list.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Empresa>> GetAllAsync(bool incluirInactivas = false, CancellationToken cancellationToken = default)
    {
        return await _context.Empresas
            .FromSqlRaw(
                "EXEC Plataforma.sp_Empresas_Select @EmpresaID = NULL, @IncluirInactivas = {0}",
                incluirInactivas ? 1 : 0)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> InsertAsync(string nombreComercial, string cedulaJuridica, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        var result = await _context.Database
            .SqlQueryRaw<int>("EXEC Plataforma.sp_Empresas_Insert @NombreComercial = {0}, @CedulaJuridica = {1}, @UsuarioID_Auditoria = {2}",
                nombreComercial, cedulaJuridica, usuarioIdAuditoria)
            .ToListAsync(cancellationToken);
        return result.FirstOrDefault();
    }

    public async Task UpdateAsync(int empresaId, string nombreComercial, bool estado, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC Plataforma.sp_Empresas_Update @EmpresaID = {0}, @NombreComercial = {1}, @Estado = {2}, @UsuarioID_Auditoria = {3}",
            empresaId, nombreComercial, estado ? 1 : 0, usuarioIdAuditoria,
            cancellationToken);
    }

    public async Task DeleteAsync(int empresaId, int usuarioIdAuditoria, CancellationToken cancellationToken = default)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC Plataforma.sp_Empresas_Delete @EmpresaID = {0}, @UsuarioID_Auditoria = {1}",
            empresaId, usuarioIdAuditoria,
            cancellationToken);
    }
}
