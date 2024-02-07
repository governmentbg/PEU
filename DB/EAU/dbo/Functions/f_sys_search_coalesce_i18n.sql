



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[f_sys_search_coalesce_i18n]  
(
	@p_str_1 NVARCHAR(MAX),
	@p_str_2 NVARCHAR(MAX),
	@p_str_3 NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @v_str NVARCHAR(MAX);

	IF @p_str_1 IS NOT NULL AND @p_str_1 != ''  
		SET @v_str = @p_str_1;
	ELSE IF @p_str_2 IS NOT NULL AND @p_str_2 != '' 
		SET @v_str = @p_str_2 ;   
	ELSE 
		SET @v_str = @p_str_3;

	RETURN @v_str;

END