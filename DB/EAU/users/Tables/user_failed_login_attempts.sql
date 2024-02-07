CREATE TABLE [users].[user_failed_login_attempts] (
    [attempt_id]            INT                NOT NULL,
    [authentication_type]   TINYINT            NOT NULL,
    [login_name]            NVARCHAR (200)     NULL,
    [failed_login_attempts] INT                CONSTRAINT [df_user_failed_login_attempts_count] DEFAULT ((0)) NOT NULL,
    [is_active]             BIT                CONSTRAINT [df_user_failed_login_attempts_isactive] DEFAULT ((1)) NOT NULL,
    [created_by]            INT                NOT NULL,
    [created_on]            DATETIMEOFFSET (3) CONSTRAINT [df_user_failed_login_attempts_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]            INT                NOT NULL,
    [updated_on]            DATETIMEOFFSET (3) CONSTRAINT [df_user_failed_login_attempts_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_user_failed_login_attempts] PRIMARY KEY CLUSTERED ([attempt_id] ASC),
    CONSTRAINT [fk_user_failed_login_attempts_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_failed_login_attempts_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [user_failed_login_attempts_login_name]
    ON [users].[user_failed_login_attempts]([login_name] ASC)
    ON [INDEXES];

