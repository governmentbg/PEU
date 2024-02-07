SELECT * FROM [dbo].[app_parameters] where code like N'GL_SERVICE_INSTANCE_PAYMENT_REASON'

update [dbo].[app_parameters] 
set description = N'Основание за плащане на задължение по услуга. Максимално 70 символа заедно с двете УРИ-та.',
value_string = N'Плащане по док. {0} за усл. {1}'
where code like N'GL_SERVICE_INSTANCE_PAYMENT_REASON'


SELECT * FROM [dbo].[app_parameters] where code like N'GL_SERVICE_INSTANCE_PAYMENT_REASON'