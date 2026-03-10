CREATE TABLE [Plataforma].[Suscripciones] (
    [SuscripcionID] INT          IDENTITY (1, 1) NOT NULL,
    [EmpresaID]     INT          NOT NULL,
    [Planes]        VARCHAR (50) NOT NULL,
    [FechaInicio]   DATE         NOT NULL,
    [FechaFin]      DATE         NULL,
    [Estado]        VARCHAR (20) DEFAULT ('Activa') NULL,
    PRIMARY KEY CLUSTERED ([SuscripcionID] ASC),
    CONSTRAINT [FK_Suscripcion_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID])
);

