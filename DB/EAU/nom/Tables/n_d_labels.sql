CREATE TABLE [nom].[n_d_labels] (
    [label_id]            INT                NOT NULL,
    [label_ver_id]        BIGINT             NOT NULL,
    [code]                NVARCHAR (200)     NULL,
    [value]               NVARCHAR (MAX)     NULL,
    [description]         NVARCHAR (2000)    NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [df_n_d_labels_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [df_n_d_labels_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_n_d_labels] PRIMARY KEY CLUSTERED ([label_id] ASC, [label_ver_id] ASC),
    CONSTRAINT [fk_n_d_labels_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_n_d_labels_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща преводите за ресурсите на Български език', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на запис за етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'label_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на версия на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'label_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'код на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'текст на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'описание на етикет', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия, с която е деактивиран записът', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_labels', @level2type = N'COLUMN', @level2name = N'updated_on';


GO

CREATE TRIGGER [nom].[trg_n_d_labels_aiud] ON [nom].[n_d_labels]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_labels]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_labels_id]
    ON [nom].[n_d_labels]([label_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_labels_code]
    ON [nom].[n_d_labels]([code] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_labels_is_last]
    ON [nom].[n_d_labels]([label_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

