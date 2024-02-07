CREATE TABLE [aspnetcore].[data_protection_keys] (
    [id]              NVARCHAR (50)      NOT NULL,
    [keyxml]          NVARCHAR (MAX)     NOT NULL,
    [creation_date]   DATETIMEOFFSET (3) NOT NULL,
    [activation_date] DATETIMEOFFSET (3) NOT NULL,
    [expiration_date] DATETIMEOFFSET (3) NOT NULL,
    CONSTRAINT [xpk_data_protection_keys] PRIMARY KEY CLUSTERED ([id] ASC)
);

