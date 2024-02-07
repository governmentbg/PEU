CREATE TABLE [eml].[n_s_email_templates] (
    [template_id]  INT                IDENTITY (1, 1) NOT NULL,
    [subject]      NVARCHAR (2000)    NOT NULL,
    [body]         NVARCHAR (MAX)     NOT NULL,
    [is_body_html] BIT                NOT NULL,
    [created_by]   INT                NOT NULL,
    [created_on]   DATETIMEOFFSET (3) CONSTRAINT [df_n_s_email_templates_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]   INT                NOT NULL,
    [updated_on]   DATETIMEOFFSET (3) CONSTRAINT [df_n_s_email_templates_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_n_s_email_templates] PRIMARY KEY CLUSTERED ([template_id] ASC),
    CONSTRAINT [fk_n_s_email_templates_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_n_s_email_templates_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на шаблон на имейл съобщение.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'template_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тема на съобщението.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тяло на съобщението.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'body';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг, указващ дали съдържанието е HTML.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'is_body_html';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител редактирал записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на редакция на записа.', @level0type = N'SCHEMA', @level0name = N'eml', @level1type = N'TABLE', @level1name = N'n_s_email_templates', @level2type = N'COLUMN', @level2name = N'updated_on';

