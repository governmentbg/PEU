CREATE TABLE [rebus].[QT_EAU_PORTAL_API] (
    [id]         BIGINT             IDENTITY (1, 1) NOT NULL,
    [priority]   INT                NOT NULL,
    [expiration] DATETIMEOFFSET (7) NOT NULL,
    [visible]    DATETIMEOFFSET (7) NOT NULL,
    [headers]    VARBINARY (MAX)    NOT NULL,
    [body]       VARBINARY (MAX)    NOT NULL,
    CONSTRAINT [PK_rebus_QT_EAU_PORTAL_API] PRIMARY KEY CLUSTERED ([priority] ASC, [id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IDX_EXPIRATION_rebus_QT_EAU_PORTAL_API]
    ON [rebus].[QT_EAU_PORTAL_API]([expiration] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_RECEIVE_rebus_QT_EAU_PORTAL_API]
    ON [rebus].[QT_EAU_PORTAL_API]([priority] ASC, [visible] ASC, [expiration] ASC, [id] ASC);

