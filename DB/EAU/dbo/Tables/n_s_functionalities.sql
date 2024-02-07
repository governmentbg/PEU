CREATE TABLE [dbo].[n_s_functionalities] (
    [functionality_id] TINYINT            NOT NULL,
    [name]             NVARCHAR (200)     NOT NULL,
    [description]      NVARCHAR (2000)    NOT NULL,
    [created_by]       INT                NOT NULL,
    [created_on]       DATETIMEOFFSET (3) CONSTRAINT [DF_n_s_functionalities_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]       INT                NOT NULL,
    [updated_on]       DATETIMEOFFSET (3) CONSTRAINT [DF_n_s_functionalities_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_n_s_functionalities] PRIMARY KEY CLUSTERED ([functionality_id] ASC),
    CONSTRAINT [FK_n_s_functionalities_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_n_s_functionalities_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакция.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаване на записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, направил записа.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'name';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на функционалност', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities', @level2type = N'COLUMN', @level2name = N'functionality_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Функционалности', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'n_s_functionalities';


GO

CREATE TRIGGER [dbo].[trg_n_s_functionalities_aiud] ON [dbo].[n_s_functionalities]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[dbo].[n_s_functionalities]'
				END