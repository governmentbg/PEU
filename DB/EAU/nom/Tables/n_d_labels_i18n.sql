CREATE TABLE [nom].[n_d_labels_i18n] (
    [label_id]            INT                NOT NULL,
    [label_ver_id]        BIGINT             NOT NULL,
    [label_i18n_ver_id]   BIGINT             NOT NULL,
    [language_id]         INT                NOT NULL,
    [value]               NVARCHAR (MAX)     NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_n_d_labels_i18n_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [PK_n_d_labels_i18n] PRIMARY KEY CLUSTERED ([label_id] ASC, [label_i18n_ver_id] ASC, [language_id] ASC),
    CONSTRAINT [FK_n_d_labels_i18n_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_labels_i18n_n_d_languages] FOREIGN KEY ([language_id]) REFERENCES [nom].[n_d_languages] ([language_id]),
    CONSTRAINT [FK_n_d_labels_i18n_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_labels_n_d_labels_i18n] FOREIGN KEY ([label_id], [label_ver_id]) REFERENCES [nom].[n_d_labels] ([label_id], [label_ver_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица за съхранение на преводите за ресурси за чужди езици.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на запис за етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'label_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на версия на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'label_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на версия на локализиран етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'label_i18n_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на езика, за който е превода на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'language_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'текст на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'value';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия, с която е деактивиран записът', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels_i18n', @level2type = N'COLUMN', @level2name = N'updated_on';


GO

CREATE TRIGGER [nom].[trg_n_d_labels_i18n_aiud] ON [nom].[n_d_labels_i18n]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_labels_i18n]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_labels_i18n_id_lang]
    ON [nom].[n_d_labels_i18n]([label_id] ASC, [language_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_labels_i18n_is_last]
    ON [nom].[n_d_labels_i18n]([label_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

