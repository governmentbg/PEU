CREATE TABLE [users].[users] (
    [user_id]    INT                NOT NULL,
    [email]      NVARCHAR (200)     NOT NULL,
    [status]     SMALLINT           NOT NULL,
    [is_system]  BIT                NOT NULL,
    [created_by] INT                NOT NULL,
    [created_on] DATETIMEOFFSET (3) CONSTRAINT [df_users_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by] INT                NOT NULL,
    [updated_on] DATETIMEOFFSET (3) CONSTRAINT [df_users_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [cin]        INT                NULL,
    CONSTRAINT [xpk_users] PRIMARY KEY CLUSTERED ([user_id] ASC),
    CONSTRAINT [ck_users_status] CHECK ([status]=(1) OR [status]=(2) OR [status]=(3) OR [status]=(4) OR [status]=(5)),
    CONSTRAINT [fk_users_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_users_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
CREATE NONCLUSTERED INDEX [IDX_users_status]
    ON [users].[users]([status] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_users_email]
    ON [users].[users]([email] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_users_cin]
    ON [users].[users]([cin] ASC)
    ON [INDEXES];

