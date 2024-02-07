


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_users_search]	
	@p_users_ids		NVARCHAR(MAX),
	@p_cin				INT,
	@p_email			NVARCHAR(1000),
	@p_username			NVARCHAR(1000),
	@p_statuses			NVARCHAR(100),
	@p_date_from		DATETIME,
	@p_date_to			DATETIME,
	@p_authentication_type SMALLINT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_max_nor			INT,
	@p_calculate_count	BIT,
	@p_count			INT OUTPUT		
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_start_index IS NULL AND @p_page_size  IS NULL AND @p_users_ids IS NULL AND @p_email IS NULL AND @p_username IS NULL AND @p_authentication_type IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_users_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_userids_table	TT_INTEGERS;

		IF @p_users_ids IS NOT NULL AND rtrim(ltrim(@p_users_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_userids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_users_ids, ',');

			IF @p_calculate_count = 1
			BEGIN				
				SELECT @p_count = COUNT(1) FROM users.users 
				WHERE 
					user_id IN (SELECT VALUE FROM @v_userids_table)
				AND is_system = 0;
			END

			SELECT 
				 u.user_id,
				 u.cin,
				 u.email, 
				 u.status, 
				 u.created_by, 
				 u.created_on, 
				 u.updated_by, 
				 u.updated_on,
				 (SELECT ua.username 
					FROM users.user_authentications ua
				   WHERE ua.user_id = u.user_id
					 AND ua.authentication_type = 2) AS username
			FROM users.users u
			WHERE 
				user_id IN (SELECT VALUE FROM @v_userids_table)
			AND is_system = 0
			ORDER BY user_id ASC
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY;
		END
		ELSE
		BEGIN

			SET @v_Params = N'
				@p_count			INT OUTPUT,
				@p_cin				INT,
				@p_email			NVARCHAR(1000),
				@p_username			NVARCHAR(1000),
				@p_statuses			NVARCHAR(100),
				@p_date_from		DATETIME,
				@p_date_to			DATETIME,
				@p_authentication_type SMALLINT,
				@p_start_index		INT,
				@p_page_size		INT';


		SET @v_statement = N'SELECT 
					 u.user_id,
					 u.cin,
					 u.email, 
					 u.status, 
					 u.created_by, 
					 u.created_on, 
					 u.updated_by, 
					 u.updated_on,
					 (SELECT ua.username 
						FROM users.user_authentications ua
					   WHERE ua.user_id = u.user_id
						 AND ua.authentication_type = 2) AS username
				FROM users.users u
				WHERE 
					u.is_system = 0 ';
		
		IF @p_cin IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND u.cin = @p_cin ';
		END
		
		IF @p_email IS NOT NULL AND rtrim(ltrim(@p_email)) <> ''
		BEGIN
			SET @v_statement = @v_statement + N' AND lower(u.email) = @p_email ';
		END

		IF @p_username IS NOT NULL AND rtrim(ltrim(@p_username)) <> ''
		BEGIN
			SET @v_statement = @v_statement + N' AND EXISTS (SELECT 1 FROM users.user_authentications ua WHERE ua.user_id = u.user_id AND lower(ua.username) LIKE ''%' + @p_username + '%'' AND ua.authentication_type = 2) ';
		END

		IF @p_statuses IS NOT NULL AND rtrim(ltrim(@p_statuses)) <> ''
		BEGIN
			SET @v_statement = @v_statement + N' AND u.status IN (SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_statuses, '','')) ';
		END
		
		IF @p_date_from IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND u.updated_on >= @p_date_from';
		END

		IF @p_date_to IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND u.updated_on <= @p_date_to';
		END

		IF @p_authentication_type IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND EXISTS (SELECT 1 FROM users.user_authentications ua WHERE ua.user_id = u.user_id AND ua.authentication_type = @p_authentication_type AND ua.IS_ACTIVE = 1) ';
		END

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,
				@p_cin				 = @p_cin,
				@p_email			 = @p_email,
				@p_username			 = @p_username,
				@p_statuses			 = @p_statuses,
				@p_date_from		 = @p_date_from,
				@p_date_to			 = @p_date_to,
				@p_authentication_type = @p_authentication_type,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END
		
		SET @v_statement = @v_statement + N' ORDER BY u.updated_on DESC, u.user_id
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,	
			@p_cin				 = @p_cin,
			@p_email			 = @p_email,
			@p_username			 = @p_username,
			@p_statuses			 = @p_statuses,
			@p_date_from		 = @p_date_from,
			@p_date_to			 = @p_date_to,
			@p_authentication_type = @p_authentication_type,
			@p_start_index		 = @p_start_index,
			@p_page_size		 = @p_page_size,
			@p_count			 = @p_count output;

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
GO
