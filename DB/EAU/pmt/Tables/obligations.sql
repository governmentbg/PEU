CREATE TABLE [pmt].[obligations] (
    [obligation_id]             BIGINT             NOT NULL,
    [status]                    TINYINT            NOT NULL,
    [amount]                    MONEY              NOT NULL,
    [discount_amount]           MONEY              NOT NULL,
    [bank_name]                 NVARCHAR (500)     NULL,
    [bic]                       NVARCHAR (8)       NOT NULL,
    [iban]                      NVARCHAR (22)      NOT NULL,
    [payment_reason]            NVARCHAR (2000)    NOT NULL,
    [pep_cin]                   NVARCHAR (100)     NULL,
    [expiration_date]           DATETIME           NOT NULL,
    [applicant_id]              INT                NULL,
    [obliged_person_name]       NVARCHAR (150)     NULL,
    [obliged_person_ident]      NVARCHAR (100)     NULL,
    [obliged_person_ident_type] TINYINT            NULL,
    [obligation_date]           DATE               NOT NULL,
    [obligation_identifier]     NVARCHAR (300)     NOT NULL,
    [type]                      TINYINT            NOT NULL,
    [service_instance_id]       BIGINT             NULL,
    [service_instance_ver_id]   BIGINT             NULL,
    [service_id]                INT                NOT NULL,
    [service_ver_id]            BIGINT             NOT NULL,
    [additional_data]           NVARCHAR (MAX)     NULL,
    [created_by]                INT                NOT NULL,
    [created_on]                DATETIMEOFFSET (3) CONSTRAINT [DF_obligations_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                INT                NOT NULL,
    [updated_on]                DATETIMEOFFSET (3) CONSTRAINT [DF_obligations_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_obligations] PRIMARY KEY CLUSTERED ([obligation_id] ASC),
    CONSTRAINT [FK_obligations_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_obligations_srv] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_obligations_srv_inst] FOREIGN KEY ([service_instance_id], [service_instance_ver_id]) REFERENCES [dbo].[service_instances] ([service_instance_id], [service_instance_ver_id]),
    CONSTRAINT [FK_obligations_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);
























GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Задължения за плащане.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Вид плащане: 1 = Инстанция на услуга; 2 = Задължение на плащане към АНД;', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статус на задължение в ЕАУ: 0 = Необработено, 1 = В процес на обработка; 2 = Платен; 3 = Обработен (Уведомен е бекен-а);', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'status';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на инстанция на услуга.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'service_instance_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услуга', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'service_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на ПЕП на ДАЕУ.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'pep_cin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Основание за плащане.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'payment_reason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на задължено лице.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obliged_person_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Вид идентификатор на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obliged_person_ident_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на задължено лице.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obliged_person_ident';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на задължението. кодирана 4-ка за акт или 2-ка за услуги: "documentType|documentSeries|documentNumber|totalAmount", "DocumentUri|ServiceInstanceID"', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obligation_identifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентифкатор за задължение.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obligation_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на задължението.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'obligation_date';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IBAN.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'iban';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата, на която изтича.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'expiration_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Сума с отстъпка.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'discount_amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIC.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'bic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'При банка.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'bank_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на заявител.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'applicant_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Сума.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни. : serviceInstanceID, paymentInstructionURI, obigedPersonIdent, obigedPersonIdentType, isServed, discount, isMainDocument, documentType, documentSeries, documentNumber, amount, fishCreateDate', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'additional_data';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия на услуга', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия на инстанция на услуга.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'obligations', @level2type = N'COLUMN', @level2name = N'service_instance_ver_id';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_obligations_active]
    ON [pmt].[obligations]([applicant_id] ASC, [obligation_date] ASC, [obligation_identifier] ASC, [type] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_obligations_srv_inst]
    ON [pmt].[obligations]([service_instance_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_obligations_applicant]
    ON [pmt].[obligations]([applicant_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_obligations_service]
    ON [pmt].[obligations]([service_id] ASC)
    ON [INDEXES];

