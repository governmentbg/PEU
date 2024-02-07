CREATE TABLE [nom].[n_d_service_terms] (
    [service_term_id]     INT                NOT NULL,
    [service_term_ver_id] BIGINT             NOT NULL,
    [service_id]          INT                NOT NULL,
    [service_ver_id]      BIGINT             NOT NULL,
    [service_term_type]   TINYINT            NOT NULL,
    [price]               MONEY              NULL,
    [execution_period]    INT                NULL,
    [description]         NVARCHAR (MAX)     NULL,
    [period_type]         TINYINT            NULL,
    [is_active]           BIT                NOT NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_terms_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_terms_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_admin_service_terms] PRIMARY KEY CLUSTERED ([service_term_id] ASC, [service_term_ver_id] ASC),
    CONSTRAINT [CK_n_d_service_terms_period_type] CHECK ([period_type]=(1) OR [period_type]=(2)),
    CONSTRAINT [CK_n_d_service_terms_service_term_type] CHECK ([service_term_type]=(1) OR [service_term_type]=(2) OR [service_term_type]=(3)),
    CONSTRAINT [FK_n_d_service_terms_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_service_terms_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_n_d_service_terms_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Активен', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'is_active';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-  Часове, 2 - Дни', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'period_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'html съдържание с кратко описание на вида услуга не зависеща от срока', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Срок за изпълнение  в дни', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'execution_period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на предаване 1-  Обикновена, 2 - Бърза, 3 - Експресна, 4 - Не зависи от срока', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'service_term_type';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на административна услуга.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'service_id';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура начини на предаване на административните услуги в административните структури.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms';


GO

CREATE TRIGGER [nom].[trg_n_d_service_terms_aiud] ON [nom].n_d_service_terms
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_service_terms]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на административна услуга.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на начини на предаване на административните услуги.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'service_term_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на начини на предаване на административните услуги.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'service_term_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Цена', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_terms', @level2type = N'COLUMN', @level2name = N'price';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_terms_id_srv_id]
    ON [nom].[n_d_service_terms]([service_term_id] ASC, [service_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_terms_id]
    ON [nom].[n_d_service_terms]([service_term_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_terms_service_id]
    ON [nom].[n_d_service_terms]([service_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_terms_is_last]
    ON [nom].[n_d_service_terms]([service_term_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

