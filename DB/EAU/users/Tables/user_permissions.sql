CREATE TABLE [users].[user_permissions] (
    [user_id]       INT                NOT NULL,
    [permission_id] INT                NOT NULL,
    [is_active]     BIT                NOT NULL,
    [created_by]    INT                NOT NULL,
    [created_on]    DATETIMEOFFSET (3) CONSTRAINT [df_user_permissions_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]    INT                NOT NULL,
    [updated_on]    DATETIMEOFFSET (3) CONSTRAINT [df_user_permissions_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [fk_user_permissions_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_permissions_prmid] FOREIGN KEY ([permission_id]) REFERENCES [users].[n_s_permissions] ([permission_id]),
    CONSTRAINT [fk_user_permissions_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_permissions_uid] FOREIGN KEY ([user_id]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [IDX_user_permissions_user_id]
    ON [users].[user_permissions]([user_id] ASC)
    ON [INDEXES];

