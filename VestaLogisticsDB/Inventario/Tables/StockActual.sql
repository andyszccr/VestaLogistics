CREATE TABLE [Inventario].[StockActual] (
    [StockID]          INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]        INT             NOT NULL,
    [SucursalID]       INT             NOT NULL,
    [ProductoID]       INT             NOT NULL,
    [Cantidad]         DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [UbicacionPasillo] VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([StockID] ASC),
    CONSTRAINT [FK_Stock_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_Stock_Producto] FOREIGN KEY ([ProductoID]) REFERENCES [Inventario].[Productos] ([ProductoID]),
    CONSTRAINT [FK_Stock_Sucursal] FOREIGN KEY ([SucursalID]) REFERENCES [Config].[Sucursales] ([SucursalID]),
    CONSTRAINT [UQ_Stock_Sucursal_Producto] UNIQUE NONCLUSTERED ([SucursalID] ASC, [ProductoID] ASC)
);

