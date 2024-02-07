


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[f_get_n_d_services_ver_id] 
(
	@ID INT
)
RETURNS BIGINT
AS
BEGIN

	DECLARE @ver_id BIGINT;

	SELECT @ver_id = MAX([service_ver_id])
	  FROM [nom].[n_d_services] 
	 WHERE [service_id] = @ID;
	
	RETURN @ver_id;
END