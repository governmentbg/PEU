﻿CREATE TABLE [idsrv].[DeviceCodes] (
    [UserCode]     NVARCHAR (200) NOT NULL,
    [DeviceCode]   NVARCHAR (200) NOT NULL,
    [SubjectId]    NVARCHAR (200) NULL,
    [ClientId]     NVARCHAR (200) NOT NULL,
    [CreationTime] DATETIME2 (7)  NOT NULL,
    [Expiration]   DATETIME2 (7)  NOT NULL,
    [Data]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_DeviceCodes] PRIMARY KEY CLUSTERED ([UserCode] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DeviceCodes_DeviceCode]
    ON [idsrv].[DeviceCodes]([DeviceCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DeviceCodes_Expiration]
    ON [idsrv].[DeviceCodes]([Expiration] ASC);

