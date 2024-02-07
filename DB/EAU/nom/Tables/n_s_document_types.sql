CREATE TABLE [nom].[n_s_document_types] (
    [doc_type_id] INT                NOT NULL,
    [name]        NVARCHAR (450)     NOT NULL,
    [uri]         NVARCHAR (100)     NULL,
    [type_id]     INT                NOT NULL,
    [created_by]  INT                NOT NULL,
    [created_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_document_kinds_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]  INT                NOT NULL,
    [updated_on]  DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_document_kinds_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_document_types] PRIMARY KEY CLUSTERED ([doc_type_id] ASC),
    CONSTRAINT [CK_n_s_document_types_type_id] CHECK ([type_id]=(1) OR [type_id]=(2)),
    CONSTRAINT [FK_n_d_document_kinds_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_document_kinds_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'created_by';


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на документ - EDocument = 1, AttachableDocument = 2', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'type_id';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на вид документ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на вид документ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'doc_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на видовете документи, които поддържа портала.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types';


GO

CREATE TRIGGER [nom].[trg_n_s_document_types_aiud] ON [nom].n_s_document_types
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_document_types]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Универсален идентификатор на ресурс', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_document_types', @level2type = N'COLUMN', @level2name = N'uri';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_n_s_document_types_uri]
    ON [nom].[n_s_document_types]([uri] ASC) WHERE ([uri] IS NOT NULL)
    ON [INDEXES];

