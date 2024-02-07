CREATE TABLE [cms].[pages] (
    [page_id]    INT                NOT NULL,
    [code]       NVARCHAR (200)     NOT NULL,
    [title]      NVARCHAR (200)     NOT NULL,
    [content]    NVARCHAR (MAX)     NULL,
    [created_by] INT                NOT NULL,
    [created_on] DATETIMEOFFSET (3) CONSTRAINT [DF_pages_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by] INT                NOT NULL,
    [updated_on] DATETIMEOFFSET (3) CONSTRAINT [DF_pages_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_pages] PRIMARY KEY CLUSTERED ([page_id] ASC),
    CONSTRAINT [FK_pages_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_pages_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [pages_code_idx]
    ON [cms].[pages]([code] ASC)
    ON [INDEXES];


GO




CREATE TRIGGER [cms].[trg_pages_aiud] ON [cms].[pages]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[cms].[pages]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HTML съдържание на страница със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Заглавие на страница със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на страница със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на страница със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages', @level2type = N'COLUMN', @level2name = N'page_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща страници със съдържание.', @level0type = N'SCHEMA', @level0name = N'cms', @level1type = N'TABLE', @level1name = N'pages';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_pages_code]
    ON [cms].[pages]([code] ASC)
    ON [INDEXES];

