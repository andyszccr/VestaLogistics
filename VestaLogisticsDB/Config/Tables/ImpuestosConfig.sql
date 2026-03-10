CREATE TABLE [Config].[ImpuestosConfig] (
    [ImpuestoID]       INT            IDENTITY (1, 1) NOT NULL,
    [EmpresaID]        INT            NOT NULL,
    [Nombre]           VARCHAR (50)   NOT NULL,
    [Porcentaje]       DECIMAL (5, 2) DEFAULT ((13.0)) NOT NULL,
    [EsPredeterminado] BIT            DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([ImpuestoID] ASC),
    CONSTRAINT [FK_Impuesto_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

