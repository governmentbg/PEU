CREATE TABLE [idsrv].[ClientCorsOrigins] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Origin]   NVARCHAR (150) NOT NULL,
    [ClientId] INT            NOT NULL,
    CONSTRAINT [PK_ClientCorsOrigins] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientCorsOrigins_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [idsrv].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientCorsOrigins_ClientId]
    ON [idsrv].[ClientCorsOrigins]([ClientId] ASC);

