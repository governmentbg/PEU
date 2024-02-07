CREATE TABLE [nom].[n_s_ekatte_types] (
    [type_id]    TINYINT        NOT NULL,
    [name]       NVARCHAR (100) NOT NULL,
    [created_by] INT            NOT NULL,
    [created_on] DATETIME       NOT NULL,
    [updated_by] INT            NOT NULL,
    [updated_on] DATETIME       NOT NULL,
    CONSTRAINT [XPK_N_S_EKATTE_TYPES] PRIMARY KEY CLUSTERED ([type_id] ASC),
    CONSTRAINT [FK_USER_EKATTE_TYPE_CREATED_BY] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_USER_EKATTE_TYPE_UPDATED_BY] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на тип.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types', @level2type = N'COLUMN', @level2name = N'type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържа типовете ЕКАТТЕ записи.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte_types';


GO


CREATE TRIGGER [nom].[trg_n_s_ekatte_types_aiud] ON [nom].[n_s_ekatte_types]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_ekatte_types]'
				END