CREATE TABLE [Inventario].[Productos] (
    [ProductoID]         INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]          INT             NOT NULL,
    [CategoriaID]        INT             NULL,
    [Nombre]             VARCHAR (150)   NOT NULL,
    [Medida]             VARCHAR (50)    NOT NULL,
    [PrecioUnitarioBase] DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductoID] ASC),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([CategoriaID]) REFERENCES [Inventario].[Categorias] ([CategoriaID]),
    CONSTRAINT [FK_Producto_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

