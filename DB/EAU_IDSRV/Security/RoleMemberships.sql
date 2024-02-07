ALTER ROLE [db_owner] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_accessadmin] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_securityadmin] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_ddladmin] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_backupoperator] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_datareader] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_datareader] ADD MEMBER [eau_idsrv_user];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [eau_idsrv_owner];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [eau_idsrv_user];

