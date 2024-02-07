CREATE TABLE [eml].[email_messages] (
    [email_id]              INT                NOT NULL,
    [priority]              SMALLINT           NOT NULL,
    [status]                SMALLINT           NOT NULL,
    [try_count]             INT                NOT NULL,
    [send_date]             DATETIMEOFFSET (3) NULL,
    [subject]               NVARCHAR (500)     NOT NULL,
    [body]                  NVARCHAR (MAX)     NOT NULL,
    [is_body_html]          BIT                NOT NULL,
    [do_not_process_before] DATETIMEOFFSET (3) NOT NULL,
    [sending_provider_name] NVARCHAR (20)      NOT NULL,
    [recipients]            NVARCHAR (MAX)     NOT NULL,
    [created_by]            INT                NOT NULL,
    [created_on]            DATETIMEOFFSET (3) CONSTRAINT [df_email_messagess_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]            INT                NOT NULL,
    [updated_on]            DATETIMEOFFSET (3) CONSTRAINT [df_email_messages_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_email_messages] PRIMARY KEY CLUSTERED ([email_id] ASC),
    CONSTRAINT [ck_email_messages_priority] CHECK ([priority]=(3) OR [priority]=(2) OR [priority]=(1)),
    CONSTRAINT [ck_email_messages_status] CHECK ([status]=(3) OR [status]=(2) OR [status]=(1)),
    CONSTRAINT [fk_email_messages_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_email_messages_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Приоритет на имейл: 1 = Нисък приоритет.; 2 = Нормален приоритет.; 3 = Висок приоритет', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'CONSTRAINT', @level2name = N'ck_email_messages_priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Позволява статуси 1 -Pending, 2 - Sent, 3 - Failed.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'CONSTRAINT', @level2name = N'ck_email_messages_status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на съобщение.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'email_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Приоритет: 1-Нисък; 2- Нормален; 3 - Висок;', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'priority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статус: 1-Pending; 2- Sent; 3 - Failed;', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Брой направени опити за изпращане - разлика спрямо фиксиран максимален брой възможни опити.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'try_count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на изпращане.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'send_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тема.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Тяло на съобщението.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'body';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг, указващ дали съдържанието е HTML.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'is_body_html';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата след която съобщението може да бъде изпратено.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'do_not_process_before';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на изпращаща услуга.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'sending_provider_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Получатели на имейла (JSON дата).', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'recipients';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител редактирал записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на редакция на записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'email_messages', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
CREATE NONCLUSTERED INDEX [idx_email_messages_priority_id]
    ON [eml].[email_messages]([priority] DESC, [email_id] ASC)
    INCLUDE([do_not_process_before]) WHERE ([STATUS]=(1))
    ON [INDEXES];

