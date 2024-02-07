CREATE TABLE [rebus].[QT_EAU_PORTAL_API_GG] (
    [id]         BIGINT             IDENTITY (1, 1) NOT NULL,
    [priority]   INT                NOT NULL,
    [expiration] DATETIMEOFFSET (7) NOT NULL,
    [visible]    DATETIMEOFFSET (7) NOT NULL,
    [headers]    VARBINARY (MAX)    NOT NULL,
    [body]       VARBINARY (MAX)    NOT NULL,
    CONSTRAINT [PK_rebus_QT_EAU_PORTAL_API_GG] PRIMARY KEY CLUSTERED ([priority] ASC, [id] ASC)
);

