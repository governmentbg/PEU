CREATE TABLE [idsrv].[AuditLog] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Event]                 NVARCHAR (MAX) NULL,
    [Source]                NVARCHAR (MAX) NULL,
    [Category]              NVARCHAR (MAX) NULL,
    [SubjectIdentifier]     NVARCHAR (MAX) NULL,
    [SubjectName]           NVARCHAR (MAX) NULL,
    [SubjectType]           NVARCHAR (MAX) NULL,
    [SubjectAdditionalData] NVARCHAR (MAX) NULL,
    [Action]                NVARCHAR (MAX) NULL,
    [Data]                  NVARCHAR (MAX) NULL,
    [Created]               DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

