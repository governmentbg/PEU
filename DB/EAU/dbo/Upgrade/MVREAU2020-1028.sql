update [dbo].[data_service_limits] 
set [service_name] = N'Лимитът не се прилага самостоятелно. Прилага се заедно с всички останали лимити. Най-малко 6 заявки за 1 секунда.'
where [service_code] like N'BASE_DATA_SERVICE_LIMIT'