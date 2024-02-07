
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_permissions_search]
	@p_users_ids			NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_users_ids IS NULL OR rtrim(ltrim(@p_users_ids)) = '' ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_permissions_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		TT_INTEGERS;

		INSERT INTO 
			@v_ids_table
		SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_users_ids, ',');

		SELECT [user_id], [permission_id]
		FROM [users].[user_permissions] up
		WHERE 
			up.user_id in (SELECT * FROM @v_ids_table)
		AND up.is_active = 1
		ORDER BY up.user_id, up.permission_id;

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
