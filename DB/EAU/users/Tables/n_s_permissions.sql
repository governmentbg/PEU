CREATE TABLE [users].[n_s_permissions] (
    [permission_id]  INT            NOT NULL,
    [permission_key] NVARCHAR (100) NOT NULL,
    [name]           NVARCHAR (200) NOT NULL,
    [order]          INT            NULL,
    [group_id]       INT            NULL,
    CONSTRAINT [xpk_n_s_permissions] PRIMARY KEY CLUSTERED ([permission_id] ASC)
);

