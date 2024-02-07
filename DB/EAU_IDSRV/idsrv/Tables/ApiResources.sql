CREATE TABLE [idsrv].[ApiResources] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Enabled]      BIT             NOT NULL,
    [Name]         NVARCHAR (200)  NOT NULL,
    [DisplayName]  NVARCHAR (200)  NULL,
    [Description]  NVARCHAR (1000) NULL,
    [Created]      DATETIME2 (7)   NOT NULL,
    [Updated]      DATETIME2 (7)   NULL,
    [LastAccessed] DATETIME2 (7)   NULL,
    [NonEditable]  BIT             NOT NULL,
    CONSTRAINT [PK_ApiResources] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ApiResources_Name]
    ON [idsrv].[ApiResources]([Name] ASC);

