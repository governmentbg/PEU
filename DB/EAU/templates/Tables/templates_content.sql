CREATE TABLE [templates].[templates_content] (
    [template_content_id] INT                IDENTITY (1, 1) NOT NULL,
    [template_id]         INT                NOT NULL,
    [content]             VARBINARY (MAX)    NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [xpk_versions] PRIMARY KEY CLUSTERED ([template_content_id] ASC),
    CONSTRAINT [fk_templates_content_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_templates_content_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);

