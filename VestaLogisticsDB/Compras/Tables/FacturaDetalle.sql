CREATE TABLE [Compras].[FacturaDetalle] (
    [DetalleID]               INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]               INT             NOT NULL,
    [FacturaID]               INT             NOT NULL,
    [ProductoID]              INT             NOT NULL,
    [Cantidad]                DECIMAL (18, 2) NOT NULL,
    [PrecioUnitarioFacturado] DECIMAL (18, 2) NOT NULL,
    [ImpuestoID]              INT             NULL,
    [MultaConfigID]           INT             NULL,
    [SubtotalLinea]           DECIMAL (18, 2) NOT NULL,
    [MontoIVA]                DECIMAL (18, 2) NOT NULL,
    [MontoMulta]              DECIMAL (18, 2) NOT NULL,
    [TotalLinea]              DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([DetalleID] ASC),
    CONSTRAINT [FK_FacturaDet_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_FacturaDet_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [Compras].[FacturaEncabezado] ([FacturaID]),
    CONSTRAINT [FK_FacturaDet_Impuesto] FOREIGN KEY ([ImpuestoID]) REFERENCES [Config].[ImpuestosConfig] ([ImpuestoID]),
    CONSTRAINT [FK_FacturaDet_MultaConfig] FOREIGN KEY ([MultaConfigID]) REFERENCES [Config].[MultasConfig] ([MultaConfigID]),
    CONSTRAINT [FK_FacturaDet_Producto] FOREIGN KEY ([ProductoID]) REFERENCES [Inventario].[Productos] ([ProductoID])
);

