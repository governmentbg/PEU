CREATE TABLE [idsrv].[ClientSecrets] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (2000) NULL,
    [Value]       NVARCHAR (4000) NOT NULL,
    [Expiration]  DATETIME2 (7)   NULL,
    [Type]        NVARCHAR (250)  NOT NULL,
    [Created]     DATETIME2 (7)   NOT NULL,
    [ClientId]    INT             NOT NULL,
    CONSTRAINT [PK_ClientSecrets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientSecrets_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [idsrv].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientSecrets_ClientId]
    ON [idsrv].[ClientSecrets]([ClientId] ASC);

