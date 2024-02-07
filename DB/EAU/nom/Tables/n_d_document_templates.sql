CREATE TABLE [nom].[n_d_document_templates] (
    [doc_template_id]                    INT                NOT NULL,
    [doc_template_ver_id]                BIGINT             NOT NULL,
    [document_type_id]                   INT                NOT NULL,
    [content]                            NVARCHAR (MAX)     NULL,
    [is_submitted_according_to_template] BIT                NOT NULL,
    [is_last]                            BIT                NOT NULL,
    [deactivation_ver_id]                INT                NULL,
    [created_by]                         INT                NOT NULL,
    [created_on]                         DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_document_templates_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                         INT                NOT NULL,
    [updated_on]                         DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_document_templates_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_document_templates] PRIMARY KEY CLUSTERED ([doc_template_id] ASC, [doc_template_ver_id] ASC),
    CONSTRAINT [FK_n_d_document_templates_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_document_templates_n_s_document_types] FOREIGN KEY ([document_type_id]) REFERENCES [nom].[n_s_document_types] ([doc_type_id]),
    CONSTRAINT [FK_n_d_document_templates_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO


CREATE TRIGGER [nom].[trg_n_d_document_templates_aiud] ON [nom].n_d_document_templates
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_document_templates]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия, с която е деактивиран записът', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Подава се само по шаблон', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'is_submitted_according_to_template';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържание на шаблон', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на вид документ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'document_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на версия на шаблон за документ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'doc_template_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на запис за шаблон на документ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates', @level2type = N'COLUMN', @level2name = N'doc_template_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Шаблони за документи', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_document_templates';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_document_templates_document_type_id]
    ON [nom].[n_d_document_templates]([document_type_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_document_templates_is_last]
    ON [nom].[n_d_document_templates]([doc_template_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

