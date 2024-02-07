CREATE TABLE [idsrv].[IdentityProperties] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [Key]                NVARCHAR (250)  NOT NULL,
    [Value]              NVARCHAR (2000) NOT NULL,
    [IdentityResourceId] INT             NOT NULL,
    CONSTRAINT [PK_IdentityProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IdentityProperties_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [idsrv].[IdentityResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IdentityProperties_IdentityResourceId]
    ON [idsrv].[IdentityProperties]([IdentityResourceId] ASC);

