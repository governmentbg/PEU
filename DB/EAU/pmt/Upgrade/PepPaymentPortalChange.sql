update [dbo].[app_parameters]
set value_string = N'ais_stru_91e4eb40-5871-4e58-8ca2-330948b845a3'
where code like  N'GL_AND_PEP_CIN' and is_last = 1

update [pmt].[n_d_registration_data] set
cin = N'ais_stru_91e4eb40-5871-4e58-8ca2-330948b845a3',
secret_word = N'9EFACF35E807468C95596693006A5AEDE1E88E5696CD458885AE45F809B524032383ADCD634A45968E04089C7032FEF7D29F9EC0E21D491DAE0CF89502429ED8',
portal_url = null,
service_url = N'https://pay-test.egov.bg:44310/'
where type = 2