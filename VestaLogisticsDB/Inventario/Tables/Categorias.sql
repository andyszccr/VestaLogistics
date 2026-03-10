CREATE TABLE [Inventario].[Categorias] (
    [CategoriaID] INT           IDENTITY (1, 1) NOT NULL,
    [EmpresaID]   INT           NOT NULL,
    [Nombre]      VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([CategoriaID] ASC),
    CONSTRAINT [FK_Categoria_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

