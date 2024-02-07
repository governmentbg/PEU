CREATE TABLE [dbo].[document_processes] (
    [document_process_id]          BIGINT             NOT NULL,
    [applicant_id]                 INT                NULL,
    [document_type_id]             INT                NOT NULL,
    [service_id]                   INT                NULL,
    [service_ver_id]               BIGINT             NULL,
    [status]                       SMALLINT           NOT NULL,
    [mode]                         SMALLINT           NOT NULL,
    [type]                         SMALLINT           NOT NULL,
    [additional_data]              NVARCHAR (MAX)     NULL,
    [signing_guid]                 UNIQUEIDENTIFIER   NULL,
    [error_message]                NVARCHAR (250)     NULL,
    [case_file_uri]                NVARCHAR (100)     NULL,
    [not_acknowledged_message_uri] NVARCHAR (500)     NULL,
    [request_id]                   NVARCHAR (200)      NULL,
    [created_by]                   INT                NOT NULL,
    [created_on]                   DATETIMEOFFSET (3) CONSTRAINT [df_document_processes_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                   INT                NOT NULL,
    [updated_on]                   DATETIMEOFFSET (3) CONSTRAINT [df_document_processes_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_document_processes] PRIMARY KEY CLUSTERED ([document_process_id] ASC),
    CONSTRAINT [ck_document_processes_mode] CHECK ([mode]=(4) OR [mode]=(3) OR [mode]=(2) OR [mode]=(1)),
    CONSTRAINT [ck_document_processes_status] CHECK ([status]=(8) OR [status]=(7) OR [status]=(6) OR [status]=(5) OR [status]=(4) OR [status]=(3) OR [status]=(2) OR [status]=(1)),
    CONSTRAINT [ck_document_processes_type] CHECK ([type]=(2) OR [type]=(1)),
    CONSTRAINT [ck_document_processes_type_request_id] CHECK ([type]=(2) AND [request_id] IS NOT NULL OR [type]<>(2) AND [request_id] IS NULL),
    CONSTRAINT [fk_document_processes_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_document_processes_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [fk_document_processes_n_s_document_types] FOREIGN KEY ([document_type_id]) REFERENCES [nom].[n_s_document_types] ([doc_type_id]),
    CONSTRAINT [fk_document_processes_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);












GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-Read;2-Write;3-Sign;4-WriteAndSign;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'CONSTRAINT', @level2name = N'ck_document_processes_mode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-В процес на подаване;2-В преоцес на подписване;3-Готово за изпращане;4-Изпраща се;5-Прието;6-Грешка при приемане;7-Регистрирано;8-Не регистрирано;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'CONSTRAINT', @level2name = N'ck_document_processes_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на данни за процеси на заявяване на услуга.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'document_process_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на заявителя.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'applicant_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на види документ инициирал процеса.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'document_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услугата инициирала процеса.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'service_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Статус на пакета', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Мод в който е вдигнат процеса.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'mode';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни описващи заявленията.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'additional_data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на заявката за подписване в модула за подписване.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'signing_guid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съобщение за грешка при обработката на процеса.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'error_message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'УРИ на преписка.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'case_file_uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' УРИ на съобщение че потвърждаването не се получава.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'not_acknowledged_message_uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител редактирал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна редакция на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
CREATE UNIQUE NONCLUSTERED INDEX [uidx_document_processes_applicant_service_id]
    ON [dbo].[document_processes]([applicant_id] ASC, [service_id] ASC) WHERE ([type]=(1) AND [service_id] IS NOT NULL AND ([mode] IN ((2), (3), (4))))
    ON [INDEXES];






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Канал по който е иницииран процеса.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_processes', @level2type = N'COLUMN', @level2name = N'type';


GO
CREATE UNIQUE NONCLUSTERED INDEX [uidx_document_processes_request_id]
    ON [dbo].[document_processes]([request_id] ASC) WHERE ([type]=(2))
    ON [INDEXES];

