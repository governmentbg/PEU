CREATE TABLE [users].[users_audit] (
    [audit_id]   INT                NOT NULL,
    [user_id]    INT                NULL,
    [user_data]  NVARCHAR (MAX)     NULL,
    [created_by] INT                NOT NULL,
    [created_on] DATETIMEOFFSET (3) CONSTRAINT [df_users_audit_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_users_audit] PRIMARY KEY CLUSTERED ([audit_id] ASC),
    CONSTRAINT [fk_users_audit_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [IDX_users_audit_user_id]
    ON [users].[users_audit]([user_id] ASC)
    ON [INDEXES];

