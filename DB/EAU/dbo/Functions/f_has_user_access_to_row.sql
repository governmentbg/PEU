


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[f_has_user_access_to_row] 
(
	@p_user_id		INT
)
RETURNS BIT
AS
BEGIN

	DECLARE @v_curr_user_id INT, @v_has_access BIT;
		
	SET @v_curr_user_id = CAST(CAST(CONTEXT_INFO() AS VARBINARY(4)) AS INTEGER);

	IF @p_user_id = @v_curr_user_id
	OR 1 = @v_curr_user_id
    BEGIN
        SET @v_has_access = CAST (1 AS BIT);
	END
    ELSE
	BEGIN
        SET @v_has_access = CAST (0 AS BIT);
    END;
	
	RETURN @v_has_access;
END