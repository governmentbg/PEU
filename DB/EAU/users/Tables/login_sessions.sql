CREATE TABLE [users].[login_sessions] (
    [id]                  BIGINT             IDENTITY (1, 1) NOT NULL,
    [login_session_id]    UNIQUEIDENTIFIER   NOT NULL,
    [user_session_id]     UNIQUEIDENTIFIER   NOT NULL,
    [user_id]             INT                NOT NULL,
    [login_date]          DATETIMEOFFSET (3) NOT NULL,
    [logout_date]         DATETIMEOFFSET (3) NULL,
    [ip_address]          VARBINARY (16)     NOT NULL,
    [authentication_type] TINYINT            NOT NULL,
    [certificate_id]      INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_login_sessions_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [df_login_sessions_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_login_sessions] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_login_sessions_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_login_sessions_certid] FOREIGN KEY ([certificate_id]) REFERENCES [users].[certificates] ([certificate_id]),
    CONSTRAINT [fk_login_sessions_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [IDX_login_sessions_user_id]
    ON [users].[login_sessions]([user_id] ASC)
    ON [INDEXES];

