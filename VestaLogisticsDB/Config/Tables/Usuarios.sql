CREATE TABLE [Config].[Usuarios] (
    [UsuarioID]      INT           IDENTITY (1, 1) NOT NULL,
    [EmpresaID]      INT           NOT NULL,
    [NombreCompleto] VARCHAR (150) NOT NULL,
    [Username]       VARCHAR (50)  NOT NULL,
    [PasswordHash]   VARCHAR (255) NOT NULL,
    [Rol]            VARCHAR (50)  NOT NULL,
    [Estado]         BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([UsuarioID] ASC),
    CONSTRAINT [FK_Usuario_Empresa] FOREIGN KEY ([EmpresaID]) REFERENCES [Plataforma].[Empresas] ([EmpresaID]),
    CONSTRAINT [UQ_Usuario_Username_Empresa] UNIQUE NONCLUSTERED ([Username] ASC, [EmpresaID] ASC)
);

