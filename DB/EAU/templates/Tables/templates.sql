CREATE TABLE [templates].[templates] (
    [template_id] INT                NOT NULL,
    [name]        NVARCHAR (200)     NOT NULL,
    [status]      SMALLINT           NOT NULL,
    [is_in_use]   BIT                NOT NULL,
    [created_by]  INT                NOT NULL,
    [created_on]  DATETIMEOFFSET (3) CONSTRAINT [df_templates_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]  INT                NOT NULL,
    [updated_on]  DATETIMEOFFSET (3) CONSTRAINT [df_templates_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_templates] PRIMARY KEY CLUSTERED ([template_id] ASC),
    CONSTRAINT [ck_templates_status] CHECK ([status]=(0) OR [status]=(1) OR [status]=(2) OR [status]=(3)),
    CONSTRAINT [fk_templates_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_templates_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO
CREATE NONCLUSTERED INDEX [ix_templates_status]
    ON [templates].[templates]([status] ASC)
    ON [INDEXES];


GO
CREATE UNIQUE NONCLUSTERED INDEX [ux_templates_name]
    ON [templates].[templates]([name] ASC)
    ON [INDEXES];

