CREATE TABLE [nom].[n_s_document_template_fields] (
    [key]         NVARCHAR (50)  NOT NULL,
    [description] NVARCHAR (200) NULL,
    [created_by]  INT            NOT NULL,
    [created_on]  DATETIME       NOT NULL,
    [updated_by]  INT            NOT NULL,
    [updated_on]  DATETIME       NOT NULL,
    CONSTRAINT [PK_n_s_document_template_fields] PRIMARY KEY CLUSTERED ([key] ASC),
    CONSTRAINT [FK_n_s_document_template_fields_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_s_document_template_fields_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO


CREATE TRIGGER [nom].[trg_n_s_document_template_fields_aiud] ON [nom].n_s_document_template_fields
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_document_template_fields]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на поле', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на запис за поле във  шаблон за документ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields', @level2type = N'COLUMN', @level2name = N'key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статична номенклатура на полетата в шаблон за документ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_template_fields';

