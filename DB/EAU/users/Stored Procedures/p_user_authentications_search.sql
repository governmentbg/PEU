

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_authentications_search]
	@p_authentication_ids	NVARCHAR(200),
	@p_user_id				INT,
	@p_authentication_type	TINYINT,
	@p_username				NVARCHAR(100),
	@p_certificate_hash		NVARCHAR(200)	
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_authentication_ids IS NULL AND @p_user_id  IS NULL AND @p_authentication_type IS NULL AND @p_username IS NULL AND @p_certificate_hash IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_authentications_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		tt_integers;

		IF @p_authentication_ids IS NOT NULL AND rtrim(ltrim(@p_authentication_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_authentication_ids, ',');
		END

		SET @v_Params = N'
				@v_ids_table			TT_INTEGERS READONLY,
				@p_user_id				INT,
				@p_authentication_type	TINYINT,
				@p_username				NVARCHAR(100),
				@p_certificate_hash		NVARCHAR(200)';


		SET @v_statement = N'SELECT t.*
              FROM users.user_authentications t
              WHERE t.is_active = 1 ';

		IF @p_authentication_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.authentication_id in (SELECT * FROM @v_ids_table)
		';
		END
  
		IF @p_user_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.user_id = @p_user_id 
		';
		END
  
		IF @p_authentication_type IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.authentication_type = @p_authentication_type 
		';
		END
  
		IF @p_username IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND lower(t.username) = lower(@p_username)
		';
		END
  
		IF @p_certificate_hash IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.certificate_id = ( SELECT c.certificate_id FROM users.certificates c WHERE c.thumbprint = @p_certificate_hash )
		';
		END
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_ids_table		 = @v_ids_table,
			@p_user_id			 = @p_user_id,
			@p_username			 = @p_username,
			@p_authentication_type = @p_authentication_type,
			@p_certificate_hash  = @p_certificate_hash;
				
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
