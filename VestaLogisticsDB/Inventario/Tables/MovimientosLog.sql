CREATE TABLE [Inventario].[MovimientosLog] (
    [MovimientoID]    INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]       INT             NOT NULL,
    [SucursalID]      INT             NOT NULL,
    [ProductoID]      INT             NOT NULL,
    [TipoMovimiento]  VARCHAR (20)    NOT NULL,
    [EntidadID]       INT             NULL,
    [Cantidad]        DECIMAL (18, 2) NOT NULL,
    [SaldoAnterior]   DECIMAL (18, 2) NOT NULL,
    [SaldoNuevo]      DECIMAL (18, 2) NOT NULL,
    [FechaMovimiento] DATETIME        DEFAULT (getdate()) NULL,
    [UsuarioID]       INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([MovimientoID] ASC),
    CONSTRAINT [FK_MovLog_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_MovLog_Producto] FOREIGN KEY ([ProductoID]) REFERENCES [Inventario].[Productos] ([ProductoID]),
    CONSTRAINT [FK_MovLog_Sucursal] FOREIGN KEY ([SucursalID]) REFERENCES [Config].[Sucursales] ([SucursalID]),
    CONSTRAINT [FK_MovLog_Usuario] FOREIGN KEY ([UsuarioID]) REFERENCES [Config].[Usuarios] ([UsuarioID])
);

