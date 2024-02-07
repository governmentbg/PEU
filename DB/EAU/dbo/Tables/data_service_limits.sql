CREATE TABLE [dbo].[data_service_limits] (
    [service_limit_id]     INT                NOT NULL,
    [service_limit_ver_id] BIGINT             NOT NULL,
    [service_code]         NVARCHAR (100)     NOT NULL,
    [service_name]         NVARCHAR (500)     NOT NULL,
    [requests_interval]    DATETIME           NOT NULL,
    [requests_number]      INT                NOT NULL,
    [status]               INT                NOT NULL,
    [is_last]              BIT                NOT NULL,
    [deactivation_ver_id]  INT                NULL,
    [created_by]           INT                NOT NULL,
    [created_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_data_service_limits_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]           INT                NOT NULL,
    [updated_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_data_service_limits_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_data_service_limits] PRIMARY KEY CLUSTERED ([service_limit_id] ASC, [service_limit_ver_id] ASC),
    CONSTRAINT [CK_data_service_limits_req] CHECK ([requests_interval]>'00:00:00' AND [requests_number]>(0)),
    CONSTRAINT [FK_data_service_limits_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_data_service_limits_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия с която е деактивиран записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг, указващ дали версията е последна', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статус на лимит: 0 -  Неактивен, 1 - Активен', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Максимален брой заявки за периода от време', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'requests_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Период от време', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'requests_interval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на услуга за предоставяне на данни', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'service_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код на услуга за предоставяне на данни', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'service_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия на лимит', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'service_limit_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на лимит', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits', @level2type = N'COLUMN', @level2name = N'service_limit_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Лимити за услуги за предоставяне на данни', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'data_service_limits';


GO


CREATE TRIGGER [dbo].[trg_data_service_limits_aiud] ON [dbo].[data_service_limits]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[dbo].[data_service_limits]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [data_service_limits__code_idx]
    ON [dbo].[data_service_limits]([service_code] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_data_service_limits_is_last]
    ON [dbo].[data_service_limits]([service_limit_id] ASC) WHERE ([is_last]=(1));

