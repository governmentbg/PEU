CREATE TABLE [dbo].[versions] (
    [version_id] BIGINT             IDENTITY (1, 1) NOT NULL,
    [created_by] INT                NOT NULL,
    [created_on] DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [xpk_versions] PRIMARY KEY CLUSTERED ([version_id] ASC),
    CONSTRAINT [fk_versions_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id])
);

