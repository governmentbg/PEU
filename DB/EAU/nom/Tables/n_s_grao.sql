CREATE TABLE [nom].[n_s_grao] (
    [grao_id]         INT            NOT NULL,
    [name]            NVARCHAR (100) NOT NULL,
    [district_id]     INT            NULL,
    [municipality_id] INT            NULL,
    [settlement_id]   INT            NULL,
    [parent_id]       INT            NULL,
    [grao_type]       TINYINT        CONSTRAINT [DF_N_S_GRAO_TYPE] DEFAULT ((0)) NOT NULL,
    [code]            NVARCHAR (50)  NULL,
    [created_by]      INT            NOT NULL,
    [created_on]      DATETIME       NOT NULL,
    [updated_by]      INT            NOT NULL,
    [updated_on]      DATETIME       NOT NULL,
    CONSTRAINT [XPKN_S_GRAO] PRIMARY KEY CLUSTERED ([grao_id] ASC),
    CONSTRAINT [FK_GRAO_MUNICIPALITY] FOREIGN KEY ([municipality_id]) REFERENCES [nom].[n_s_grao] ([grao_id]),
    CONSTRAINT [FK_GRAO_PARENT] FOREIGN KEY ([parent_id]) REFERENCES [nom].[n_s_grao] ([grao_id]),
    CONSTRAINT [FK_GRAO_REGION] FOREIGN KEY ([district_id]) REFERENCES [nom].[n_s_grao] ([grao_id]),
    CONSTRAINT [FK_GRAO_TOWN] FOREIGN KEY ([settlement_id]) REFERENCES [nom].[n_s_grao] ([grao_id]),
    CONSTRAINT [FK_GRAO_TYPE] FOREIGN KEY ([grao_type]) REFERENCES [nom].[n_s_grao_types] ([type_id]),
    CONSTRAINT [FK_USER_GRAO_CREATED_BY] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_USER_GRAO_UPDATED_BY] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на тип запис в GRAO.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'grao_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на елемент-родител в йерархията - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'parent_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на град - запис също от GRAO.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'settlement_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на община - запис също от GRAO.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'municipality_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на регион - запис също от GRAO.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'district_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на запис от GRAO.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao', @level2type = N'COLUMN', @level2name = N'grao_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържа данни от GRAO системата.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_grao';


GO


CREATE TRIGGER [nom].[trg_n_s_grao_aiud] ON [nom].[n_s_grao]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_grao]'
				END
GO
CREATE NONCLUSTERED INDEX [IDX_n_s_grao_grao_type]
    ON [nom].[n_s_grao]([grao_type] ASC)
    ON [INDEXES];

