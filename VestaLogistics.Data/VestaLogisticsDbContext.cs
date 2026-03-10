using Microsoft.EntityFrameworkCore;
using VestaLogistics.Entities;
using VestaLogistics.Entities.Plataforma;

namespace VestaLogistics.Data;

/// <summary>
/// DbContext principal con soporte Multitenant mediante Global Query Filters por EmpresaID.
/// Las entidades que implementen IEntityWithEmpresa se filtran automáticamente por el tenant actual.
/// </summary>
public class VestaLogisticsDbContext : DbContext
{
    private readonly ITenantContext? _tenantContext;

    public VestaLogisticsDbContext(DbContextOptions<VestaLogisticsDbContext> options)
        : base(options)
    {
        _tenantContext = null;
    }

    /// <summary>
    /// Constructor para Multitenancy: el filtro global usa ITenantContext.EmpresaId en cada consulta.
    /// </summary>
    public VestaLogisticsDbContext(DbContextOptions<VestaLogisticsDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <summary>
    /// EmpresaID del tenant actual (para uso en expresiones del Global Query Filter).
    /// </summary>
    internal int CurrentEmpresaId => _tenantContext?.EmpresaId ?? 0;

    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Sucursales> Sucursales => Set<Sucursales>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la tabla Empresas (esquema Plataforma)
        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.ToTable("Empresas", "Plataforma");
            entity.HasKey(e => e.EmpresaID);
            entity.Property(e => e.NombreComercial).HasMaxLength(200);
            entity.Property(e => e.CedulaJuridica).HasMaxLength(20);
        });

        // Configuración de la tabla Sucursales (esquema Config)
        modelBuilder.Entity<Sucursales>(entity =>
        {
            entity.ToTable("Sucursales", "Config");
            entity.HasKey(e => e.SucursalID);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.HasOne<Empresa>()
                .WithMany()
                .HasForeignKey(e => e.EmpresaID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Global Query Filter para todas las entidades que implementan IEntityWithEmpresa
        ApplyGlobalQueryFilters(modelBuilder);
    }

    private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntityWithEmpresa).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                var property = System.Linq.Expressions.Expression.Property(parameter, nameof(IEntityWithEmpresa.EmpresaID));
                // Usar CurrentEmpresaId del DbContext para que el filtro se evalúe en runtime por request
                var contextAccess = System.Linq.Expressions.Expression.Property(
                    System.Linq.Expressions.Expression.Constant(this),
                    nameof(CurrentEmpresaId));
                var equality = System.Linq.Expressions.Expression.Equal(property, contextAccess);
                var lambda = System.Linq.Expressions.Expression.Lambda(equality, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
