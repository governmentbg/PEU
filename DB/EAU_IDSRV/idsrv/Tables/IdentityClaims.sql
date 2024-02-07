CREATE TABLE [idsrv].[IdentityClaims] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Type]               NVARCHAR (200) NOT NULL,
    [IdentityResourceId] INT            NOT NULL,
    CONSTRAINT [PK_IdentityClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IdentityClaims_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [idsrv].[IdentityResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IdentityClaims_IdentityResourceId]
    ON [idsrv].[IdentityClaims]([IdentityResourceId] ASC);

