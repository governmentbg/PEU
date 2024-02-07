CREATE TABLE [dbo].[attached_documents] (
    [attached_document_id]        BIGINT             NOT NULL,
    [attached_document_guid]      UNIQUEIDENTIFIER   NOT NULL,
    [document_process_id]         BIGINT             NOT NULL,
    [document_process_content_id] BIGINT             NULL,
    [document_type_id]            INT                NULL,
    [description]                 NVARCHAR (1000)    NULL,
    [mime_type]                   NVARCHAR (100)     NULL,
    [file_name]                   NVARCHAR (200)     NULL,
    [html_template_content]       NVARCHAR (MAX)     NULL,
    [signing_guid]                UNIQUEIDENTIFIER   NULL,
    [created_by]                  INT                NOT NULL,
    [created_on]                  DATETIMEOFFSET (3) CONSTRAINT [df_attached_documents_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                  INT                NOT NULL,
    [updated_on]                  DATETIMEOFFSET (3) CONSTRAINT [df_attached_documents_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_attached_documents] PRIMARY KEY CLUSTERED ([attached_document_id] ASC),
    CONSTRAINT [fk_attached_documents_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_attached_documents_document_process] FOREIGN KEY ([document_process_id]) REFERENCES [dbo].[document_processes] ([document_process_id]),
    CONSTRAINT [fk_attached_documents_document_process_contents] FOREIGN KEY ([document_process_content_id]) REFERENCES [dbo].[document_process_contents] ([document_process_content_id]),
    CONSTRAINT [fk_attached_documents_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на прикачен документ към процес.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'attached_document_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на прикачен документ към процес.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'attached_document_guid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на данни за процес на заявяване на услуга.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'document_process_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на съдържание на прикачен документ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'document_process_content_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на вид документ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'document_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на документа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MIME тип на документа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'mime_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на файла (включва файловото разширение).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'file_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител редактирал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна редакция на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на заявката за подписване в модула за подписване.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'signing_guid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HTML съдържание на документа по шаблон.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'attached_documents', @level2type = N'COLUMN', @level2name = N'html_template_content';

