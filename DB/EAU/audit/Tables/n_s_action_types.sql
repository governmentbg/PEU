CREATE TABLE [audit].[n_s_action_types] (
    [action_type_id] TINYINT            NOT NULL,
    [name]           NVARCHAR (200)     NOT NULL,
    [description]    NVARCHAR (2000)    NOT NULL,
    [created_by]     INT                NOT NULL,
    [created_on]     DATETIMEOFFSET (3) CONSTRAINT [df_n_s_action_types_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]     INT                NOT NULL,
    [updated_on]     DATETIMEOFFSET (3) CONSTRAINT [df_n_s_action_types_uo] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_n_s_action_types] PRIMARY KEY CLUSTERED ([action_type_id] ASC),
    CONSTRAINT [fk_n_s_action_types_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_n_s_action_types_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Име на действието.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на обект.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на действие за одит.', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types', @level2type = N'COLUMN', @level2name = N'action_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Действия за одит', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'n_s_action_types';


GO

CREATE TRIGGER [audit].[trg_n_s_action_types_aiud] ON [audit].[n_s_action_types]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[audit].[n_s_action_types]'
				END