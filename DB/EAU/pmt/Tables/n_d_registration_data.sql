CREATE TABLE [pmt].[n_d_registration_data] (
    [registration_data_id] INT                NOT NULL,
    [type]                 INT                NOT NULL,
    [description]          NVARCHAR (2000)    NULL,
    [cin]                  NVARCHAR (100)     NOT NULL,
    [email]                NVARCHAR (200)     NULL,
    [secret_word]          NVARCHAR (200)     NOT NULL,
    [validity_period]      TIME (7)           NOT NULL,
    [portal_url]           NVARCHAR (1000)    NULL,
    [notification_url]     NVARCHAR (1000)    NULL,
    [service_url]          NVARCHAR (1000)    NULL,
    [created_by]           INT                NOT NULL,
    [created_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_registration_data_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]           INT                NOT NULL,
    [updated_on]           DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_registration_data_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_registration_data] PRIMARY KEY CLUSTERED ([registration_data_id] ASC),
    CONSTRAINT [FK_n_d_registration_data_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_registration_data_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);














GO



GO

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'created_by';


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Период на валидност на плащане - необходим за определяне на крайната дата и час на валидността на плащането през оператора', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'validity_period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Буквено цифрова секретна дума генерирана от ePay', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'secret_word';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E-mail на ПЕАУ по регистрация', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'КИН на ПЕАУ - клиентски номер в личните данни на търговеца', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'cin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на вид на плащане', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на регистрационните данни: 1 - ePay, 2 - ПЕП на ДАЕУ', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_n_d_registration_data_cin]
    ON [pmt].[n_d_registration_data]([cin] ASC, [type] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_n_d_registration_data_Type]
    ON [pmt].[n_d_registration_data]([type] ASC) WHERE ([type]=(1));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на регистрационни данни.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'registration_data_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL за достъп до услугите на ПЕП на ДАЕУ', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'service_url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес за достъп', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'portal_url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Електронен адрес за уведомяване - електронен адрес, на който се изпраща съобщение за променен статус на заявка за плащане', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'n_d_registration_data', @level2type = N'COLUMN', @level2name = N'notification_url';

