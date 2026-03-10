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
        var query = _context.Empresas.AsNoTracking()
            .Where(e => e.EmpresaID == empresaId);
        if (!incluirInactivas)
            query = query.Where(e => e.Estado == true);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Empresa>> GetAllAsync(bool incluirInactivas = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _context.Database
                .SqlQueryRaw<Empresa>("EXEC Plataforma.sp_Empresas_Select")
                .ToListAsync(cancellationToken);
            return result;
        }
        catch (Exception ex) when (ex.InnerException != null)
        {
            // Expone el error real de SQL (conexión, base de datos, tabla) en lugar del mensaje genérico "transient failure".
            throw ex.InnerException;
        }
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
