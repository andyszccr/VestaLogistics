CREATE TABLE [Config].[AuditoríaAccesos] (
    [AccesoID]          INT          IDENTITY (1, 1) NOT NULL,
    [EmpresaID]         INT          NOT NULL,
    [UsuarioID]         INT          NULL,
    [UsernameIntentado] VARCHAR (50) NOT NULL,
    [FechaAcceso]       DATETIME     DEFAULT (getdate()) NULL,
    [DireccionIP]       VARCHAR (50) NULL,
    [Resultado]         VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([AccesoID] ASC),
    CONSTRAINT [FK_AuditAcceso_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_AuditAcceso_Usuario] FOREIGN KEY ([UsuarioID]) REFERENCES [Config].[Usuarios] ([UsuarioID])
);

