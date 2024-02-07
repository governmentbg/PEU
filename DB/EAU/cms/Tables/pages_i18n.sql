CREATE TABLE [cms].[pages_i18n] (
    [page_id]     INT                NOT NULL,
    [language_id] INT                NOT NULL,
    [title]       NVARCHAR (200)     NULL,
    [content]     NVARCHAR (MAX)     NULL,
    [created_by]  INT                NOT NULL,
    [created_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_pages_i18n_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]  INT                NOT NULL,
    [updated_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_pages_i18n_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_pages_i18n_1] PRIMARY KEY CLUSTERED ([page_id] ASC, [language_id] ASC),
    CONSTRAINT [FK_pages_i18n_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_pages_i18n_n_d_languages] FOREIGN KEY ([language_id]) REFERENCES [nom].[n_d_languages] ([language_id]),
    CONSTRAINT [FK_pages_i18n_pages] FOREIGN KEY ([page_id]) REFERENCES [cms].[pages] ([page_id]),
    CONSTRAINT [FK_pages_i18n_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Превид на съдържание на сраница', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Превод на заглавие на страница', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на език', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'language_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на страница със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n', @level2type = N'COLUMN', @level2name = N'page_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща преводи на страници със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages_i18n';


GO





CREATE TRIGGER [cms].[trg_pages_i18n_aiud] ON [cms].[pages_i18n]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[cms].[pages_i18n]'
				END
GO
CREATE NONCLUSTERED INDEX [IDX_pages_i18n_language_id]
    ON [cms].[pages_i18n]([language_id] ASC)
    ON [INDEXES];

