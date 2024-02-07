CREATE TABLE [users].[user_processes] (
    [process_id]    INT                NOT NULL,
    [process_guid]  UNIQUEIDENTIFIER   NOT NULL,
    [process_type]  TINYINT            NOT NULL,
    [user_id]       INT                NOT NULL,
    [invalid_after] DATETIME           NULL,
    [status]        TINYINT            NOT NULL,
    [created_by]    INT                NOT NULL,
    [created_on]    DATETIMEOFFSET (3) CONSTRAINT [df_user_processes_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]    INT                NOT NULL,
    [updated_on]    DATETIMEOFFSET (3) CONSTRAINT [df_user_processes_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_user_processes] PRIMARY KEY CLUSTERED ([process_id] ASC),
    CONSTRAINT [ck_user_processes_type] CHECK ([process_type]=(1) OR [process_type]=(2)),
    CONSTRAINT [fk_user_processes_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_user_processes_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [IDX_user_processes_user_id]
    ON [users].[user_processes]([user_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_user_processes_status]
    ON [users].[user_processes]([status] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_user_processes_process_type]
    ON [users].[user_processes]([process_type] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_user_processes_process_guid]
    ON [users].[user_processes]([process_guid] ASC)
    ON [INDEXES];

