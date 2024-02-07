CREATE TABLE [idsrv].[ApiClaims] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Type]          NVARCHAR (200) NOT NULL,
    [ApiResourceId] INT            NOT NULL,
    CONSTRAINT [PK_ApiClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [idsrv].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiClaims_ApiResourceId]
    ON [idsrv].[ApiClaims]([ApiResourceId] ASC);

