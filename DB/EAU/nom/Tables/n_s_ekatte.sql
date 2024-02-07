CREATE TABLE [nom].[n_s_ekatte] (
    [ekatte_id]       INT            NOT NULL,
    [name]            NVARCHAR (100) NOT NULL,
    [district_id]     INT            NULL,
    [municipality_id] INT            NULL,
    [settlement_id]   INT            NULL,
    [area_id]         INT            NULL,
    [mayoralty_id]    INT            NULL,
    [parent_id]       INT            NULL,
    [ekatte_type]     TINYINT        CONSTRAINT [DF_N_S_EKATTE_TYPE] DEFAULT ((0)) NOT NULL,
    [code]            NVARCHAR (50)  NULL,
    [created_by]      INT            NOT NULL,
    [created_on]      DATETIME       NOT NULL,
    [updated_by]      INT            NOT NULL,
    [updated_on]      DATETIME       NOT NULL,
    CONSTRAINT [XPKN_S_EKATTE] PRIMARY KEY CLUSTERED ([ekatte_id] ASC),
    CONSTRAINT [FK_EKATTE_MAYORALTY] FOREIGN KEY ([mayoralty_id]) REFERENCES [nom].[n_s_ekatte] ([ekatte_id]),
    CONSTRAINT [FK_EKATTE_MUNICIPALITY] FOREIGN KEY ([municipality_id]) REFERENCES [nom].[n_s_ekatte] ([ekatte_id]),
    CONSTRAINT [FK_EKATTE_PARENT] FOREIGN KEY ([parent_id]) REFERENCES [nom].[n_s_ekatte] ([ekatte_id]),
    CONSTRAINT [FK_EKATTE_REGION] FOREIGN KEY ([district_id]) REFERENCES [nom].[n_s_ekatte] ([ekatte_id]),
    CONSTRAINT [FK_EKATTE_TOWN] FOREIGN KEY ([settlement_id]) REFERENCES [nom].[n_s_ekatte] ([ekatte_id]),
    CONSTRAINT [FK_EKATTE_TYPE] FOREIGN KEY ([ekatte_type]) REFERENCES [nom].[n_s_ekatte_types] ([type_id]),
    CONSTRAINT [FK_USER_EKATTE_CREATED_BY] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [FK_USER_EKATTE_UPDATED_BY] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на редакцията.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'updated_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, редактирал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'updated_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и час на създаването на записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'created_on';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на потребителя, създал записа.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'created_by';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на тип запис в ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'ekatte_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на елемент-родител в йерархията - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'parent_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на кметство - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'mayoralty_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на район - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'area_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на град - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'settlement_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на община - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'municipality_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на регион - запис също от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'district_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Идентификатор на запис от ЕКАТТЕ.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte', @level2type = N'COLUMN', @level2name = N'ekatte_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Съдържа данни от ЕКАТТЕ системата.', @level0type = N'SCHEMA', @level0name = N'nom', @level1type = N'TABLE', @level1name = N'n_s_ekatte';


GO


CREATE TRIGGER [nom].[trg_n_s_ekatte_aiud] ON [nom].[n_s_ekatte]
				FOR INSERT, UPDATE, DELETE AS BEGIN
				SET NOCOUNT ON
				EXEC [dbo].[p_sys_cache_update_nom_changes] '[nom].[n_s_ekatte]'
				END
GO
CREATE NONCLUSTERED INDEX [IDX_n_s_ekatte_ekatte_type]
    ON [nom].[n_s_ekatte]([ekatte_type] ASC)
    ON [INDEXES];

