CREATE TABLE [nom].[n_s_countries] (
    [country_id] INT                NOT NULL,
    [name]       NVARCHAR (100)     NOT NULL,
    [cod_l]      NVARCHAR (2)       NULL,
    [created_by] INT                NOT NULL,
    [created_on] DATETIMEOFFSET (3) NOT NULL,
    [updated_by] INT                NOT NULL,
    [updated_on] DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [XPKN_S_COUNTRIES] PRIMARY KEY CLUSTERED ([country_id] ASC),
    CONSTRAINT [FK_USER_COUNTRY_CREATED_BY] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_USER_COUNTRY_UPDATED_BY] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO

CREATE TRIGGER [nom].[trg_n_s_countries_aiud] ON [nom].n_s_countries
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_countries]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция на запис на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя редактирал запис на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на запис на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя създал запис на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код на държава на латиница.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'cod_l';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на държава.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries', @level2type = N'COLUMN', @level2name = N'country_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статична номенклатура на държавите.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_countries';

