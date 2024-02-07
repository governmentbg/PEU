


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_data_service_limits_search]
	@p_service_limit_ids		NVARCHAR(MAX),
	@p_service_code				NVARCHAR(100),
	@p_service_name				NVARCHAR(500),
	@p_status					INT,
	@p_start_index				INT,
	@p_page_size				INT,
	@p_calculate_count			BIT,
	@p_count					INT OUT,
	@p_last_updated_on			DATETIMEOFFSET(3) OUT
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
		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[dbo].[data_service_limits]'

		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		tt_integers;

		IF @p_service_limit_ids IS NOT NULL AND rtrim(ltrim(@p_service_limit_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_service_limit_ids, ',');
		END

		SET @v_Params = N'
				@v_ids_table			TT_INTEGERS READONLY,
				@p_service_code			NVARCHAR(100),
				@p_service_name			NVARCHAR(500),
				@p_status				INT,
				@p_start_index			INT,
				@p_page_size			INT,
				@p_count				INT OUTPUT';

		SET @v_statement = N'SELECT 
					t.[service_limit_id]
					  ,t.[service_code]
					  ,t.[service_name]
					  ,t.[requests_interval]
					  ,t.[requests_number]
					  ,t.[status]
					FROM [dbo].[data_service_limits] t 
                    WHERE [is_last] = 1 AND [deactivation_ver_id] IS NULL';

		IF @p_service_limit_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.service_limit_id in (SELECT * FROM @v_ids_table)
		';
		END
  
		IF @p_service_code IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.service_code = @p_service_code
		';
		END

		IF @p_service_name IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' lower(t.service_name) LIKE concat(''%'',lower(@p_service_name), ''%'')
		';
		END
  
		IF @p_status IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND t.status = @p_status 
		';
		END;

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params					= @v_Params,				
				@v_ids_table			= @v_ids_table,	
				@p_service_code			= @p_service_code,
				@p_service_name			= @p_service_name,	
				@p_status				= @p_status,				
				@p_start_index			= @p_start_index,
				@p_page_size			= @p_page_size,
				@p_count				= @p_count output;
		END

		SET @v_statement = @v_statement + N' ORDER BY t.service_limit_id desc
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';		
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_ids_table			= @v_ids_table,	
			@p_service_code			= @p_service_code,
			@p_service_name			= @p_service_name,	
			@p_status				= @p_status,			
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