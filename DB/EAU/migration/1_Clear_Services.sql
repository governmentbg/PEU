BEGIN TRANSACTION

--[dbo].[app_parameters]
DELETE [dbo].[app_parameters]
where is_last <> 1

UPDATE [dbo].[app_parameters]
SET updated_by = 1,
	created_by = 1

truncate table [audit].[log_actions]

delete attached_documents

delete document_process_contents

delete document_processes

delete pmt.payment_requests 

delete pmt.obligations

delete [dbo].[service_instances]

delete [sign].[signers]

delete [sign].[signing_processes]

--USERS 

truncate table [users].[login_sessions]

truncate table [users].[user_processes]

truncate table users.users_h

truncate table [users].[login_sessions]

truncate table [users].[user_failed_login_attempts]


delete [users].[user_permissions]
where user_id not in (select user_id from [users].[user_authentications] where authentication_type = 2) 
	and user_id  not in (1,2)

update [users].[user_permissions]
set created_by = 1,
	updated_by = 1


delete [dbo].[data_service_user_limits]

delete [dbo].[data_service_limits]
where is_last <> 1

update [dbo].[data_service_limits]
set updated_by = 1,
	created_by = 1

delete [users].[user_authentications]
where authentication_type <> 2

update [users].[user_authentications]
set created_by = 1,
	updated_by = 1

delete [users].[certificates]	

--TODO: Дали да не се изтрият
delete versions
where version_id <> (select MAX(version_id) from versions)

update versions
set created_by = 1

truncate table [eml].[email_messages]

update users.users
set created_by = 1,
	updated_by = 1

COMMIT

