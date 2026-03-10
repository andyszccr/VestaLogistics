


CREATE   PROCEDURE [dbo].[GenerarClaseCSharp]
    @NombreTabla  NVARCHAR(128),
    @SchemaName   NVARCHAR(128) = N'dbo',
    @NombreClase  NVARCHAR(128) = NULL,
    @Namespace    NVARCHAR(256) = N'SmartTag.Domain.Entities',
    @NullableRef  BIT           = 1
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ClaseCSharp		NVARCHAR(MAX) = N'';
    DECLARE @NombreColumna		NVARCHAR(128);
    DECLARE @TipoDato			NVARCHAR(128);
    DECLARE @EsNullable			VARCHAR(3);
    DECLARE @ClaseNombre		NVARCHAR(128);
    DECLARE @TipoCSharp			NVARCHAR(128);
    DECLARE @PropiedadNombre	NVARCHAR(128);
    DECLARE @Linea				NVARCHAR(MAX);
    DECLARE @EsStringNoNullable BIT;
    DECLARE @ResumenPropiedad	NVARCHAR(MAX);
    DECLARE @Col				NVARCHAR(256);
    DECLARE @Pos				INT;
    DECLARE @Parte				NVARCHAR(128);
    DECLARE @Pascal				NVARCHAR(512);

    -- IMPORTANTE: Crear y ejecutar este SP en la misma base de datos donde están las tablas (ej. USE SmartTag;).
    -- Resolver esquema por nombre de tabla en la base de datos actual
    SET @SchemaName = 
	(
        SELECT TOP 1 TABLE_SCHEMA
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_NAME = @NombreTabla
        ORDER BY TABLE_SCHEMA
    );
    
	IF @SchemaName IS NULL
    BEGIN
        SELECT N'Error: No se encontró la tabla [' + @NombreTabla + N']. Asegúrate de ejecutar en la BD correcta (ej. USE SmartTag;). Consulta: SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N''' + @NombreTabla + N''' ORDER BY TABLE_SCHEMA' AS ClaseGenerada;
        RETURN;
    END

    SET @ClaseNombre = ISNULL(NULLIF(LTRIM(RTRIM(@NombreClase)), N''), @NombreTabla);

    -- Encabezado
    SET @ClaseCSharp = N'namespace ' + @Namespace + N';' + NCHAR(13) + NCHAR(10) + NCHAR(10)
                     + N'/// <summary>' + NCHAR(13) + NCHAR(10)
                     + N'/// Entidad de dominio ' + @ClaseNombre + N'. Alineada con [' + @SchemaName + N'].[' + @NombreTabla + N'].' + NCHAR(13) + NCHAR(10)
                     + N'/// </summary>' + NCHAR(13) + NCHAR(10)
                     + N'public class ' + @ClaseNombre + NCHAR(13) + NCHAR(10)
                     + N'{' + NCHAR(13) + NCHAR(10);

    -- Columnas desde INFORMATION_SCHEMA (misma base de datos donde se ejecuta el SP)
    DECLARE ColCursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT
        c.COLUMN_NAME,
        c.DATA_TYPE,
        c.IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS c
    WHERE c.TABLE_SCHEMA = @SchemaName
      AND c.TABLE_NAME = @NombreTabla
    ORDER BY c.ORDINAL_POSITION;

    OPEN ColCursor;
    FETCH NEXT FROM ColCursor INTO @NombreColumna, @TipoDato, @EsNullable;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Mapeo SQL -> C#
        SET @TipoCSharp = CASE UPPER(LTRIM(RTRIM(@TipoDato)))
            WHEN 'INT'              THEN CASE WHEN @EsNullable = 'YES' THEN 'int?'			ELSE 'int'			END
            WHEN 'BIGINT'           THEN CASE WHEN @EsNullable = 'YES' THEN 'long?'			ELSE 'long'			END
            WHEN 'SMALLINT'         THEN CASE WHEN @EsNullable = 'YES' THEN 'short?'		ELSE 'short'		END
            WHEN 'TINYINT'          THEN CASE WHEN @EsNullable = 'YES' THEN 'byte?'			ELSE 'byte'			END
            WHEN 'BIT'              THEN CASE WHEN @EsNullable = 'YES' THEN 'bool?'			ELSE 'bool'			END
            WHEN 'DATETIME'         THEN CASE WHEN @EsNullable = 'YES' THEN 'DateTime?'		ELSE 'DateTime'		END
            WHEN 'DATETIME2'        THEN CASE WHEN @EsNullable = 'YES' THEN 'DateTime?'		ELSE 'DateTime'		END
            WHEN 'DATE'             THEN CASE WHEN @EsNullable = 'YES' THEN 'DateTime?'		ELSE 'DateTime'		END
            WHEN 'SMALLDATETIME'    THEN CASE WHEN @EsNullable = 'YES' THEN 'DateTime?'		ELSE 'DateTime'		END
            WHEN 'TIME'             THEN CASE WHEN @EsNullable = 'YES' THEN 'TimeSpan?'		ELSE 'TimeSpan'		END
            WHEN 'DECIMAL'          THEN CASE WHEN @EsNullable = 'YES' THEN 'decimal?'		ELSE 'decimal'		END
            WHEN 'NUMERIC'          THEN CASE WHEN @EsNullable = 'YES' THEN 'decimal?'		ELSE 'decimal'		END
            WHEN 'MONEY'            THEN CASE WHEN @EsNullable = 'YES' THEN 'decimal?'		ELSE 'decimal'		END
            WHEN 'SMALLMONEY'       THEN CASE WHEN @EsNullable = 'YES' THEN 'decimal?'		ELSE 'decimal'		END
            WHEN 'FLOAT'            THEN CASE WHEN @EsNullable = 'YES' THEN 'double?'		ELSE 'double'		END
            WHEN 'REAL'             THEN CASE WHEN @EsNullable = 'YES' THEN 'float?'		ELSE 'float'		END
            WHEN 'UNIQUEIDENTIFIER' THEN CASE WHEN @EsNullable = 'YES' THEN 'Guid?'			ELSE 'Guid'			END
            WHEN 'BINARY'           THEN 'byte[]'
            WHEN 'VARBINARY'        THEN 'byte[]'
            WHEN 'IMAGE'            THEN 'byte[]'
            WHEN 'CHAR'             THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'VARCHAR'          THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'NCHAR'            THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'NVARCHAR'         THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'TEXT'             THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'NTEXT'            THEN CASE WHEN @NullableRef = 1 AND @EsNullable = 'YES' THEN 'string?' ELSE 'string' END
            WHEN 'XML'              THEN 'string'
            WHEN 'TIMESTAMP'        THEN 'byte[]'
            WHEN 'SQL_VARIANT'      THEN 'object'
            ELSE N'object /* ' + @TipoDato + N' */'
        END;

        SET @EsStringNoNullable = 0;
        IF @TipoCSharp = N'string' SET @EsStringNoNullable = 1;

        -- PascalCase desde snake_case (password_hash -> PasswordHash)
        SET @Pascal = N'';
        SET @Col = LOWER(LTRIM(RTRIM(@NombreColumna))) + N'_';
        SET @Pos = CHARINDEX(N'_', @Col);
        WHILE @Pos > 0
        BEGIN
            SET @Parte = SUBSTRING(@Col, 1, @Pos - 1);
            IF LEN(@Parte) > 0
                SET @Pascal = @Pascal + UPPER(LEFT(@Parte, 1)) + SUBSTRING(@Parte, 2, 128);
            SET @Col = SUBSTRING(@Col, @Pos + 1, 256);
            SET @Pos = CHARINDEX(N'_', @Col);
        END;
        IF LEN(@Pascal) = 0
            SET @PropiedadNombre = UPPER(LEFT(@NombreColumna, 1)) + SUBSTRING(@NombreColumna, 2, 128);
        ELSE
            SET @PropiedadNombre = @Pascal;

        -- Conflicto genérico: si el nombre de la propiedad coincide con el de la clase, se evita shadowing en C#
        IF @PropiedadNombre = @ClaseNombre
        BEGIN
            SET @PropiedadNombre = @PropiedadNombre + N'Value';
            SET @ResumenPropiedad = N'    /// <summary>Columna: ' + @NombreColumna + N' (renombrada por conflicto con nombre de clase).</summary>' + NCHAR(13) + NCHAR(10);
        END
        ELSE
            SET @ResumenPropiedad = N'';

        SET @Linea = @ResumenPropiedad;

        IF @EsStringNoNullable = 1
            SET @Linea = @Linea + N'    public ' + @TipoCSharp + N' ' + @PropiedadNombre + N' { get; set; } = string.Empty;' + NCHAR(13) + NCHAR(10);
        ELSE
            SET @Linea = @Linea + N'    public ' + @TipoCSharp + N' ' + @PropiedadNombre + N' { get; set; }' + NCHAR(13) + NCHAR(10);

        SET @ClaseCSharp = @ClaseCSharp + @Linea;

        FETCH NEXT FROM ColCursor INTO @NombreColumna, @TipoDato, @EsNullable;
    END;

    CLOSE ColCursor;
    DEALLOCATE ColCursor;

    SET @ClaseCSharp = @ClaseCSharp + N'}' + NCHAR(13) + NCHAR(10);

    SELECT @ClaseCSharp AS ClaseGenerada;

	print @ClaseCSharp;
END