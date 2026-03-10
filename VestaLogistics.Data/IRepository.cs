using System.Linq.Expressions;

namespace VestaLogistics.Data;

/// <summary>
/// Contrato del Patrón Repositorio genérico para operaciones CRUD.
/// La implementación base puede ejecutar Stored Procedures o usar DbSet según la entidad.
/// </summary>
/// <typeparam name="TEntity">Tipo de entidad (POCO).</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
