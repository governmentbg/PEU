CREATE TABLE [nom].[n_d_service_groups] (
    [group_id]            INT                NOT NULL,
    [group_ver_id]        BIGINT             NOT NULL,
    [name]                NVARCHAR (1000)    NOT NULL,
    [order_number]        INT                NOT NULL,
    [icon_name]           VARCHAR (100)      NULL,
    [is_active]           BIT                NOT NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_groups_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_groups_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_service_groups] PRIMARY KEY CLUSTERED ([group_id] ASC, [group_ver_id] ASC),
    CONSTRAINT [FK_USER_SERVICE_GROUP_CREATED_BY] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_USER_SERVICE_GROUP_UPDATED_BY] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща групи услуги по направление на дейност в МВР', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'group_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование на групата', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'name';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'updated_on';


GO

CREATE TRIGGER [nom].[trg_n_d_service_groups_aiud] ON [nom].n_d_service_groups
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_service_groups]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_groups_id]
    ON [nom].[n_d_service_groups]([group_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг указващ дали групата е активна', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_groups', @level2type = N'COLUMN', @level2name = N'is_active';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_groups_name]
    ON [nom].[n_d_service_groups]([name] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_groups]
    ON [nom].[n_d_service_groups]([group_id] ASC) WHERE ([is_last]=(1));

