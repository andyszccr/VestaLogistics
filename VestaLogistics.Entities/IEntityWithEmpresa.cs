namespace VestaLogistics.Entities;

/// <summary>
/// Marca las entidades que participan en el modelo Multitenant.
/// El DbContext aplicará un Global Query Filter por EmpresaID a todas las entidades que implementen esta interfaz.
/// </summary>
public interface IEntityWithEmpresa
{
    int EmpresaID { get; set; }
}
