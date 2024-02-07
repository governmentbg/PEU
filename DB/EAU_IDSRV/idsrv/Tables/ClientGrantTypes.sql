CREATE TABLE [idsrv].[ClientGrantTypes] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [GrantType] NVARCHAR (250) NOT NULL,
    [ClientId]  INT            NOT NULL,
    CONSTRAINT [PK_ClientGrantTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientGrantTypes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [idsrv].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientGrantTypes_ClientId]
    ON [idsrv].[ClientGrantTypes]([ClientId] ASC);

