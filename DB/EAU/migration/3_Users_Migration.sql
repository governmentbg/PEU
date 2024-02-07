BEGIN TRANSACTION

INSERT INTO [users].[users]
SELECT 
	P.USER_PROFILE_ID,
	P.E_MAIL,
	2, --Aктиве
	0, --системен
	1, 
	ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	1,
	ToDateTimeOffset(P.UPDATED_ON, '+02:00'),
	NEXT VALUE FOR [users].[seq_users_cin]
FROM MVR_ESP_PROD.dbo.PUBLIC_USER_PROFILES P
where HISTORY_ID = 0 and IS_LAST = 1 and DEACTIVATION_DATE is null and USER_PROFILE_STATUS = 2 and P.USER_PROFILE_ID <> 1;

INSERT INTO [users].[user_authentications]
SELECT 
	P.USER_PROFILE_ID,
	P.USER_PROFILE_ID,
	1, --автентикаци¤ с потребителско име и парола
	null,
	null,
	null,
	0, --записа не е заключен
	null,
	1,
	1,
	ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	1,
	ToDateTimeOffset(P.UPDATED_ON, '+02:00')	
FROM MVR_ESP_PROD.dbo.PUBLIC_USER_PROFILES P
where HISTORY_ID = 0 and IS_LAST = 1 and DEACTIVATION_DATE is null and USER_PROFILE_STATUS = 2 and P.USER_PROFILE_ID <> 1;


COMMIT

declare @user_id int

set @user_id = (select max(user_id) + 1 from users.users)

DECLARE @sql NVARCHAR(MAX)

SET @sql = '
ALTER SEQUENCE [users].[seq_users]  
 RESTART  WITH ' + CONVERT(nvarchar, @user_id) 

EXEC(@sql) 

SET @sql = '
ALTER SEQUENCE [users].[seq_user_authentications] 
 RESTART  WITH ' + CONVERT(nvarchar, @user_id)

EXEC(@sql) 


