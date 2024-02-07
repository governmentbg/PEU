CREATE TABLE [users].[user_authentications] (
    [authentication_id]   INT                NOT NULL,
    [user_id]             INT                NOT NULL,
    [authentication_type] TINYINT            NOT NULL,
    [password_hash]       NVARCHAR (200)     NULL,
    [username]            NVARCHAR (100)     NULL,
    [certificate_id]      INT                NULL,
    [is_locked]           BIT                CONSTRAINT [df_user_authentications_locked] DEFAULT ((0)) NOT NULL,
    [locked_until]        DATETIME           NULL,
    [is_active]           BIT                CONSTRAINT [df_user_authentications_isactive] DEFAULT ((1)) NOT NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_user_authentications_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [df_user_authentications_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_user_authentications] PRIMARY KEY CLUSTERED ([authentication_id] ASC),
    CONSTRAINT [fk_user_authentications_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_authentications_certid] FOREIGN KEY ([certificate_id]) REFERENCES [users].[certificates] ([certificate_id]),
    CONSTRAINT [fk_user_authentications_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_authentications_uid] FOREIGN KEY ([user_id]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [user_authentications_authentication_type]
    ON [users].[user_authentications]([authentication_type] ASC)
    ON [INDEXES];

