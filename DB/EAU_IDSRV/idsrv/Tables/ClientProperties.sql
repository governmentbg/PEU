CREATE TABLE [idsrv].[ClientProperties] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Key]      NVARCHAR (250)  NOT NULL,
    [Value]    NVARCHAR (2000) NOT NULL,
    [ClientId] INT             NOT NULL,
    CONSTRAINT [PK_ClientProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientProperties_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [idsrv].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientProperties_ClientId]
    ON [idsrv].[ClientProperties]([ClientId] ASC);

