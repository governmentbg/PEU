CREATE TABLE [sign].[signers] (
    [signer_id]            BIGINT             NOT NULL,
    [process_id]           UNIQUEIDENTIFIER   NOT NULL,
    [name]                 NVARCHAR (200)     NULL,
    [ident]                NVARCHAR (10)      NULL,
    [order]                SMALLINT           NOT NULL,
    [status]               SMALLINT           NOT NULL,
    [signing_channel]      SMALLINT           NULL,
    [transaction_id]       NVARCHAR (50)      NULL,
    [additional_sign_data] NVARCHAR (MAX)     NULL,
    [reject_reson_label]   NVARCHAR (50)      NULL,
    [created_by]           INT                NOT NULL,
    [created_on]           DATETIMEOFFSET (3) CONSTRAINT [df_signers_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]           INT                NOT NULL,
    [updated_on]           DATETIMEOFFSET (3) CONSTRAINT [df_signers_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [pk_signers] PRIMARY KEY CLUSTERED ([signer_id] ASC),
    CONSTRAINT [chk_additional_signer_data] CHECK ([additional_sign_data] IS NULL OR [additional_sign_data] IS NOT NULL AND ([signing_channel]=(2) OR [signing_channel]=(1))),
    CONSTRAINT [chk_signer_transaction_id] CHECK ([transaction_id] IS NULL OR [transaction_id] IS NOT NULL AND ([signing_channel]=(2) OR [signing_channel]=(1))),
    CONSTRAINT [chk_signers_status] CHECK ([status]=(3) OR [status]=(2) OR [status]=(1) OR [status]=(0)),
    CONSTRAINT [chk_signing_channel] CHECK ([signing_channel] IS NULL OR ([signing_channel]=(2) OR [signing_channel]=(1) OR [signing_channel]=(0))),
    CONSTRAINT [fk_signers_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_signers_signing_processes] FOREIGN KEY ([process_id]) REFERENCES [sign].[signing_processes] ([process_id]),
    CONSTRAINT [fk_signers_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна редакция на записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител последно редактирал записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Причина за отказ от продписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'reject_reson_label';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни за при започнато отдалечено подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'additional_sign_data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на процеса по подписване в системата на доставчика.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'transaction_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Канал за подписване: 0 - B-trust локално , 1 - B-trust отдалечено и 2 - Evrotrust отдалечено.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'signing_channel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статус на полагане на подпис: 0 - чака, 1 - започнал подписването, 2 - подписал.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Поредност на полагане на подписа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'order';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ЕГН/ЛНЧ на подписващия.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'ident';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на подписващия.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на процеса по подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'process_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на подписващия.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'signers', @level2type = N'COLUMN', @level2name = N'signer_id';


GO
CREATE NONCLUSTERED INDEX [IDX_signers_status]
    ON [sign].[signers]([status] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_signers_signing_channel]
    ON [sign].[signers]([signing_channel] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_signers_process_id]
    ON [sign].[signers]([process_id] ASC)
    ON [INDEXES];

