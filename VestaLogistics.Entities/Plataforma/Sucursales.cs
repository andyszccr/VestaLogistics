using VestaLogistics.Entities;

namespace VestaLogistics.Entities.Plataforma;

/// <summary>
/// POCO que representa la tabla Config.Sucursales.
/// Implementa IEntityWithEmpresa para filtro multitenant.
/// </summary>
public class Sucursales : IEntityWithEmpresa
{
    public int SucursalID { get; set; }
    public int EmpresaID { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
