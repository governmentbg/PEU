CREATE TABLE [nom].[n_d_service_document_types] (
    [service_id]                   INT                NOT NULL,
    [service_ver_id]               BIGINT             NOT NULL,
    [doc_type_id]                  INT                NOT NULL,
    [service_document_kind_ver_id] BIGINT             NOT NULL,
    [is_last]                      BIT                NOT NULL,
    [deactivation_ver_id]          INT                NULL,
    [created_by]                   INT                NOT NULL,
    [created_on]                   DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_document_types_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                   INT                NOT NULL,
    [updated_on]                   DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_document_types_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_service_document_types] PRIMARY KEY CLUSTERED ([service_document_kind_ver_id] ASC, [service_id] ASC, [doc_type_id] ASC),
    CONSTRAINT [FK_n_d_service_document_types_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_service_document_types_n_d_document_types] FOREIGN KEY ([doc_type_id]) REFERENCES [nom].[n_s_document_types] ([doc_type_id]),
    CONSTRAINT [FK_n_d_service_document_types_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_n_d_service_document_types_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на вид документ, връзка към n_d_document_kinds', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'doc_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на услуга връзка към n_d_services', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услуга връзка към n_d_services', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types', @level2type = N'COLUMN', @level2name = N'service_id';


GO

CREATE TRIGGER [nom].[trg_n_d_service_document_types_aiud] ON [nom].n_d_service_document_types
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_service_document_types]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_document_types_srv_id_dec_id]
    ON [nom].[n_d_service_document_types]([service_id] ASC, [doc_type_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура видовете документи от които заявителя на услугата ще може да избира когато прилага документи', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_document_types';


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_document_types_service_id]
    ON [nom].[n_d_service_document_types]([service_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_document_types_doc_type_id]
    ON [nom].[n_d_service_document_types]([doc_type_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

