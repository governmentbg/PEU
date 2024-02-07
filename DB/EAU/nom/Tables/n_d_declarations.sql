CREATE TABLE [nom].[n_d_declarations] (
    [delcaration_id]                     INT                NOT NULL,
    [declaration_ver_id]                 BIGINT             NOT NULL,
    [description]                        NVARCHAR (1000)    NOT NULL,
    [content]                            NVARCHAR (MAX)     NOT NULL,
    [is_required]                        BIT                NOT NULL,
    [is_additional_description_required] BIT                NOT NULL,
    [code]                               NVARCHAR (100)     NOT NULL,
    [is_last]                            BIT                NOT NULL,
    [deactivation_ver_id]                INT                NULL,
    [created_by]                         INT                NOT NULL,
    [created_on]                         DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_declarations_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                         INT                NOT NULL,
    [updated_on]                         DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_declarations_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_declarations] PRIMARY KEY CLUSTERED ([delcaration_id] ASC, [declaration_ver_id] ASC),
    CONSTRAINT [FK_n_d_declarations_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_declarations_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код на декларативно обстоятелство/ политика', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Изисква допълнително поле за въвеждане на описание', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'is_additional_description_required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Задължително за маркиране в заявленията', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'is_required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържание', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на декларативно обстоятелство/ политика', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'declaration_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на декларативно обстоятелство/ политика', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations', @level2type = N'COLUMN', @level2name = N'delcaration_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура на декларативно обстоятелство/ политика', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_declarations';


GO

CREATE TRIGGER [nom].[trg_n_d_declarations_aiud] ON [nom].[n_d_declarations]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_declarations]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_declarations_id]
    ON [nom].[n_d_declarations]([delcaration_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_declarations_is_last]
    ON [nom].[n_d_declarations]([delcaration_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

