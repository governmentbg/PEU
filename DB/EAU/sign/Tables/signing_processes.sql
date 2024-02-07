CREATE TABLE [sign].[signing_processes] (
    [process_id]                UNIQUEIDENTIFIER   NOT NULL,
    [callback_client_config_id] INT                NOT NULL,
    [format]                    SMALLINT           NOT NULL,
    [status]                    SMALLINT           NOT NULL,
    [level]                     SMALLINT           NOT NULL,
    [type]                      SMALLINT           NOT NULL,
    [digest_method]             SMALLINT           NOT NULL,
    [additional_data]           NVARCHAR (MAX)     NULL,
    [rejected_callback_url]     NVARCHAR (300)     NOT NULL,
    [completed_callback_url]    NVARCHAR (300)     NOT NULL,
    [file_name]                 NVARCHAR (200)     NOT NULL,
    [content_type]              NVARCHAR (200)     NOT NULL,
    [content]                   VARBINARY (MAX)    NULL,
    [created_by]                INT                NOT NULL,
    [created_on]                DATETIMEOFFSET (3) CONSTRAINT [df_signing_processes_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                INT                NOT NULL,
    [updated_on]                DATETIMEOFFSET (3) CONSTRAINT [df_signing_processes_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [pk_signing_processes] PRIMARY KEY CLUSTERED ([process_id] ASC),
    CONSTRAINT [chk_digest_method] CHECK ([digest_method]=(7) OR [digest_method]=(6) OR [digest_method]=(5) OR [digest_method]=(4) OR [digest_method]=(3) OR [digest_method]=(2) OR [digest_method]=(1) OR [digest_method]=(0)),
    CONSTRAINT [chk_format] CHECK ([format]=(4) OR [format]=(3) OR [format]=(2) OR [format]=(1) OR [format]=(0)),
    CONSTRAINT [chk_level] CHECK ([level]=(3) OR [level]=(2) OR [level]=(1) OR [level]=(0)),
    CONSTRAINT [chk_status] CHECK ([status]=(3) OR [status]=(2) OR [status]=(1)),
    CONSTRAINT [chk_type] CHECK ([type]=(2) OR [type]=(1) OR [type]=(0)),
    CONSTRAINT [fk_signing_processes_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_signing_processes_n_s_callback_clients_config] FOREIGN KEY ([callback_client_config_id]) REFERENCES [sign].[n_s_callback_clients_config] ([callback_client_id]),
    CONSTRAINT [fk_signing_processes_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна промяна на записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител последно редактирал записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържанието на документа за подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mime тип на файла за подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'content_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на файла за подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'file_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес за известяване при успешно завършен процес по подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'completed_callback_url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес за известяване при прекратен процес по подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'rejected_callback_url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни за процеса на подписване в JSON формат.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'additional_data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Хеш алгоритам: 0 - SHA1, 1- SHA224, 2 - SHA256, 3 - SHA384 , 4 - SHA512, 5 - RIPEMD160, 6 - MD2, 7 - MD5', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'digest_method';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на пакетиране на подписа: 0 - ENVELOPED , 1 - ENVELOPING и 2 - DETACHED', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ниво на подписа: 0 - BASELINE_B, 1 - BASELINE_T, 2- BASELINE_LT и 3 - BASELINE_LTA', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'level';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - in process, 2- rejected, 3 - completed', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Формата възможни стойности: 0 - CADES, 1 - PADES, 2 - XADES, 3 - ASICS и 4 - ASICE', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'format';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на клиентска конфигурация за обратно известяване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'callback_client_config_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signing_processes', @level2type = N'COLUMN', @level2name = N'process_id';


GO
CREATE NONCLUSTERED INDEX [IDX_signing_processes_status]
    ON [sign].[signing_processes]([status] ASC)
    ON [INDEXES];

