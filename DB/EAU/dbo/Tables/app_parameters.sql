CREATE TABLE [dbo].[app_parameters] (
    [app_param_id]        INT                NOT NULL,
    [app_param_ver_id]    BIGINT             NOT NULL,
    [functionality_id]    TINYINT            NOT NULL,
    [code]                NVARCHAR (100)     NOT NULL,
    [description]         NVARCHAR (500)     NOT NULL,
    [is_system]           BIT                NOT NULL,
    [param_type]          INT                NOT NULL,
    [value_datetime]      DATETIME           NULL,
    [value_interval]      DATETIME           NULL,
    [value_string]        NVARCHAR (2000)    NULL,
    [value_int]           INT                NULL,
    [value_hour]          TIME (7)           NULL,
    [is_last]             BIT                NOT NULL,
    [deactivation_ver_id] INT                NULL,
    [created_by]          INT                NOT NULL,
    [created_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_app_parameters_created_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    [updated_by]          INT                NOT NULL,
    [updated_on]          DATETIMEOFFSET (3) CONSTRAINT [DF_app_parameters_updated_on] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [PK_app_parameters] PRIMARY KEY CLUSTERED ([app_param_id] ASC, [app_param_ver_id] ASC),
    CONSTRAINT [CK_app_parameters_type] CHECK ([param_type]=(1) AND [value_datetime] IS NOT NULL AND [value_interval] IS NULL AND [value_string] IS NULL AND [value_int] IS NULL AND [value_hour] IS NULL OR [param_type]=(2) AND [value_datetime] IS NULL AND [value_interval] IS NOT NULL AND [value_string] IS NULL AND [value_int] IS NULL AND [value_hour] IS NULL OR [param_type]=(3) AND [value_datetime] IS NULL AND [value_interval] IS NULL AND [value_string] IS NOT NULL AND [value_int] IS NULL AND [value_hour] IS NULL OR [param_type]=(4) AND [value_datetime] IS NULL AND [value_interval] IS NULL AND [value_string] IS NULL AND [value_int] IS NOT NULL AND [value_hour] IS NULL OR [param_type]=(5) AND [value_datetime] IS NULL AND [value_interval] IS NULL AND [value_string] IS NULL AND [value_int] IS NULL AND [value_hour] IS NOT NULL),
    CONSTRAINT [FK_app_parameters_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_app_parameters_n_s_functionalities] FOREIGN KEY ([functionality_id]) REFERENCES [dbo].[n_s_functionalities] ([functionality_id]),
    CONSTRAINT [FK_app_parameters_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);












GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител направил последна промяна на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP на създаване на записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на потребител създал записа', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия, с която е деактивиран записът', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'deactivation_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'флаг, указващ дали това е последната версия на етикета', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'is_last';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност са тип параметър час и минути.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'value_hour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност са тип на параметър цяло число', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'value_int';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност са тип на параметър стринг', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'value_string';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност са тип на параметър интервал от време', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'value_interval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Стойност са тип на параметър дата, час и минути', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'value_datetime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип на параметър: 1-дата, час и минути; 2-период от време; 3-стринг; 4-цяло число; 5-час и минути;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'param_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Флаг, указващ дали параметъра е системен или не', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'is_system';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание на параметър', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код на параметър', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Уникален идентификатор на функционалност', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'functionality_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на версия на параметър', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'app_param_ver_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентиификатор на параметър', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters', @level2type = N'COLUMN', @level2name = N'app_param_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица съхраняваща конфигурационни параметри за системата.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'app_parameters';


GO
CREATE UNIQUE NONCLUSTERED INDEX [app_parameters_code_is_last_idx]
    ON [dbo].[app_parameters]([code] ASC) WHERE ([is_last]=(1));


GO



CREATE TRIGGER [dbo].[trg_app_parameters_aiud] ON [dbo].[app_parameters]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[dbo].[app_parameters]'
				END
GO
CREATE UNIQUE NONCLUSTERED INDEX [IDX_app_parameters_is_last]
    ON [dbo].[app_parameters]([app_param_id] ASC) WHERE ([is_last]=(1));


GO
CREATE NONCLUSTERED INDEX [IDX_app_parameters_param_type]
    ON [dbo].[app_parameters]([param_type] ASC)
    ON [INDEXES];


GO
CREATE NONCLUSTERED INDEX [IDX_app_parameters_functionality_id]
    ON [dbo].[app_parameters]([functionality_id] ASC)
    ON [INDEXES];

