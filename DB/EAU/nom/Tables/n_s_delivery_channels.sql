CREATE TABLE [nom].[n_s_delivery_channels] (
    [delivery_channel_id] TINYINT            NOT NULL,
    [name]                NVARCHAR (200)     NOT NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_s_delivery_channels_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_n_s_delivery_channels_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [XPK_N_S_DELIVERY_CHANNELS] PRIMARY KEY CLUSTERED ([delivery_channel_id] ASC),
    CONSTRAINT [FK_n_s_delivery_channels_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_s_delivery_channels_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_delivery_channels', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_delivery_channels', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_delivery_channels', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номенклатура на начините на предаване.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_delivery_channels';




GO

CREATE TRIGGER [nom].[trg_n_s_delivery_channels_aiud] ON [nom].n_s_delivery_channels
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_delivery_channels]'
				END