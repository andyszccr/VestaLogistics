CREATE TABLE [Compras].[Proveedores] (
    [ProveedorID]    INT           IDENTITY (1, 1) NOT NULL,
    [EmpresaID]      INT           NOT NULL,
    [RazonSocial]    VARCHAR (200) NOT NULL,
    [CedulaJuridica] VARCHAR (20)  NOT NULL,
    [Email]          VARCHAR (100) NULL,
    [CuentaIban]     VARCHAR (34)  NULL,
    PRIMARY KEY CLUSTERED ([ProveedorID] ASC),
    CONSTRAINT [FK_Proveedor_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [UQ_Proveedor_Cedula_Empresa] UNIQUE NONCLUSTERED ([CedulaJuridica] ASC, [EmpresaID] ASC)
);

