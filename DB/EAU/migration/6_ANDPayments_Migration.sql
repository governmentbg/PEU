BEGIN TRANSACTION

INSERT INTO [pmt].[obligations]
select
	P.PAYMENT_ID,
	2,--Платено
	P.AMOUNT,
	P.AMOUNT,
	null,
	P.BIC,
	P.IBAN,
	MVR_ESP_PROD.[pmt].[f_Get_Epotal_Service_Payment_Descritption](SI.[DOCUMENT_TYPE], SI.[DOCUMENT_SERIES], SI.[DOCUMENT_NUMBER]),
	null,
	DATEADD(day, 1, ANNOUNCEMENT_DATE), --Срока за плащанията към АНД е 1 ден
	P.USER_PROFILE_ID,
	P.OBLIGED_PERSON_NAME,
	CASE
		WHEN OBLIGED_PERSON_EGN is not null then OBLIGED_PERSON_EGN --ЕГН
		when OBLIGED_PERSON_LNCH is not null then OBLIGED_PERSON_LNCH --ЛНЧ
		else OBLIGED_PERSON_BULSTAT --ЕИК
	end AS OBLIGED_PERSON_IDENT,
	CASE
		WHEN OBLIGED_PERSON_EGN is not null then 1
		when OBLIGED_PERSON_LNCH is not null then 2
		else 3
	end  AS OBLIGED_PERSON_IDENT_TYPE,
	ANNOUNCEMENT_DATE,
	CASE
		WHEN SI.DOCUMENT_SERIES  IS NOT NULL THEN N'' + CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'|' + SI.DOCUMENT_SERIES + N'|' + SI.DOCUMENT_NUMBER + N'|' + CAST(P.AMOUNT as nvarchar)
		ELSE N'' + CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'|' + SI.DOCUMENT_NUMBER + N'|' + CAST(P.AMOUNT as nvarchar)
	END,	
	2, --АНД Плащане
	null as service_instance_id,
	null as service_instance_ver_id,
	(SELECT service_id FROM nom.n_d_services where initiation_type_id = 2) as service_id, --Единствената услуга стратираща се без заявление
	(SELECT service_ver_id FROM nom.n_d_services where initiation_type_id = 2) as service_ver_id, --Единствената услуга стратираща се без заявление
	CASE
		WHEN SI.DOCUMENT_SERIES  IS NOT NULL THEN '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentSeries":"' + SI.DOCUMENT_SERIES + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
		ELSE '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
	END,	
	1,
	ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	1,
	ToDateTimeOffset(P.UPDATED_ON, '+02:00')
from MVR_ESP_PROD.[pmt].[PAYMENTS] P
inner join MVR_ESP_PROD.dbo.EPORTAL_SERVICE_INSTANCES SI on P.EPORTAL_SERVICE_INSTANCES_ID = SI.SERVICE_INSTANCE_ID
where P.PAYMENT_ID in (
	select 
		MIN(PAYMENT_ID)
	from MVR_ESP_PROD.[pmt].[PAYMENTS] P
	inner join MVR_ESP_PROD.dbo.EPORTAL_SERVICE_INSTANCES SI on P.EPORTAL_SERVICE_INSTANCES_ID = SI.SERVICE_INSTANCE_ID
	where PAYMENT_STATUS = 'Paid'
	group by USER_PROFILE_ID, SI.DOCUMENT_TYPE, SI.DOCUMENT_NUMBER, SI.DOCUMENT_SERIES)

COMMIT

--Ако няма повтарящи се, към монета няма

BEGIN TRANSACTION

INSERT INTO [pmt].[payment_requests]
select 
	 P.PAYMENT_ID,
	 2, --EPay профил
	 1, --EPay
	 3, --Платена
	 P.PAYMENT_ID,
	 P.OBLIGED_PERSON_NAME,
	 CASE
		WHEN OBLIGED_PERSON_EGN is not null then OBLIGED_PERSON_EGN
		when OBLIGED_PERSON_LNCH is not null then OBLIGED_PERSON_LNCH
		else OBLIGED_PERSON_BULSTAT
	 end AS OBLIGED_PERSON_IDENT,
	 CASE
		WHEN OBLIGED_PERSON_EGN is not null then 1 --ЕГН
		when OBLIGED_PERSON_LNCH is not null then 2 --ЛНЧ
		else 3 --ЕИК
	 end  AS OBLIGED_PERSON_IDENT_TYPE,
	 P.ANNOUNCEMENT_DATE,
	 P.ACHIEVEMENT_DATE,
	 null,
	 P.AMOUNT,
	 CASE
		WHEN SI.DOCUMENT_SERIES  IS NOT NULL THEN '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentSeries":"' + SI.DOCUMENT_SERIES + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
		ELSE '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
	 END,
	 1,
	 ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	 1,
	 ToDateTimeOffset(P.UPDATED_ON, '+02:00')
