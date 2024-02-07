

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [audit].[p_log_actions_search]
	@p_log_action_ids			NVARCHAR(MAX),
	@p_log_action_date_from		DATETIME,
	@p_log_action_date_to		DATETIME,
	@p_object_type_id			TINYINT,
	@p_action_type_id			TINYINT,
	@p_functionality_id			TINYINT,
	@p_key						NVARCHAR(100),
	@p_user_id					INT,
	@p_ip_address				VARBINARY(16),
	@p_start_index				INT,
	@p_page_size				INT,
	@p_calculate_count			BIT,
	@p_count					INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_log_actions_search]';
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

		IF @p_log_action_ids IS NOT NULL AND rtrim(ltrim(@p_log_action_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_log_action_ids, ',');
		END

		SET @v_Params = N'
				@v_ids_table			TT_INTEGERS READONLY,
				@p_log_action_date_from	DATETIME,
				@p_log_action_date_to	DATETIME,
				@p_object_type_id		TINYINT,
				@p_action_type_id		TINYINT,
				@p_functionality_id		TINYINT,
				@p_key					NVARCHAR(100),
				@p_user_id				INT,
				@p_ip_address			VARBINARY(16),
				@p_start_index			INT,
				@p_page_size			INT,
				@p_count				INT OUTPUT';

		SET @v_statement = N'SELECT 
					t.[log_action_id],
                    t.[log_action_date],
                    t.[object_type_id],
                    t.[action_type_id],
                    t.[functionality_id],
                    t.[key],
                    t.[login_session_id],
                    t.[user_id],
                    t.[ip_address],
                    t.[additional_data],
                    u.[email],
                    (SELECT ua.username 
                       FROM users.user_authentications ua
                      WHERE ua.user_id = u.user_id
                        AND ua.authentication_type = 2) username
					FROM audit.log_actions t, users.users u 
                    WHERE t.user_id = u.user_id';

		IF @p_log_action_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.log_action_id in (SELECT * FROM @v_ids_table)
		';
		END
  
		IF @p_log_action_date_from IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.log_action_date >= @p_log_action_date_from
		';
		END

		IF @p_log_action_date_to IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.log_action_date <= @p_log_action_date_to
		';
		END
  
		IF @p_object_type_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.object_type_id = @p_object_type_id 
		';
		END;

		IF @p_action_type_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.action_type_id = @p_action_type_id
		';
		END;

		IF @p_functionality_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.functionality_id = @p_functionality_id
		';
		END;

		IF @p_key IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.[key] = @p_key
		';
		END;

		IF @p_user_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.user_id = @p_user_id
		';
		END;

		IF @p_ip_address IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.ip_address = @p_ip_address
		';
		END;

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params					= @v_Params,				
				@v_ids_table			= @v_ids_table,	
				@p_log_action_date_from	= @p_log_action_date_from,
				@p_log_action_date_to	= @p_log_action_date_to,	
				@p_object_type_id		= @p_object_type_id,		
				@p_action_type_id		= @p_action_type_id,	
				@p_functionality_id		= @p_functionality_id,	
				@p_key					= @p_key,			
				@p_user_id				= @p_user_id,			
				@p_ip_address			= @p_ip_address,		
				@p_start_index			= @p_start_index,
				@p_page_size			= @p_page_size,
				@p_count				= @p_count output;
		END

		SET @v_statement = @v_statement + N' ORDER BY t.log_action_date desc
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';		
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_ids_table			= @v_ids_table,	
			@p_log_action_date_from	= @p_log_action_date_from,
			@p_log_action_date_to	= @p_log_action_date_to,	
			@p_object_type_id		= @p_object_type_id,		
			@p_action_type_id		= @p_action_type_id,
			@p_functionality_id		= @p_functionality_id,
			@p_key					= @p_key,			
			@p_user_id				= @p_user_id,			
			@p_ip_address			= @p_ip_address,		
			@p_start_index			= @p_start_index,
			@p_page_size			= @p_page_size,
			@p_count				= @p_count output;
				
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
