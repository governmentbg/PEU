CREATE TABLE [idsrv].[ApiScopeClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Type]       NVARCHAR (200) NOT NULL,
    [ApiScopeId] INT            NOT NULL,
    CONSTRAINT [PK_ApiScopeClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiScopeClaims_ApiScopes_ApiScopeId] FOREIGN KEY ([ApiScopeId]) REFERENCES [idsrv].[ApiScopes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiScopeClaims_ApiScopeId]
    ON [idsrv].[ApiScopeClaims]([ApiScopeId] ASC);

