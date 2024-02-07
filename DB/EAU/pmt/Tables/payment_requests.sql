CREATE TABLE [pmt].[payment_requests] (
    [payment_request_id]             BIGINT             NOT NULL,
    [registration_data_id]           INT                NOT NULL,
    [registration_data_type]         INT                NOT NULL,
    [status]                         TINYINT            NOT NULL,
    [obligation_id]                  BIGINT             NOT NULL,
    [obliged_person_name]            NVARCHAR (150)     NULL,
    [obliged_person_ident]           NVARCHAR (100)     NULL,
    [obliged_person_ident_type]      TINYINT            NULL,
    [send_date]                      DATETIME           NULL,
    [pay_date]                       DATETIME           NULL,
    [external_portal_payment_number] NVARCHAR (100)     NULL,
    [amount]                         MONEY              NOT NULL,
    [additional_data]                NVARCHAR (MAX)     NULL,
    [created_by]                     INT                NOT NULL,
    [created_on]                     DATETIMEOFFSET (3) CONSTRAINT [DF_payment_requests_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                     INT                NOT NULL,
    [updated_on]                     DATETIMEOFFSET (3) CONSTRAINT [DF_payment_requests_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_payment_requests] PRIMARY KEY CLUSTERED ([payment_request_id] ASC),
    CONSTRAINT [FK_payment_requests_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_payment_requests_obl] FOREIGN KEY ([obligation_id]) REFERENCES [pmt].[obligations] ([obligation_id]),
    CONSTRAINT [FK_payment_requests_reg_data] FOREIGN KEY ([registration_data_id]) REFERENCES [pmt].[n_d_registration_data] ([registration_data_id]),
    CONSTRAINT [FK_payment_requests_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);
























GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Заявка за плащане', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'updated_by';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статус на заявка за плащане: 1 = Нова; 2 = Изпратена; 3 = Платена; 4 = Отказана; 5 = Изтекла;', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на изпращане към платежен портал.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'send_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на система за електронни разплащания.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'registration_data_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентифкатор на заявка за плащане.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'payment_request_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на плащане.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'pay_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на задължено лице.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'obliged_person_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Вид идентификатор на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'obliged_person_ident_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на задължено лице.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'obliged_person_ident';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентифкатор за задължение.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'obligation_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номер на плащане от външна система.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'external_portal_payment_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Платена сума.', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни. : reason; boricaCode; transactionNumber; portalUrl; resultTime; clientId; hmac; data; vposResultGid; errorMessage; okCancelUrl;', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'additional_data';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на регистрационните данни: 1 - ePay, 2 - ПЕП на ДАЕУ', @level0type = N'SCHEMA', @level0name = N'pmt', @level1type = N'TABLE', @level1name = N'payment_requests', @level2type = N'COLUMN', @level2name = N'registration_data_type';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_payment_requests_pep]
    ON [pmt].[payment_requests]([registration_data_id] ASC, [obligation_id] ASC) WHERE ([registration_data_type]=(2))
    ON [INDEXES];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_payment_requests_ext_num]
    ON [pmt].[payment_requests]([registration_data_id] ASC, [external_portal_payment_number] ASC) WHERE ([external_portal_payment_number] IS NOT NULL)
    ON [INDEXES];




GO
CREATE NONCLUSTERED INDEX [IDX_payment_requests_obligation]
    ON [pmt].[payment_requests]([obligation_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_payment_requests_status]
    ON [pmt].[payment_requests]([status] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_payment_requests_registration_data_id]
    ON [pmt].[payment_requests]([registration_data_id] ASC)
    ON [INDEXES];

