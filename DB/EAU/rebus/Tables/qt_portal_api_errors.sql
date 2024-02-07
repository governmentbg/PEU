CREATE TABLE [rebus].[qt_portal_api_errors] (
    [id]         BIGINT             IDENTITY (1, 1) NOT NULL,
    [priority]   INT                NOT NULL,
    [expiration] DATETIMEOFFSET (7) NOT NULL,
    [visible]    DATETIMEOFFSET (7) NOT NULL,
    [headers]    VARBINARY (MAX)    NOT NULL,
    [body]       VARBINARY (MAX)    NOT NULL,
    CONSTRAINT [PK_rebus_qt_portal_api_errors] PRIMARY KEY CLUSTERED ([priority] ASC, [id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IDX_EXPIRATION_rebus_qt_portal_api_errors]
    ON [rebus].[qt_portal_api_errors]([expiration] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_RECEIVE_rebus_qt_portal_api_errors]
    ON [rebus].[qt_portal_api_errors]([priority] DESC, [visible] ASC, [id] ASC, [expiration] ASC);

