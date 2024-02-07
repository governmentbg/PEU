CREATE TABLE [users].[users_h] (
    [user_id]             INT                NOT NULL,
    [user_ver_id]         BIGINT             NOT NULL,
    [cin]                 INT                NOT NULL,
    [email]               NVARCHAR (200)     NOT NULL,
    [status]              SMALLINT           NOT NULL,
    [is_system]           BIT                NOT NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_users_h_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [df_users_h_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_users_h] PRIMARY KEY CLUSTERED ([user_id] ASC, [user_ver_id] ASC),
    CONSTRAINT [ck_users_h_status] CHECK ([status]=(1) OR [status]=(2) OR [status]=(3) OR [status]=(4) OR [status]=(5)),
    CONSTRAINT [fk_users_h_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_users_h_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_users_h_uid] FOREIGN KEY ([user_id]) REFERENCES [users].[users] ([user_id])
);








GO
CREATE NONCLUSTERED INDEX [IDX_users_h_is_last]
    ON [users].[users_h]([user_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

