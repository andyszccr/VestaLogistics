CREATE TABLE [Config].[Sucursales] (
    [SucursalID] INT           IDENTITY (1, 1) NOT NULL,
    [EmpresaID]  INT           NOT NULL,
    [Nombre]     VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([SucursalID] ASC),
    CONSTRAINT [FK_Sucursal_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

