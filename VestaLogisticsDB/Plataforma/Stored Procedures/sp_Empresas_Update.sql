
CREATE PROCEDURE Plataforma.sp_Empresas_Update
(
    @EmpresaID INT,
    @NombreComercial VARCHAR(200),
    @Estado BIT,
    @UsuarioID_Auditoria INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- VALIDACIÓN: Verificar que la empresa exista
        IF NOT EXISTS (SELECT 1 FROM Plataforma.Empresas WHERE EmpresaID = @EmpresaID)
        BEGIN
            ;THROW 50002, 'Error: La empresa especificada no existe.', 1;
        END

        -- ACTUALIZACIÓN
        UPDATE Plataforma.Empresas
        SET NombreComercial = @NombreComercial,
            Estado = @Estado -- Permite activar/desactivar la empresa (Bloqueo SaaS)
        WHERE EmpresaID = @EmpresaID;
        
        PRINT 'Empresa ID ' + CAST(@EmpresaID AS VARCHAR) + ' actualizada exitosamente.';

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessageU NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverityU INT = ERROR_SEVERITY();
        DECLARE @ErrorStateU INT = ERROR_STATE();
        
        ;THROW @ErrorSeverityU, @ErrorMessageU, @ErrorStateU;
    END CATCH
END