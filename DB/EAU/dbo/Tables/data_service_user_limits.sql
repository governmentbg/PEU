CREATE TABLE [dbo].[data_service_user_limits] (
    [user_limit_id]        INT                NOT NULL,
    [user_limit_ver_id]    BIGINT             NOT NULL,
    [service_limit_id]     INT                NOT NULL,
    [service_limit_ver_id] BIGINT             NOT NULL,
    [user_id]              INT                NOT NULL,
    [requests_interval]    DATETIME           NOT NULL,
    [requests_number]      INT                NOT NULL,
    [status]               INT                NOT NULL,
    [is_last]              BIT                NOT NULL,
    [deactivation_ver_id]  INT                NULL,
    [created_by]           INT                NOT NULL,
    [created_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_data_service_user_limits_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]           INT                NOT NULL,
    [updated_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_data_service_user_limits_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_data_service_user_limits] PRIMARY KEY CLUSTERED ([user_limit_id] ASC, [user_limit_ver_id] ASC),
    CONSTRAINT [FK_data_service_user_limits_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_data_service_user_limits_lm] FOREIGN KEY ([service_limit_id], [service_limit_ver_id]) REFERENCES [dbo].[data_service_limits] ([service_limit_id], [service_limit_ver_id]),
    CONSTRAINT [FK_data_service_user_limits_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_data_service_user_limits_usr] FOREIGN KEY ([user_id]) REFERENCES [users].[users] ([user_id])
);








GO


CREATE TRIGGER [dbo].[trg_data_service_user_limits_aiud] ON [dbo].[data_service_user_limits]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[dbo].[data_service_user_limits]'
				END