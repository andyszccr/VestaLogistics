CREATE TABLE [Compras].[FacturaEncabezado] (
    [FacturaID]          INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]          INT             NOT NULL,
    [NumeroFactura]      VARCHAR (50)    NOT NULL,
    [FechaEmision]       DATE            NOT NULL,
    [ProveedorID]        INT             NOT NULL,
    [MonedaID]           INT             NOT NULL,
    [TipoCambioAplicado] DECIMAL (12, 4) DEFAULT ((1.0)) NOT NULL,
    [UsuarioID]          INT             NOT NULL,
    [Estado]             VARCHAR (50)    DEFAULT ('Borrador') NULL,
    PRIMARY KEY CLUSTERED ([FacturaID] ASC),
    CONSTRAINT [FK_FacturaEnc_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_FacturaEnc_Moneda] FOREIGN KEY ([MonedaID]) REFERENCES [Config].[Monedas] ([MonedaID]),
    CONSTRAINT [FK_FacturaEnc_Proveedor] FOREIGN KEY ([ProveedorID]) REFERENCES [Compras].[Proveedores] ([ProveedorID]),
    CONSTRAINT [FK_FacturaEnc_Usuario] FOREIGN KEY ([UsuarioID]) REFERENCES [Config].[Usuarios] ([UsuarioID])
);

