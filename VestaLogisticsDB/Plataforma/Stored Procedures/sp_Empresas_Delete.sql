
CREATE PROCEDURE Plataforma.sp_Empresas_Delete
(
    @EmpresaID INT,
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

        -- SOFT DELETE: Cambiar el estado a Inactivo (0).
        -- Esto bloquea el acceso a todos los usuarios de esa empresa.
        UPDATE Plataforma.Empresas
        SET Estado = 0 -- Inactivo
        WHERE EmpresaID = @EmpresaID;
        
        PRINT 'Empresa ID ' + CAST(@EmpresaID AS VARCHAR) + ' desactivada exitosamente (Soft Delete).';

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessageD NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverityD INT = ERROR_SEVERITY();
        DECLARE @ErrorStateD INT = ERROR_STATE();
        
        ;THROW @ErrorSeverityD, @ErrorMessageD, @ErrorStateD;
    END CATCH
END