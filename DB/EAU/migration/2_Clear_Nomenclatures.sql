BEGIN TRANSACTION

--[nom].[n_d_languages]
UPDATE [nom].[n_d_languages]
SET updated_by = 1,
	created_by = 1

--[nom].n_d_labels
DELETE [nom].[n_d_labels_i18n]

DELETE [nom].n_d_labels
WHERE is_last <> 1

UPDATE [nom].[n_d_labels]
SET updated_by = 1,
	created_by = 1

--[cms].[pages_i18n]
DELETE [cms].[pages_i18n]

update cms.pages
set updated_by = 1,
	created_by = 1


--n_d_declarations

DELETE [nom].[n_d_service_declarations]
where is_last <> 1 or deactivation_ver_id is not null

UPDATE [nom].[n_d_service_declarations]
SET updated_by = 1,
	created_by = 1,
	declaration_ver_id = (select d.declaration_ver_id from [nom].[n_d_declarations] d where d.delcaration_id = [nom].[n_d_service_declarations].delcaration_id and is_last = 1),
	service_ver_id = (select s.service_ver_id from nom.n_d_services s where s.service_id = [nom].[n_d_service_declarations].service_id and is_last = 1)
	
UPDATE [nom].[n_d_declarations]
SET updated_by = 1,
	created_by = 1

DELETE [nom].[n_d_declarations]
WHERE is_last <> 1 or deactivation_ver_id is not null 

--[nom].[n_d_document_templates]

DELETE [nom].[n_d_document_templates]
WHERE is_last <> 1 or deactivation_ver_id is not null 

UPDATE [nom].[n_d_document_templates]
set updated_by = 1,
	created_by = 1

DELETE [nom].[n_d_service_document_types]
where is_last <> 1 or deactivation_ver_id is not null

UPDATE [nom].[n_d_service_document_types]
SET updated_by = 1,
	created_by = 1,	
	service_ver_id = (select s.service_ver_id from nom.n_d_services s where s.service_id = [nom].[n_d_service_document_types].service_id and is_last = 1)
	
--[nom].[n_d_service_delivery_channels]
DELETE [nom].[n_d_service_delivery_channels]
where is_last <> 1 or deactivation_ver_id is not null

UPDATE [nom].[n_d_service_delivery_channels]
SET updated_by = 1,
	created_by = 1,	
	service_ver_id = (select s.service_ver_id from nom.n_d_services s where s.service_id = [nom].[n_d_service_delivery_channels].service_id and is_last = 1)
	

--n_d_service_terms
DELETE nom.n_d_service_terms
where is_last <> 1 or deactivation_ver_id is not null

UPDATE nom.n_d_service_terms
SET updated_by = 1,
	created_by = 1,	
	service_ver_id = (select s.service_ver_id from nom.n_d_services s where s.service_id = nom.n_d_service_terms.service_id and is_last = 1)
	

--[nom].[n_d_services]
delete [nom].[n_d_services_i18n]

delete [nom].[n_d_services]
WHERE is_last <> 1 or deactivation_ver_id is not null 

delete [nom].[n_d_services]
WHERE is_last <> 1 or deactivation_ver_id is not null 

delete [nom].[n_d_services]
  WHERE is_active = 0

UPDATE [nom].[n_d_services]
SET updated_by = 1,
	created_by = 1,	
	group_ver_id = (select sg.group_ver_id from nom.n_d_service_groups sg where sg.group_id = [nom].[n_d_services].group_id and is_last = 1)


--[nom].[n_d_service_groups]
delete [nom].[n_d_service_groups_i18n]

delete [nom].[n_d_service_groups]
WHERE is_last <> 1 or deactivation_ver_id is not null 

delete [nom].[n_d_service_groups]
WHERE is_active = 0

update [nom].[n_d_service_groups]
SET updated_by = 1,
	created_by = 1

update [pmt].[n_d_registration_data]
SET updated_by = 1,
	created_by = 1
	
delete users.users
where user_id not in (select user_id from [users].[user_authentications] where authentication_type = 2) 
	and user_id  not in (1,2)


COMMIT
