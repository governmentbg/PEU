CREATE ROLE [sql_dependency_subscriber]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [sql_dependency_subscriber] ADD MEMBER [eau_sql_dependency];


GO
ALTER ROLE [sql_dependency_subscriber] ADD MEMBER [eau_user];

