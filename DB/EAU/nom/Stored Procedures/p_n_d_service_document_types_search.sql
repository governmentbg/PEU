



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_service_document_types_search]
	@p_service_ids		NVARCHAR(MAX),
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_service_document_types_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		 NVARCHAR(max);
		DECLARE @v_statement_count	 NVARCHAR(max);
		DECLARE @v_Params			 NVARCHAR(1000);
		DECLARE @v_service_ids_table tt_integers;

		IF @p_service_ids IS NOT NULL AND rtrim(ltrim(@p_service_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_service_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_service_ids, ',');
		END

		SET @v_Params = N'
		@v_service_ids			TT_INTEGERS READONLY,
		@p_start_index			INT,
		@p_page_size			INT,
		@p_count				INT OUTPUT';

		SET @v_statement = N'
		SELECT [service_id]
			  ,[doc_type_id]			  
		  FROM [nom].[n_d_service_document_types]
		 WHERE [is_last] = 1 
		   AND [deactivation_ver_id] IS NULL';

		IF @p_service_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND [service_id] in (SELECT * FROM @v_service_ids)';
		END  		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@v_service_ids		 = @v_service_ids_table,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement = @v_statement + N' 
			ORDER BY [service_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_service_ids		 = @v_service_ids_table,
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