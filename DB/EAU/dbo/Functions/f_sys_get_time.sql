


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[f_sys_get_time] 
(
	-- Add the parameters for the function here
)
RETURNS dateTimeoffset(3)
AS
BEGIN

	DECLARE @v_Date				DATETIMEOFFSET(3);
	DECLARE @v_CtxInfo          VARBINARY(128);
	DECLARE @vTmpDateBinary		BINARY(8);

	SET @v_CtxInfo = CONTEXT_INFO();

	SET @vTmpDateBinary = SUBSTRING(@v_CtxInfo, 9 ,8);

	IF CAST(@vTmpDateBinary AS BIGINT) = 0 OR @vTmpDateBinary IS NULL
		SET @v_Date = SYSDATETIMEOFFSET();
	ELSE 
		SET @v_Date = CAST(@vTmpDateBinary AS DATETIMEOFFSET(3));
	
	RETURN @v_Date;
END
