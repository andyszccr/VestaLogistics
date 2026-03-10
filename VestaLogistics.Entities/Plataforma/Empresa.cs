namespace VestaLogistics.Entities.Plataforma;

/// <summary>
/// POCO que representa la tabla Plataforma.Empresas.
/// No implementa IEntityWithEmpresa porque es la entidad raíz del tenant.
/// </summary>
public class Empresa
{
    public int EmpresaID { get; set; }
    public string NombreComercial { get; set; } = string.Empty;
    public string CedulaJuridica { get; set; } = string.Empty;
    public DateTime? FechaRegistro { get; set; }
    public bool? Estado { get; set; }
}
