BEGIN TRANSACTION

INSERT INTO [dbo].[service_instances]
SELECT 
	si.SERVICE_INSTANCE_ID,
	si.SERVICE_INSTANCE_VER_ID,
	si.STATUS,
	si.APPLICANT_ID,
	si.SERVICE_INSTANCE_DATE,
	si.SERVICE_ID,
	si.SERVICE_VER_ID,
	si.CASE_FILE_URI,
	N'{"subStatus":"' + si.SUB_STATUS + N'","lastStage":"' + si.LAST_STAGE + N'","lastStageActualCompletionDate":"' + FORMAT(si.STATUS_DATE, 'dd.MM.yyyy') +N'"}',
	1,
	null,
	1,
	ToDateTimeOffset(si.CREATED_ON,'+02:00'),
	1,
	ToDateTimeOffset(si.UPDATED_ON,'+02:00'),
	si.STATUS_DATE
FROM (
	SELECT 
		pa.USER_PROFILE_APPLICATION_ID AS SERVICE_INSTANCE_ID,
		pa.VERSION_ID AS SERVICE_INSTANCE_VER_ID,
		case si.STATUS_ID
			when 3 then 2 -- "Изпълнен" отива в "Изпълнен"  
			else 3 -- Всичко друго отива в "Прекратен"
		end as STATUS,
		pa.USER_PROFILE_ID AS APPLICANT_ID,
		si.DATE_STARTED AS SERVICE_INSTANCE_DATE,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then 351
			else (select service_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		end as SERVICE_ID,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then (select service_ver_id from nom.n_d_services where service_id = 351 and is_last = 1) 
			else (select service_ver_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		END as SERVICE_VER_ID,
		pa.CASE_FILE_URI,
		case si.STATUS_ID
			when 1 then 'Termination' -- "В процес на изпълнение" автоматично се прекратява
			when 2 then 'Termination' -- "В изчакване на отговор" автоматично се прекратява
			when 3 then 'Completed'
			when 4 then 'Cancelled'
			when 5 then 'CancelIssuingAdministrativeAct'
			when 6 then 'Termination' -- "Изчакване на заявление с отстранени нередовности" автоматично се прекратява
			when 7 then 'Termination'
			when 8 then 'OutstandingConditions'
			when 9 then 'Termination' -- "Изчаква плащане" автоматично се прекратява
		end AS SUB_STATUS,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 st.NAME from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					inner join MVR_WAIS_PROD.dbo.N_D_STAGES st on st.STAGE_ID = sti.STAGE_ID and st.HISTORY_ID = 0
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else N'Прекратяване на електронна услуга' --Виски неизпълнени се слагат като прекратени
		END AS LAST_STAGE,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 sti.DATE_FINISHED from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else SYSDATETIME() --TODO каква дада да ползваме
		END AS STATUS_DATE,
		pa.CREATED_ON,
		pa.UPDATED_ON 
	FROM MVR_WAIS_PROD.dsi.SERVICE_INSTANCES si
	inner join MVR_WAIS_PROD.df.CASE_FILES cf on cf.CASE_FILE_ID = si.CASE_FILE_ID and cf.HISTORY_ID = 0
	inner join MVR_WAIS_PROD.df.DOCUMENTS d on d.DOC_ID = cf.DOC_ID
	inner join MVR_WAIS_PROD.dbo.N_D_SERVICES s on s.HISTORY_ID = 0 and s.SERVICE_ID = si.SERVICE_ID
	inner join MVR_ESP_PROD.[dbo].[PUBLIC_USER_PROFILE_APPLICATIONS] pa on  pa.HISTORY_ID = 0 and pa.APPL_ENTRY_STATUS_ID = 2 and pa.CASE_FILE_URI = d.URI
	WHERE si.HISTORY_ID = 0 and si.WORKFLOW_INSTANCE_ID is not null) si
where si.SERVICE_ID IS NOT NULL

COMMIT

---------------------------------------------------------------------

--Мигрира не вързаните към акаунт услуги, за които има връзка по BACKEND_INCOMING_DOC_GUID
BEGIN TRANSACTION

INSERT INTO [dbo].[service_instances]
SELECT 
	si.SERVICE_INSTANCE_ID,
	si.SERVICE_INSTANCE_VER_ID,
	si.STATUS,
	si.APPLICANT_ID,
	si.SERVICE_INSTANCE_DATE,
	si.SERVICE_ID,
	si.SERVICE_VER_ID,
	si.CASE_FILE_URI,
	N'{"subStatus":"' + si.SUB_STATUS + N'","lastStage":"' + si.LAST_STAGE + N'","lastStageActualCompletionDate":"' + FORMAT(si.STATUS_DATE, 'dd.MM.yyyy') +N'"}',
	1,
	null,
	1,
	ToDateTimeOffset(si.CREATED_ON,'+02:00'),
	1,
	ToDateTimeOffset(si.UPDATED_ON,'+02:00'),
	si.STATUS_DATE
FROM (
	SELECT 
		pa.USER_PROFILE_APPLICATION_ID AS SERVICE_INSTANCE_ID,
		pa.VERSION_ID AS SERVICE_INSTANCE_VER_ID,
		case si.STATUS_ID
			when 3 then 2 -- "Изпълнен" отива в "Изпълнен"  
			else 3 -- Всичко друго отива в "Прекратен"
		end as STATUS,
		pa.USER_PROFILE_ID AS APPLICANT_ID,
		si.DATE_STARTED AS SERVICE_INSTANCE_DATE,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then 351
			else (select service_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		end as SERVICE_ID,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then (select service_ver_id from nom.n_d_services where service_id = 351 and is_last = 1) 
			else (select service_ver_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		END as SERVICE_VER_ID,
		d.URI as CASE_FILE_URI,
		case si.STATUS_ID
			when 1 then 'Termination' -- "В процес на изпълнение" автоматично се прекратява
			when 2 then 'Termination' -- "В изчакване на отговор" автоматично се прекратява
			when 3 then 'Completed'
			when 4 then 'Cancelled'
			when 5 then 'CancelIssuingAdministrativeAct'
			when 6 then 'Termination' -- "Изчакване на заявление с отстранени нередовности" автоматично се прекратява
			when 7 then 'Termination'
			when 8 then 'OutstandingConditions'
			when 9 then 'Termination' -- "Изчаква плащане" автоматично се прекратява
		end AS SUB_STATUS,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 st.NAME from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					inner join MVR_WAIS_PROD.dbo.N_D_STAGES st on st.STAGE_ID = sti.STAGE_ID and st.HISTORY_ID = 0
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else N'Прекратяване на електронна услуга' --Виски неизпълнени се слагат като прекратени
		END AS LAST_STAGE,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 sti.DATE_FINISHED from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else SYSDATETIME() --TODO каква дада да ползваме
		END AS STATUS_DATE,
		pa.CREATED_ON,
		pa.UPDATED_ON 
	FROM MVR_WAIS_PROD.dsi.SERVICE_INSTANCES si
	inner join MVR_WAIS_PROD.df.CASE_FILES cf on cf.CASE_FILE_ID = si.CASE_FILE_ID and cf.HISTORY_ID = 0
	inner join MVR_WAIS_PROD.df.DOCUMENTS d on d.DOC_ID = cf.DOC_ID
	inner join MVR_WAIS_PROD.dbo.N_D_SERVICES s on s.HISTORY_ID = 0 and s.SERVICE_ID = si.SERVICE_ID	
	inner join MVR_WAIS_PROD.iq.INCOMING_DOCUMENTS id on id.DOC_ID = cf.DOC_ID 
	inner join MVR_WAIS_PROD.iq.INCOMING_PACKAGES ipkg on ipkg.INCOMING_PKG_ID = id.INCOMING_PKG_ID
	inner join MVR_ESP_PROD.[dbo].[PUBLIC_USER_PROFILE_APPLICATIONS] pa on  pa.HISTORY_ID = 0 and pa.APPL_ENTRY_STATUS_ID = 1 and pa.BACKEND_INCOMING_DOC_GUID = ipkg.INCOMING_PKG_GUID
	WHERE si.HISTORY_ID = 0 and si.WORKFLOW_INSTANCE_ID is not null) si
where si.SERVICE_ID IS NOT NULL

COMMIT

---------------------------------------------------------------------

declare @max_service_id bigint

set @max_service_id = (select max(service_instance_id) + 1 from [dbo].[service_instances])

DECLARE @sql NVARCHAR(MAX)

SET @sql = '
ALTER SEQUENCE [dbo].[seq_service_instances] 
 RESTART  WITH ' + CONVERT(nvarchar, @max_service_id) 

EXEC(@sql) 

---------------------------------------------------------------------

--Мигрира не вързаните към акаунт услуги, за които няма връзка по BACKEND_INCOMING_DOC_GUID, а се връзват по email на заявителя

BEGIN TRANSACTION

INSERT INTO [dbo].[service_instances]
SELECT 
	NEXT VALUE FOR [dbo].[seq_service_instances],
	si.SERVICE_INSTANCE_VER_ID,
	si.STATUS,
	si.APPLICANT_ID,
	si.SERVICE_INSTANCE_DATE,
	si.SERVICE_ID,
	si.SERVICE_VER_ID,
	si.CASE_FILE_URI,
	N'{"subStatus":"' + si.SUB_STATUS + N'","lastStage":"' + si.LAST_STAGE + N'","lastStageActualCompletionDate":"' + FORMAT(si.STATUS_DATE, 'dd.MM.yyyy') +N'"}',
	1,
	null,
	1,
	ToDateTimeOffset(si.CREATED_ON,'+02:00'),
	1,
	ToDateTimeOffset(si.UPDATED_ON,'+02:00'),
	si.STATUS_DATE
FROM (
	SELECT 		
		SI.VERSION_ID AS SERVICE_INSTANCE_VER_ID,
		case si.STATUS_ID
			when 3 then 2 -- "Изпълнен" отива в "Изпълнен"  
			else 3 -- Всичко друго отива в "Прекратен"
		end as STATUS,
		u.user_id AS APPLICANT_ID,
		si.DATE_STARTED AS SERVICE_INSTANCE_DATE,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then 351
			else (select service_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		end as SERVICE_ID,
		case 
			when s.SUNAU_SERVICE_URI = N'ТОДО Аналитици' then (select service_ver_id from nom.n_d_services where service_id = 351 and is_last = 1) 
			else (select service_ver_id from nom.n_d_services where sunau_service_uri = s.SUNAU_SERVICE_URI and is_last = 1) 
		END as SERVICE_VER_ID,
		d.URI as CASE_FILE_URI,
		case si.STATUS_ID
			when 1 then 'Termination' -- "В процес на изпълнение" автоматично се прекратява
			when 2 then 'Termination' -- "В изчакване на отговор" автоматично се прекратява
			when 3 then 'Completed'
			when 4 then 'Cancelled'
			when 5 then 'CancelIssuingAdministrativeAct'
			when 6 then 'Termination' -- "Изчакване на заявление с отстранени нередовности" автоматично се прекратява
			when 7 then 'Termination'
			when 8 then 'OutstandingConditions'
			when 9 then 'Termination' -- "Изчаква плащане" автоматично се прекратява
		end AS SUB_STATUS,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 st.NAME from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					inner join MVR_WAIS_PROD.dbo.N_D_STAGES st on st.STAGE_ID = sti.STAGE_ID and st.HISTORY_ID = 0
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else N'Прекратяване на електронна услуга' --Виски неизпълнени се слагат като прекратени
		END AS LAST_STAGE,
		case 
			when si.STATUS_ID in (3,4,5,7,8) then
				(select top 1 sti.DATE_FINISHED from MVR_WAIS_PROD.dsi.STAGE_INSTANCES sti
					where sti.HISTORY_ID = 0 and sti.SERVICE_INST_ID = si.SERVICE_INST_ID and sti.STATUS_ID = 2 --изпълнене
					order by sti.ORDER_NUMBER desc)
			else SYSDATETIME() --TODO каква дада да ползваме
		END AS STATUS_DATE,
		si.CREATED_ON,
		si.UPDATED_ON 
	FROM MVR_WAIS_PROD.dsi.SERVICE_INSTANCES si
	inner join MVR_WAIS_PROD.df.CASE_FILES cf on cf.CASE_FILE_ID = si.CASE_FILE_ID and cf.HISTORY_ID = 0
	inner join MVR_WAIS_PROD.df.DOCUMENTS d on d.DOC_ID = cf.DOC_ID
	inner join MVR_WAIS_PROD.dbo.N_D_SERVICES s on s.HISTORY_ID = 0 and s.SERVICE_ID = si.SERVICE_ID
	inner join users.users u on u.email = si.SERVICE_APPLICANT_EMAIL
	WHERE si.HISTORY_ID = 0 and si.WORKFLOW_INSTANCE_ID is not null and d.URI not in (select case_file_uri from service_instances)) si
where si.SERVICE_ID IS NOT NULL

COMMIT