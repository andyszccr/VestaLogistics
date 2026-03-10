CREATE TABLE [Config].[MultasConfig] (
    [MultaConfigID] INT            IDENTITY (1, 1) NOT NULL,
    [EmpresaID]     INT            NOT NULL,
    [Descripcion]   VARCHAR (100)  NOT NULL,
    [Porcentaje]    DECIMAL (5, 2) DEFAULT ((0.0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([MultaConfigID] ASC),
    CONSTRAINT [FK_MultaConfig_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

