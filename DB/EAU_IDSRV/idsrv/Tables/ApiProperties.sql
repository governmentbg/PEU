CREATE TABLE [idsrv].[ApiProperties] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Key]           NVARCHAR (250)  NOT NULL,
    [Value]         NVARCHAR (2000) NOT NULL,
    [ApiResourceId] INT             NOT NULL,
    CONSTRAINT [PK_ApiProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiProperties_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [idsrv].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiProperties_ApiResourceId]
    ON [idsrv].[ApiProperties]([ApiResourceId] ASC);

