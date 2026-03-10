CREATE TABLE [Config].[Monedas] (
    [MonedaID]    INT             IDENTITY (1, 1) NOT NULL,
    [EmpresaID]   INT             NOT NULL,
    [Codigo]      VARCHAR (10)    NOT NULL,
    [Nombre]      VARCHAR (50)    NOT NULL,
    [TipoCambio]  DECIMAL (12, 4) DEFAULT ((1.0)) NOT NULL,
    [EsPrincipal] BIT             DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([MonedaID] ASC),
    CONSTRAINT [FK_Moneda_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [UQ_Moneda_Codigo_Empresa] UNIQUE NONCLUSTERED ([Codigo] ASC, [EmpresaID] ASC)
);

