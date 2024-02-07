


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_processes_search]
	@p_process_ids		NVARCHAR(MAX),
	@p_process_guids	NVARCHAR(MAX),
	@p_user_ids			NVARCHAR(MAX),
	@p_process_type		TINYINT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_page_size IS NULL AND @p_start_index  IS NULL AND (@p_process_guids IS NULL OR @p_process_guids = '')) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_processes_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_pids_table		tt_integers;
		DECLARE @v_uids_table		tt_integers;
		DECLARE @v_guids_table		tt_strings;

		IF @p_process_ids IS NOT NULL AND rtrim(ltrim(@p_process_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_pids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_process_ids, ',');
		END

		IF @p_user_ids IS NOT NULL AND rtrim(ltrim(@p_user_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_uids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_user_ids, ',');
		END

		IF @p_process_guids IS NOT NULL AND rtrim(ltrim(@p_process_guids)) <> ''
		BEGIN
			INSERT INTO 
				@v_guids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_strings](@p_process_guids, ',');
		END

		SET @v_Params = N'
				@v_pids_table		TT_INTEGERS READONLY,
				@v_uids_table		TT_INTEGERS READONLY,
				@v_guids_table		TT_STRINGS READONLY,
				@p_process_type		INT,
				@p_start_index		INT,
				@p_page_size		INT,
				@p_count			INT OUTPUT';

		SET @v_statement = N'SELECT 
					[process_id], [process_guid], 
					[process_type], [user_id], 
					[invalid_after], [status],
					[updated_by], [updated_on]
				FROM [users].[user_processes] p
				WHERE 1 = 1';

		IF @p_process_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND p.process_id in (SELECT * FROM @v_pids_table)
		';
		END
  
		IF @p_user_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND p.user_id in (SELECT * FROM @v_uids_table)
		';
		END

		IF @p_process_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND p.process_id in (SELECT * FROM @v_pids_table)
		';
		END
  
		IF @p_process_guids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND p.process_guid in (SELECT * FROM @v_guids_table)
		';
		END

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@v_pids_table		 = @v_pids_table,
				@v_uids_table		 = @v_uids_table,
				@v_guids_table		 = @v_guids_table,
				@p_process_type		 = @p_process_type,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement = @v_statement + N' ORDER BY p.user_id
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_pids_table		 = @v_pids_table,
			@v_uids_table		 = @v_uids_table,
			@v_guids_table		 = @v_guids_table,
			@p_process_type		 = @p_process_type,
			@p_start_index		 = @p_start_index,
			@p_page_size		 = @p_page_size,
			@p_count			 = @p_count output;
				
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
