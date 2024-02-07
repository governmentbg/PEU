SELECT * FROM [dbo].[app_parameters] where code like N'GL_EAU_PUBLIC_API'

update [dbo].[app_parameters] set code = N'GL_EAU_PUBLIC_APP' where code like N'GL_EAU_PUBLIC_API'

SELECT * FROM [dbo].[app_parameters] where code like N'GL_EAU_PUBLIC_APP'