CREATE TABLE [dbo].[document_process_contents] (
    [document_process_content_id] BIGINT             NOT NULL,
    [document_process_id]         BIGINT             NOT NULL,
    [type]                        SMALLINT           NOT NULL,
    [content]                     VARBINARY (MAX)    NULL,
    [created_by]                  INT                NOT NULL,
    [created_on]                  DATETIMEOFFSET (3) CONSTRAINT [df_document_process_contents_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                  INT                NOT NULL,
    [updated_on]                  DATETIMEOFFSET (3) CONSTRAINT [df_document_process_contents_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [length]                      INT                CONSTRAINT [DF_document_process_contents_length] DEFAULT ((0)) NOT NULL,
    [text_content]                NVARCHAR (MAX)     NULL,
    CONSTRAINT [xpk_document_process_contents] PRIMARY KEY CLUSTERED ([document_process_content_id] ASC),
    CONSTRAINT [ck_content_or_textcontent] CHECK ([text_content] IS NOT NULL AND [type]<>(3) OR [type]=(3) AND [text_content] IS NULL),
    CONSTRAINT [ck_type] CHECK ([type]=(3) OR [type]=(2) OR [type]=(1)),
    CONSTRAINT [fk_document_process_contents_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_document_process_contents_document_processes] FOREIGN KEY ([document_process_id]) REFERENCES [dbo].[document_processes] ([document_process_id]),
    CONSTRAINT [fk_document_process_contents_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
CREATE NONCLUSTERED INDEX [IDX_document_process_contents_document_process_id]
    ON [dbo].[document_process_contents]([document_process_id] ASC)
    ON [INDEXES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна редакция на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител редактирал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържание.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на съдържанието.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на данни за процеси на заявяване на услуга.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'document_process_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на съдържание на процес.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'COLUMN', @level2name = N'document_process_content_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на данните: 1- json съдържание на заявлението; 2- xml съдържание на заявлението; 3- съдържание на прикачен документ; ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'document_process_contents', @level2type = N'CONSTRAINT', @level2name = N'ck_type';

