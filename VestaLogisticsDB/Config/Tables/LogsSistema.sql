CREATE TABLE [Config].[LogsSistema] (
    [LogID]      INT          IDENTITY (1, 1) NOT NULL,
    [EmpresaID]  INT          NOT NULL,
    [FechaLog]   DATETIME     DEFAULT (getdate()) NULL,
    [Nivel]      VARCHAR (20) NOT NULL,
    [Mensaje]    TEXT         NOT NULL,
    [Stacktrace] TEXT         NULL,
    [UsuarioID]  INT          NULL,
    PRIMARY KEY CLUSTERED ([LogID] ASC),
    CONSTRAINT [FK_LogSist_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_LogSist_Usuario] FOREIGN KEY ([UsuarioID]) REFERENCES [Config].[Usuarios] ([UsuarioID])
);

