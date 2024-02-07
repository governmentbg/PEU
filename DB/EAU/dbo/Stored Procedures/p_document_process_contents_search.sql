
-- =============================================
-- Author:		Georgiev
-- Create date: 5/12/2020 6:03:00 PM
-- Description:	Търси записи от таблица [dbo].[document_process_contents]
-- =============================================
CREATE PROCEDURE [dbo].[p_document_process_contents_search]
	@p_document_process_ids		[dbo].[tt_bigintegers] READONLY,
	@p_type						SMALLINT,
	@p_start_index				INT,
	@p_page_size				INT,
	@p_calculate_count			BIT,
	@p_count					INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_start_index IS NULL 
		OR @p_page_size IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_process_contents_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params_count		NVARCHAR(1000);
		DECLARE @v_Params			NVARCHAR(1000);
		--DECLARE @v_ids_table		[dbo].[tt_bigintegers];

		--IF @p_document_process_ids IS NOT NULL AND rtrim(ltrim(@p_document_process_ids)) <> ''
		--BEGIN
		--	INSERT INTO 
		--		@v_ids_table
		--	SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_bigint_numbers](@p_document_process_ids, ',');
		--END

		SET @v_Params = N'
			@p_document_process_ids		[dbo].[tt_bigintegers] READONLY,
			@p_type						SMALLINT';

		SET @v_statement = N'
			SELECT [document_process_content_id]
			  ,[document_process_id]
			  ,[type]
			  ,[created_by]
			  ,[created_on]
			  ,[updated_by]
			  ,[updated_on]
			  ,[length]
		     FROM [dbo].[document_process_contents]
			 WHERE 1 = 1 
			 ';

		IF (EXISTS(SELECT 1 FROM @p_document_process_ids))
		BEGIN
			SET @v_statement = @v_statement + N' AND [document_process_id] in (SELECT [VALUE] FROM @p_document_process_ids)
		';
		END  

		IF @p_type IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [type] = @p_type
		';
		END

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';

			SET @v_Params_count = N' @p_count		INT OUTPUT,
			' + @v_Params;
			
			EXEC sp_executeSQL @v_statement_count,
				@params						= @v_Params_count,	
				@p_count					= @p_count output,
				@p_document_process_ids		= @p_document_process_ids,
				@p_type						= @p_type;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';

		SET @v_statement = @v_statement + N' ORDER BY [document_process_content_id]
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params						= @v_Params,				
			@p_document_process_ids		= @p_document_process_ids,
			@p_type						= @p_type,
			@p_start_index				= @p_start_index,
			@p_page_size				= @p_page_size;	
				
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