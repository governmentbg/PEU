CREATE TABLE [templates].[templates_ver] (
    [template_id]         INT                NOT NULL,
    [template_ver_id]     BIGINT             NOT NULL,
    [name]                NVARCHAR (200)     NOT NULL,
    [status]              SMALLINT           NOT NULL,
    [is_in_use]           BIT                NOT NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_templates_ver_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [df_templates_ver_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_templates_ver] PRIMARY KEY CLUSTERED ([template_id] ASC, [template_ver_id] ASC),
    CONSTRAINT [ck_templates_ver_status] CHECK ([status]=(0) OR [status]=(1) OR [status]=(2) OR [status]=(3)),
    CONSTRAINT [fk_templates_ver_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_templates_ver_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO
CREATE NONCLUSTERED INDEX [ix_templates_ver_status]
    ON [templates].[templates_ver]([status] ASC)
    ON [INDEXES];


GO
CREATE UNIQUE NONCLUSTERED INDEX [ux_templates_ver_name]
    ON [templates].[templates_ver]([name] ASC)
    ON [INDEXES];

