CREATE PROCEDURE Plataforma.sp_Empresas_Insert
(
    @NombreComercial VARCHAR(200),
    @CedulaJuridica VARCHAR(20),
    @UsuarioID_Auditoria INT -- Quién hizo la acción (para un futuro Log)
)
AS
BEGIN
    SET NOCOUNT ON; -- Mejora rendimiento al no devolver contador de filas

    BEGIN TRY
        -- VALIDACIÓN: Verificar que la Cédula Jurídica no exista (POO: Integridad)
        IF EXISTS (SELECT 1 FROM Plataforma.Empresas WHERE CedulaJuridica = @CedulaJuridica)
        BEGIN
            ;THROW 50001, 'Error: Ya existe una empresa registrada con esa Cédula Jurídica.', 1;
        END

        -- INSERCIÓN: Usando los valores por defecto para FechaRegistro (getdate()) y Estado (1)
        INSERT INTO Plataforma.Empresas (NombreComercial, CedulaJuridica)
        VALUES (@NombreComercial, @CedulaJuridica);

        -- RETORNO: Devolver el ID generado (SCOPE_IDENTITY) para que C# sepa cuál es
        SELECT SCOPE_IDENTITY() AS NuevoEmpresaID;
        
        PRINT 'Empresa ''' + @NombreComercial + ''' insertada exitosamente.';

    END TRY
    BEGIN CATCH
        -- CAPA DE DATOS: Manejo centralizado de errores de SQL
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        ;THROW @ErrorSeverity, @ErrorMessage, @ErrorState; -- Relanzar el error para que la Capa de Negocio lo capture
    END CATCH
END