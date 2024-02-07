CREATE TABLE [idsrv].[ClientPostLogoutRedirectUris] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [PostLogoutRedirectUri] NVARCHAR (2000) NOT NULL,
    [ClientId]              INT             NOT NULL,
    CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [idsrv].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientPostLogoutRedirectUris_ClientId]
    ON [idsrv].[ClientPostLogoutRedirectUris]([ClientId] ASC);

