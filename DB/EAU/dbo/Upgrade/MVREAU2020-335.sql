select * from app_parameters where code like N'SERVICE_LIMIT_DISABLED'

update app_parameters 
set description = N'Параметърът спира използването на услугата за лимитиране при стойност > 0. При стойност 0 се използва услугата за лимитиране.',
value_int = 0
where code like N'SERVICE_LIMIT_DISABLED'

select * from app_parameters where code like N'SERVICE_LIMIT_DISABLED'


--Най-добре да се изтрият всички [data_service_limits] и да се вземат наново от Create scripts, защото там съм оправил поредния номер да няма дупки.

--Алтернативен вариант:
  DELETE FROM [dbo].[data_service_limits]
WHERE service_code like N'MAX_COUNT_REQUEST_SERVICE_POWER'
OR service_code like N'PEAU_APPLICATION_PREVIEW_LIMIT'
OR service_code like N'PEAU_DOWNLOAD_DOCUMENT_LIMIT'
OR service_code like N'PEAU_SEARCH_LIMIT'

INSERT [dbo].[data_service_limits] ([service_limit_id], [service_limit_ver_id], [service_code], [service_name], [requests_interval], [requests_number], [status], [is_last], [deactivation_ver_id], [created_by], [created_on], [updated_by], [updated_on]) VALUES (9, 118323, N'NAIF_NRBLD_LIMIT', N'Брой справки към НАИФ НРБЛД', CAST(N'1900-01-05T11:12:22.000' AS DateTime), 2, 1, 1, NULL, 2, CAST(N'2020-12-04T17:23:56.6830000+00:00' AS DateTimeOffset), 2, CAST(N'2020-12-04T17:23:56.6830000+00:00' AS DateTimeOffset))
GO

--след изтриването и добавянето може да се разместят primary key-овете за да няма дупки 