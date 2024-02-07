CREATE TABLE [audit].[log_actions] (
    [log_action_id]    BIGINT             NOT NULL,
    [log_action_date]  DATETIME           NOT NULL,
    [object_type_id]   TINYINT            NOT NULL,
    [action_type_id]   TINYINT            NOT NULL,
    [functionality_id] TINYINT            NOT NULL,
    [key]              NVARCHAR (100)     NULL,
    [login_session_id] UNIQUEIDENTIFIER   NULL,
    [user_id]          INT                NOT NULL,
    [ip_address]       VARBINARY (16)     NOT NULL,
    [additional_data]  NVARCHAR (MAX)     NULL,
    [created_by]       INT                NOT NULL,
    [created_on]       DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [xpk_log_actions] PRIMARY KEY CLUSTERED ([log_action_id] ASC) ON [AUDIT],
    CONSTRAINT [fk_log_actions_atid] FOREIGN KEY ([action_type_id]) REFERENCES [audit].[n_s_action_types] ([action_type_id]),
    CONSTRAINT [fk_log_actions_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_log_actions_fid] FOREIGN KEY ([functionality_id]) REFERENCES [dbo].[n_s_functionalities] ([functionality_id]),
    CONSTRAINT [fk_log_actions_otid] FOREIGN KEY ([object_type_id]) REFERENCES [audit].[n_s_object_types] ([object_type_id]),
    CONSTRAINT [fk_log_actions_uid] FOREIGN KEY ([user_id]) REFERENCES [users].[users] ([user_id])
) ON [AUDIT];












GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребителски профил', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'user_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на обект, за който е събитието', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'object_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на логин сесия', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'login_session_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на събитието', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'log_action_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на настъпване на събитието', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'log_action_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност на ключов атрибут на обекта', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'key';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP адрес на потребителя, извършващ действието', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'ip_address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Функционалност през която е настъпило събитието', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'functionality_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Допълнителна информация', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'additional_data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Събитие, за което се записват данните за одит', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions', @level2type = N'COLUMN', @level2name = N'action_type_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Одитен журнал', @level0type = N'SCHEMA', @level0name = N'audit', @level1type = N'TABLE', @level1name = N'log_actions';


GO
CREATE NONCLUSTERED INDEX [IDX_log_actions_user_id]
    ON [audit].[log_actions]([user_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_log_actions_object_type_id]
    ON [audit].[log_actions]([object_type_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_log_actions_functionality_id]
    ON [audit].[log_actions]([functionality_id] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_log_actions_action_type_id]
    ON [audit].[log_actions]([action_type_id] ASC)
    ON [INDEXES];

