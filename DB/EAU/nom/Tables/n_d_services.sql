CREATE TABLE [nom].[n_d_services] (
    [service_id]                                     INT                NOT NULL,
    [service_ver_id]                                 BIGINT             NOT NULL,
    [group_id]                                       INT                NULL,
    [group_ver_id]                                   BIGINT             NULL,
    [doc_type_id]                                    INT                NULL,
    [name]                                           NVARCHAR (1000)    NOT NULL,
    [sunau_service_uri]                              NVARCHAR (100)     NULL,
    [initiation_type_id]                             TINYINT            NOT NULL,
    [result_document_name]                           NVARCHAR (1000)    NULL,
    [description]                                    NVARCHAR (MAX)     NULL,
    [explanatory_text_service]                       NVARCHAR (MAX)     NULL,
    [explanatory_text_fulfilled_service]             NVARCHAR (MAX)     NULL,
    [explanatory_text_refused_or_terminated_service] NVARCHAR (MAX)     NULL,
    [order_number]                                   INT                NOT NULL,
    [adm_structure_unit_name]                        NVARCHAR (500)     NULL,
    [attached_documents_description]                 NVARCHAR (MAX)     NULL,
    [additional_configuration]                       NVARCHAR (MAX)     NULL,
    [service_url]                                    NVARCHAR (1000)    NULL,
    [is_active]                                      BIT                NOT NULL,
    [is_last]                                        BIT                NOT NULL,
    [deactivation_ver_id]                            INT                NULL,
    [created_by]                                     INT                NOT NULL,
    [created_on]                                     DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_services_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                                     INT                NOT NULL,
    [updated_on]                                     DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_services_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_services] PRIMARY KEY CLUSTERED ([service_id] ASC, [service_ver_id] ASC),
    CONSTRAINT [CK_n_d_services_initiation_type_id] CHECK ([initiation_type_id]=(1) OR [initiation_type_id]=(2)),
    CONSTRAINT [FK_n_d_services_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_services_n_d_service_groups] FOREIGN KEY ([group_id], [group_ver_id]) REFERENCES [nom].[n_d_service_groups] ([group_id], [group_ver_id]),
    CONSTRAINT [FK_n_d_services_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_s_document_tpyes_n_d_services] FOREIGN KEY ([doc_type_id]) REFERENCES [nom].[n_s_document_types] ([doc_type_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'is_last';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на структурна еденица  от администрацията или длъжностно лице до което се подава заявлението – използва се при визуализация и печат.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'adm_structure_unit_name';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'html съдържание за описание на административната услуга. УРИ на обекта по РИО - 0008-000079', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на документа получен в резултат изпълнението на услугата. (Ползва се когато с едно заявление може да се заявят множе се стартират различни услуги.)', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'result_document_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Начин на стартиране на услугата (възможни стоиности 1- чрез заявление, 2 - чрез препращане към web страница)', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'initiation_type_id';


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование  на услугата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на група, връзка към n_d_service_groups', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'group_ver_id';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на група, връзка към n_d_service_groups', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'group_id';




GO
CREATE TRIGGER [nom].[trg_n_d_services_aiud] ON nom.n_d_services
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_services]'
				END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура на административните услуги.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на услуга', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услуга', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'service_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'УРИ на административна услуга', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'sunau_service_uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пореден номер на услугата за група.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'order_number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг указващ дали услугата е активна', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'is_active';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на документа инициращ услугата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'doc_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на прилаганите документи', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'attached_documents_description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Доплънителна конфигурация', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'additional_configuration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пояснителен текст към услуга', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'explanatory_text_service';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пояснителен текст при отказана/прекратена услуга', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'explanatory_text_refused_or_terminated_service';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пояснителен текст при изпълнена услуга ', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'explanatory_text_fulfilled_service';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_n_d_services_name]
    ON [nom].[n_d_services]([name] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_services_is_last]
    ON [nom].[n_d_services]([service_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_services_group_id]
    ON [nom].[n_d_services]([group_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_services_doc_type_id]
    ON [nom].[n_d_services]([doc_type_id] ASC)
    ON [INDEXES];


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_n_d_services_sunau_uri]
    ON [nom].[n_d_services]([sunau_service_uri] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес на услугата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_services', @level2type = N'COLUMN', @level2name = N'service_url';

