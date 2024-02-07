




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_srv_instances_search]
	@p_srv_instance_ids					  NVARCHAR(MAX),
	@p_status							  TINYINT,
	@p_applicant_id						  INT,
	@p_service_instance_date_from		  DATETIME,
	@p_service_instance_date_to			  DATETIME,
	@p_service_id					      INT,
	@p_case_file_uri					  NVARCHAR(100),
	@p_start_index						  INT,
	@p_page_size						  INT,
	@p_calculate_count					  BIT,
	@p_count							  INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
		IF (@p_page_size IS NULL AND @p_start_index  IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_srv_instance_search]';
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
		DECLARE @v_curr_user_id		INT;

		IF @p_srv_instance_ids IS NOT NULL AND rtrim(ltrim(@p_srv_instance_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_srv_instance_ids, ',');
		END

		SET @v_Params = N'
		@v_ids_table						  TT_INTEGERS READONLY,
		@p_status							  TINYINT,
		@p_applicant_id						  INT,
		@p_service_instance_date_from		  DATETIME,
		@p_service_instance_date_to			  DATETIME,
		@p_service_id					      INT,
		@p_case_file_uri					  NVARCHAR(100),
		@p_start_index						  INT,
		@p_page_size						  INT,
		@p_count							  INT OUTPUT';

		SET @v_statement = N'
			SELECT si.[service_instance_id]
				   ,si.[status]
				   ,si.[applicant_id]
				   ,si.[service_instance_date]
				   ,si.[service_id]
				   ,si.[case_file_uri]
				   ,si.[additional_data]
			  FROM [dbo].[service_instances] si
			 WHERE 
			   dbo.[f_has_user_access_to_row](si.applicant_id) = CAST (1 AS BIT)
			   AND si.[is_last] = 1 
			   AND si.[deactivation_ver_id] IS NULL';
	   
	   	IF @p_srv_instance_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND si.service_instance_id in (SELECT * FROM @v_ids_table)
		';
		END

		IF @p_status IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND si.[status] = @p_status';
		END  	

		IF @p_applicant_id IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND si.[applicant_id] = @p_applicant_id';
		END  	
		
		IF @p_service_instance_date_from IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND si.[service_instance_date] >= @p_service_instance_date_from
		';
		END

		IF @p_service_instance_date_to IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND si.[service_instance_date] <= @p_service_instance_date_to
		';
		END

		IF @p_service_id IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND si.[service_id] = @p_service_id';
		END  	
		
		IF @p_case_file_uri IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND si.[case_file_uri] = @p_case_file_uri';
		END  	
		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params							= @v_Params,				
				@v_ids_table					= @v_ids_table,
				@p_status						= @p_status,
				@p_applicant_id					= @p_applicant_id,
				@p_service_instance_date_from	= @p_service_instance_date_from,
				@p_service_instance_date_to		= @p_service_instance_date_to,
				@p_service_id					= @p_service_id,
				@p_case_file_uri				= @p_case_file_uri,
				@p_start_index					= @p_start_index,
				@p_page_size					= @p_page_size,
				@p_count						= @p_count output;				
		END

		SET @v_statement += N' 
			ORDER BY [service_instance_date] DESC, [service_instance_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';

			
		
		EXEC sp_executeSQL @v_statement,
			@params							= @v_Params,				
			@v_ids_table					= @v_ids_table,
			@p_status						= @p_status,
			@p_applicant_id					= @p_applicant_id,
			@p_service_instance_date_from	= @p_service_instance_date_from,
			@p_service_instance_date_to		= @p_service_instance_date_to,
			@p_service_id					= @p_service_id,
			@p_case_file_uri				= @p_case_file_uri,
			@p_start_index					= @p_start_index,
			@p_page_size					= @p_page_size,
			@p_count						= @p_count output;	
				
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