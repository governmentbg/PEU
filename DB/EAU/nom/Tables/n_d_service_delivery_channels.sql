CREATE TABLE [nom].[n_d_service_delivery_channels] (
    [service_delivery_channel_ver_id] BIGINT             NOT NULL,
    [service_id]                      INT                NOT NULL,
    [service_ver_id]                  BIGINT             NOT NULL,
    [delivery_channel_id]             TINYINT            NOT NULL,
    [is_last]                         BIT                NOT NULL,
    [deactivation_ver_id]             INT                NULL,
    [created_by]                      INT                NOT NULL,
    [created_on]                      DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_delivery_channels_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]                      INT                NOT NULL,
    [updated_on]                      DATETIMEOFFSET (3) CONSTRAINT [DF_n_d_service_delivery_channels_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_d_service_delivery_channels] PRIMARY KEY CLUSTERED ([service_delivery_channel_ver_id] ASC, [service_id] ASC, [delivery_channel_id] ASC),
    CONSTRAINT [FK_n_d_service_delivery_channels_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_d_service_delivery_channels_n_d_services] FOREIGN KEY ([service_id], [service_ver_id]) REFERENCES [nom].[n_d_services] ([service_id], [service_ver_id]),
    CONSTRAINT [FK_n_d_service_delivery_channels_n_s_delivery_channels] FOREIGN KEY ([delivery_channel_id]) REFERENCES [nom].[n_s_delivery_channels] ([delivery_channel_id]),
    CONSTRAINT [FK_n_d_service_delivery_channels_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);










GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на деактивиране на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на начините на предаване, връзка към n_s_delivery_channels', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'delivery_channel_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Версия на услуга, връзка към n_d_services', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'service_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на услуга, връзка към n_d_services', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels', @level2type = N'COLUMN', @level2name = N'service_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура начини на предаване на административните услуги в административните структури.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_d_service_delivery_channels';


GO

CREATE TRIGGER [nom].[trg_n_d_service_delivery_channels_aiud] ON [nom].[n_d_service_delivery_channels]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_d_service_delivery_channels]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_n_d_service_delivery_channels_srv_id_del_id]
    ON [nom].[n_d_service_delivery_channels]([service_id] ASC, [delivery_channel_id] ASC) WHERE ([is_last]=(1) AND [deactivation_ver_id] IS NULL);




GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_delivery_channels_service_id_is_last]
    ON [nom].[n_d_service_delivery_channels]([service_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_n_d_service_delivery_channels_delivery_channel_id]
    ON [nom].[n_d_service_delivery_channels]([delivery_channel_id] ASC) WHERE ([is_last]=(1))
    ON [INDEXES];

