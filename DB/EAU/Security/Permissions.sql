GRANT EXECUTE
    ON SCHEMA::[dbo] TO [eau_user];


GO
GRANT RECEIVE
    ON OBJECT::[dbo].[QueryNotificationErrorsQueue] TO [eau_sql_dependency]
    AS [dbo];


GO
GRANT REFERENCES
    ON CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification] TO [eau_sql_dependency];

