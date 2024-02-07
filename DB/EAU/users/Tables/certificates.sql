CREATE TABLE [users].[certificates] (
    [certificate_id] INT                NOT NULL,
    [serial_number]  NVARCHAR (100)     NOT NULL,
    [issuer]         NVARCHAR (500)     NULL,
    [subject]        NVARCHAR (500)     NULL,
    [not_after]      DATETIMEOFFSET (3) NOT NULL,
    [not_before]     DATETIMEOFFSET (3) NOT NULL,
    [thumbprint]     VARCHAR (50)       NOT NULL,
    [content]        VARBINARY (MAX)    NOT NULL,
    [created_by]     INT                NOT NULL,
    [created_on]     DATETIMEOFFSET (3) CONSTRAINT [df_certificates_co] DEFAULT ([dbo].[f_sys_get_time]()) NOT NULL,
    CONSTRAINT [xpk_certificates] PRIMARY KEY CLUSTERED ([certificate_id] ASC),
    CONSTRAINT [fk_certificates_cb] FOREIGN KEY ([created_by]) REFERENCES [users].[users] ([user_id])
);

