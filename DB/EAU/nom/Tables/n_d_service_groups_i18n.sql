CREATE TABLE [nom].[n_d_service_groups_i18n] (
    [group_id]            INT                NOT NULL,
    [group_ver_id]        BIGINT             NOT NULL,
    [group_i18n_ver_id]   BIGINT             NOT NULL,
    [language_id]         INT                NOT NULL,
    [name]                NVARCHAR (1000)    NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_groups_i18n_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_groups_i18n_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_service_groups_i18n_1] PRIMARY KEY CLUSTERED ([group_id] ASC, [group_i18n_ver_id] ASC, [language_id] ASC),
    CONSTRAINT [FK_n_d_service_groups_i18n_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_service_groups_i18n_n_d_languages] FOREIGN KEY ([language_id]) REFERENCES [nom].[n_d_languages] ([language_id]),
    CONSTRAINT [FK_n_d_service_groups_i18n_n_d_service_groups] FOREIGN KEY ([group_id], [group_ver_id]) REFERENCES [nom].[n_d_service_groups] ([group_id], [group_ver_id]),
    CONSTRAINT [FK_n_d_service_groups_i18n_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща групи услуги по направление на дейност в МВР за чужди езици.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'group_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на версия на локализирана група', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'group_i18n_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на езика, за който е превода на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'language_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups_i18n', @level2type = N'COLUMN', @level2name = N'updated_on';


GO

CREATE TRIGGER [nom].[trg_n_d_service_groups_i18n_aiud] ON [nom].n_d_service_groups_i18n
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_service_groups_i18n]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_groups_i18n_id]
    ON [nom].[n_d_service_groups_i18n]([group_id] ASC, [language_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_groups_i18n_language_id]
    ON [nom].[n_d_service_groups_i18n]([language_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_groups_i18n_is_last]
    ON [nom].[n_d_service_groups_i18n]([group_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

