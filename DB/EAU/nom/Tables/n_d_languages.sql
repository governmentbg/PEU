CREATE TABLE [nom].[n_d_languages] (
    [language_id] INT                NOT NULL,
    [code]        NVARCHAR (50)      NOT NULL,
    [name]        NVARCHAR (100)     NOT NULL,
    [is_active]   BIT                NOT NULL,
    [is_default]  BIT                NOT NULL,
    [created_by]  INT                NOT NULL,
    [created_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_languages_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]  INT                NOT NULL,
    [updated_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_languages_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_nom.n_d_languages] PRIMARY KEY CLUSTERED ([language_id] ASC),
    CONSTRAINT [FK_n_d_languages_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_languages_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на запис за език', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'language_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'код на език', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'наименование на език изписано на съответния език', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали езикът е маркиран като активен', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'is_active';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали езикът е маркиран като език "по подразбиране"', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'is_default';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages', @level2type = N'COLUMN', @level2name = N'updated_on';


GO

CREATE TRIGGER [nom].[trg_d_languages_aiud] ON [nom].[n_d_languages]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_languages]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура на езици за локализация на системата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_languages';


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_languages_is_active]
    ON [nom].[n_d_languages]([language_id] ASC) WHERE ([is_active]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_languages_code]
    ON [nom].[n_d_languages]([code] ASC)
    ON [INDEXES];

