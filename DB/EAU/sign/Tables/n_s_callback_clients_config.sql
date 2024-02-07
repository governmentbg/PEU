CREATE TABLE [sign].[n_s_callback_clients_config] (
    [callback_client_id] INT                NOT NULL,
    [base_url]           NVARCHAR (250)     NOT NULL,
    [http_client_name]   NVARCHAR (100)     NULL,
    [created_by]         INT                NOT NULL,
    [created_on]         DATETIMEOFFSET (3) CONSTRAINT [df_n_s_callback_clients_config_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]         INT                NOT NULL,
    [updated_on]         DATETIMEOFFSET (3) CONSTRAINT [df_n_s_callback_clients_config_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [pk_n_s_callback_clients_config] PRIMARY KEY CLUSTERED ([callback_client_id] ASC),
    CONSTRAINT [fk_n_s_callback_clients_config_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_n_s_callback_clients_config_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на последна редакция.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител последно редактирал записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата на създаване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребител създал записа.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на клиент от конфигурацията.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'http_client_name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Базов адрес на клиент.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'base_url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на клиент ползващ модула за подписване.', @level0type = N'SCHEMA', @level0name = N'sign', @level1type = N'TABLE', @level1name = N'n_s_callback_clients_config', @level2type = N'COLUMN', @level2name = N'callback_client_id';

