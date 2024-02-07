CREATE TABLE [idsrv].[ApiSecrets] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Description]   NVARCHAR (1000) NULL,
    [Value]         NVARCHAR (4000) NOT NULL,
    [Expiration]    DATETIME2 (7)   NULL,
    [Type]          NVARCHAR (250)  NOT NULL,
    [Created]       DATETIME2 (7)   NOT NULL,
    [ApiResourceId] INT             NOT NULL,
    CONSTRAINT [PK_ApiSecrets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiSecrets_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [idsrv].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiSecrets_ApiResourceId]
    ON [idsrv].[ApiSecrets]([ApiResourceId] ASC);

