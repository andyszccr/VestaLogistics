
CREATE PROCEDURE Plataforma.sp_Empresas_Select
(
    @EmpresaID INT = NULL, -- Si es NULL, devuelve todas
    @IncluirInactivas BIT = 0 -- 0: Solo activas (por defecto), 1: Todas
)
AS
BEGIN
    SET NOCOUNT ON;

    -- CONSULTA
    SELECT EmpresaID,
           NombreComercial,
           CedulaJuridica,
           FechaRegistro,
           Estado -- Muestra el estado actual
    FROM Plataforma.Empresas
    WHERE (@EmpresaID IS NULL OR EmpresaID = @EmpresaID) -- Filtro por ID (opcional)
      AND (@IncluirInactivas = 1 OR Estado = 1); -- Filtro por Estado (opcional)
END