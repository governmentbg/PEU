CREATE TABLE [dbo].[service_instances] (
    [service_instance_id]     BIGINT             NOT NULL,
    [service_instance_ver_id] BIGINT             NOT NULL,
    [status]                  TINYINT            NOT NULL,
    [applicant_id]            INT                NULL,
    [service_instance_date]   DATETIME           NOT NULL,
    [service_id]              INT                NOT NULL,
    [service_ver_id]          BIGINT             NOT NULL,
    [case_file_uri]           NVARCHAR (100)     NOT NULL,
    [additional_data]         NVARCHAR (MAX)     NULL,
    [is_last]                 BIT                NOT NULL,
    [deactivation_ver_id]     INT                NULL,
    [created_by]              INT                NOT NULL,
    [created_on]              DATETIMEOFFSET (3) CONSTRAINT [DF_service_instances_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]              INT                NOT NULL,
    [updated_on]              DATETIMEOFFSET (3) CONSTRAINT [DF_service_instances_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_service_instances] PRIMARY KEY CLUSTERED ([service_instance_id] ASC, [service_instance_ver_id] ASC),
    CONSTRAINT [FK_service_instances_appl] FOREIGN KEY ([applicant_id]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_service_instances_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_service_instances_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_service_instances_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'is_last';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'УРИ на преписка.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'case_file_uri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия на услуга.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услуга.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'service_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на инстанцята.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'service_instance_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на заявител.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'applicant_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Статуси на инстанция на услуга. 1 = Текущ; 2 = Изпълнен; 3 = Прекратен;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на инстанция на услуга', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'service_instance_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на инстанция на услуга', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'service_instance_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Инстанции на услуги.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителни данни описващи заявленията.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'service_instances', @level2type = N'COLUMN', @level2name = N'additional_data';


GO
CREATE NONCLUSTERED INDEX [IDX_service_instances_status]
    ON [dbo].[service_instances]([status] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_service_instances_service_id]
    ON [dbo].[service_instances]([service_id] ASC)
    ON [INDEXES];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_service_instances_is_last]
    ON [dbo].[service_instances]([service_instance_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_service_instances_applicant_id]
    ON [dbo].[service_instances]([applicant_id] ASC)
    ON [INDEXES];

