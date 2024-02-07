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
	MVR_ESP_PROD.[pmt].[f_Get_Service_Payment_Descritption](P.CASE_FILE_URI),
	null,
	DATEADD(day, 3, ANNOUNCEMENT_DATE),--Срока за плащане на услугите е 3 дни
	P.USER_PROFILE_ID,
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
	ANNOUNCEMENT_DATE,
	N'' + P.CASE_FILE_URI + N'|' + CAST((SELECT service_instance_id FROM service_instances where case_file_uri = P.CASE_FILE_URI) as nvarchar),
	1, --Плащане по Инстанция на услуга.
	(SELECT service_instance_id FROM service_instances where case_file_uri = P.CASE_FILE_URI) as service_instance_id,
	(SELECT service_instance_ver_id FROM service_instances where case_file_uri = P.CASE_FILE_URI) as service_instance_ver_id,
	(SELECT service_id FROM service_instances where case_file_uri = P.CASE_FILE_URI) as service_id,
	(SELECT service_ver_id FROM service_instances where case_file_uri = P.CASE_FILE_URI) as service_ver_id,
	null,
	1,
	ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	1,
	ToDateTimeOffset(P.UPDATED_ON, '+02:00')
from MVR_ESP_PROD.[pmt].[PAYMENTS] P
where P.PAYMENT_ID in (
	select 
		MIN(PAYMENT_ID)
	from MVR_ESP_PROD.[pmt].[PAYMENTS] 
	where PAYMENT_STATUS = 'Paid' and CASE_FILE_URI in (select case_file_uri from service_instances)
	group by CASE_FILE_URI, USER_PROFILE_ID)


INSERT INTO [pmt].[payment_requests]
select 
	P.PAYMENT_ID,
	2, --EPay профил
	1, --EPay
	3, --Платена
	(select 
		obl.obligation_id 
	 from pmt.obligations obl
	 inner join service_instances si on obl.service_instance_id = si.service_instance_id
	 where si.case_file_uri = P.CASE_FILE_URI and ((P.USER_PROFILE_ID is null and obl.applicant_id is null) or p.USER_PROFILE_ID = obl.applicant_id)),
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
	 null,
	 1,
	 ToDateTimeOffset(P.CREATED_ON, '+02:00'),
	 1,
	 ToDateTimeOffset(P.UPDATED_ON, '+02:00')
from MVR_ESP_PROD.[pmt].[PAYMENTS] P
where P.PAYMENT_STATUS = 'Paid' and P.CASE_FILE_URI in (select case_file_uri from service_instances)

COMMIT
