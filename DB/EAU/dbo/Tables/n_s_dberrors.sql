CREATE TABLE [dbo].[n_s_dberrors] (
    [error_id]    INT             NOT NULL,
    [description] NVARCHAR (1000) NOT NULL,
    [created_by]  INT             NOT NULL,
    [created_on]  DATETIME        NOT NULL,
    [updated_by]  INT             NOT NULL,
    [updated_on]  DATETIME        NOT NULL,
    CONSTRAINT [xpk_n_s_dberrors] PRIMARY KEY CLUSTERED ([error_id] ASC),
    CONSTRAINT [fk_n_s_dberrors_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id]),
    CONSTRAINT [fk_n_s_dberrors_ub] FOREIGN KEY ([updated_by]) REFERENCES [users].[users] ([user_id])
);

