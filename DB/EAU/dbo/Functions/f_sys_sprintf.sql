﻿
CREATE FUNCTION [dbo].[f_sys_sprintf] 
(		@S NVARCHAR(MAX),  
        @PARAMS NVARCHAR(MAX), 
        @SEPARATOR CHAR(1) = ','
) 
RETURNS NVARCHAR(MAX) 
AS 
BEGIN 
	DECLARE @P NVARCHAR(MAX) 
	DECLARE @PARAMLEN INT 
	DECLARE @v_HasWork BIT;
	
	SET @v_HasWork = 1;
	
	SET @PARAMS = @PARAMS + @SEPARATOR 
	SET @PARAMLEN = LEN(@PARAMS) 
	WHILE @v_HasWork = 1
	BEGIN 
	
		DECLARE @v_Tmp	INT;
		SET @v_Tmp = CHARINDEX('%S', @S);
		
		IF @v_Tmp = 0 OR @v_Tmp IS NULL
		BEGIN
			SET @v_HasWork = 0;
			BREAK;
		END;
		
		SET @P = LEFT(@PARAMS+@SEPARATOR, CHARINDEX(@SEPARATOR, @PARAMS)-1) 
		
		SET @S = STUFF(@S, @v_Tmp, 2, @P) 
		SET @PARAMS = SUBSTRING(@PARAMS, LEN(@P)+2, @PARAMLEN) 
	END 
	RETURN @S 
END 


