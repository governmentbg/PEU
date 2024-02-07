ALTER ROLE [db_owner] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_accessadmin] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_securityadmin] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_ddladmin] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_backupoperator] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_datareader] ADD MEMBER [eau_owner];


GO
ALTER ROLE [db_datareader] ADD MEMBER [eau_user];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [eau_owner];

