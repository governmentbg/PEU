CREATE TABLE [nom].[n_d_services_i18n] (
    [service_id]                     INT                NOT NULL,
    [service_ver_id]                 BIGINT             NOT NULL,
    [service_i18n_ver_id]            BIGINT             NOT NULL,
    [language_id]                    INT                NOT NULL,
    [name]                           NVARCHAR (1000)    NULL,
    [description]                    NVARCHAR (MAX)     NULL,
    [attached_documents_description] NVARCHAR (MAX)     NULL,
    [is_last]                        BIT                NOT NULL,
    [deactivation_ver_id]            INT                NULL,
    [created_by]                     INT                NOT NULL,
    [created_on]                     DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_services_i18n_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                     INT                NOT NULL,
    [updated_on]                     DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_services_i18n_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_services_i18n] PRIMARY KEY CLUSTERED ([service_id] ASC, [service_i18n_ver_id] ASC, [language_id] ASC),
    CONSTRAINT [FK_n_d_services_i18n_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_services_i18n_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_n_d_services_i18n_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'is_last';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'html съдържание за описание на административната услуга. УРИ на обекта по РИО - 0008-000079', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование  на услугата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'name';


GO

CREATE TRIGGER [nom].[trg_n_d_services_i18n_aiud] ON [nom].n_d_services_i18n
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_services_i18n]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на езика, за който е превода на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'language_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на прилаганите документи', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services_i18n', @level2type = N'COLUMN', @level2name = N'attached_documents_description';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_services_i18n_id]
    ON [nom].[n_d_services_i18n]([service_id] ASC, [language_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_services_i18n_name]
    ON [nom].[n_d_services_i18n]([name] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_services_i18n_language_id]
    ON [nom].[n_d_services_i18n]([language_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_services_i18n_is_last]
    ON [nom].[n_d_services_i18n]([service_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

