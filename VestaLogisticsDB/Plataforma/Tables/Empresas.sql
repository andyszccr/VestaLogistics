CREATE TABLE [Plataforma].[Empresas] (
    [EmpresaID]       INT           IDENTITY (1, 1) NOT NULL,
    [NombreComercial] VARCHAR (200) NOT NULL,
    [CedulaJuridica]  VARCHAR (20)  NOT NULL,
    [FechaRegistro]   DATETIME      DEFAULT (getdate()) NULL,
    [Estado]          BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([EmpresaID] ASC),
    UNIQUE NONCLUSTERED ([CedulaJuridica] ASC)
);