from MVR_ESP_PROD.[pmt].[PAYMENTS] P
	inner join MVR_ESP_PROD.dbo.EPORTAL_SERVICE_INSTANCES SI on P.EPORTAL_SERVICE_INSTANCES_ID = SI.SERVICE_INSTANCE_ID
where P.PAYMENT_STATUS = 'Paid' 

COMMIT

declare @max_obligation_id bigint
declare @max_payment_request_id bigint

set @max_obligation_id = (select max(obligation_id) + 1 from pmt.obligations)

DECLARE @sql NVARCHAR(MAX)

SET @sql = '
ALTER SEQUENCE [pmt].[seq_obligations] 
 RESTART  WITH ' + CONVERT(nvarchar, @max_obligation_id) 

EXEC(@sql) 


set @max_payment_request_id = (select max(payment_request_id) + 1 from pmt.payment_requests)

 SET @sql = '
ALTER SEQUENCE [pmt].[seq_payment_requests] 
 RESTART  WITH ' + CONVERT(nvarchar, @max_payment_request_id) 

EXEC(@sql) 

--Ако има повтарящи се

--BEGIN TRANSACTION

--INSERT INTO [pmt].[payment_requests]
--select 
--	P.PAYMENT_ID,
--	2, --EPay профил
--	1, --EPay
--	3, --Платена
--	(select 
--		obl.obligation_id 
--	 from pmt.obligations obl	 
--	 where ((SI.DOCUMENT_SERIES  IS NOT NULL AND obl.obligation_identifier =  N'' + CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'|' + SI.DOCUMENT_SERIES + N'|' + SI.DOCUMENT_NUMBER + N'|' + CAST(P.AMOUNT as nvarchar)) OR
--			(SI.DOCUMENT_SERIES  IS NULL AND obl.obligation_identifier =  N'' + CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'|' + SI.DOCUMENT_NUMBER + N'|' + CAST(P.AMOUNT as nvarchar)))
--	       and ((P.USER_PROFILE_ID is null and obl.applicant_id is null) or p.USER_PROFILE_ID = obl.applicant_id)),
--	 P.OBLIGED_PERSON_NAME,
--	 CASE
--		WHEN OBLIGED_PERSON_EGN is not null then OBLIGED_PERSON_EGN
--		when OBLIGED_PERSON_LNCH is not null then OBLIGED_PERSON_LNCH
--		else OBLIGED_PERSON_BULSTAT
--	 end AS OBLIGED_PERSON_IDENT,
--	 CASE
--		WHEN OBLIGED_PERSON_EGN is not null then 1 --ЕГН
--		when OBLIGED_PERSON_LNCH is not null then 2 --ЛНЧ
--		else 3 --ЕИК
--	 end  AS OBLIGED_PERSON_IDENT_TYPE,
--	 P.ANNOUNCEMENT_DATE,
--	 P.ACHIEVEMENT_DATE,
--	 null,
--	 P.AMOUNT,
--	 CASE
--		WHEN SI.DOCUMENT_SERIES  IS NOT NULL THEN '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentSeries":"' + SI.DOCUMENT_SERIES + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
--		ELSE '{"documentType":"'+ CASE SI.DOCUMENT_TYPE WHEN 1 THEN 'TICKET' WHEN 2 THEN 'ACT' ELSE 'PENAL_DECREE' END + N'","documentNumber":"' + SI.DOCUMENT_NUMBER + N'","amount":"' + CAST(P.AMOUNT as nvarchar) + N'"}'
--	 END,
--	 1,
--	 ToDateTimeOffset(P.CREATED_ON, '+02:00'),
--	 1,
--	 ToDateTimeOffset(P.UPDATED_ON, '+02:00')
--from MVR_ESP_PROD.[pmt].[PAYMENTS] P
--	inner join MVR_ESP_PROD.dbo.EPORTAL_SERVICE_INSTANCES SI on P.EPORTAL_SERVICE_INSTANCES_ID = SI.SERVICE_INSTANCE_ID
--where P.PAYMENT_STATUS = 'Paid' 

--COMMIT
