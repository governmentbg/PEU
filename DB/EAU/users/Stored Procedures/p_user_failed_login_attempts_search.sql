
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_failed_login_attempts_search]
	@p_attempt_ids			NVARCHAR(MAX),
	@p_login_name			NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( (@p_login_name IS NULL AND @p_attempt_ids IS NULL) OR 
		 (@p_login_name IS NOT NULL AND @p_attempt_ids IS NOT NULL)
	   ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_failed_login_attempts_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
						
		DECLARE @v_ids_table		TT_INTEGERS;

		INSERT INTO 
			@v_ids_table
		SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_attempt_ids, ',');

		IF @p_attempt_ids IS NOT NULL
		BEGIN
			SELECT * FROM [users].[user_failed_login_attempts]
			WHERE [attempt_id] IN (SELECT * FROM @v_ids_table);
		END
		ELSE 
		BEGIN
			SELECT * FROM [users].[user_failed_login_attempts]
			WHERE 
				[login_name] = @p_login_name 
			AND [is_active] = 1;
		END;		
				
		-- ===============================================================================			
	END TRY
	   BEGIN CATCH

		-- ===============================================================================
		-- STANDARD ERROR HANDLING MODULE;
		
		-- Raise an error with the details of the exception
        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)
        
		-- RETURN -ERROR_NUMBER();
		-- ===============================================================================
		
	  END CATCH;
	END;
END
