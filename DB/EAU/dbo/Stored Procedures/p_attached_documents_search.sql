
-- =============================================
-- Author:		Georgiev
-- Create date: 5/12/2020 9:23:15 AM
-- Description:	Търси записи от таблица [dbo].[attached_documents]
-- =============================================
CREATE PROCEDURE [dbo].[p_attached_documents_search]
	@p_attached_document_id		BIGINT,
	@p_document_process_id		BIGINT,
	@p_load_content				BIT,
	@p_signing_guid				UNIQUEIDENTIFIER,
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
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_attached_documents_search]';
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


		SET @v_Params = N'
			@p_attached_document_id		BIGINT,
			@p_document_process_id		BIGINT,
			@p_signing_guid				UNIQUEIDENTIFIER';

		SET @v_statement = N'
			SELECT [attached_document_id]
				,[attached_document_guid]
				,[document_process_id]
				,[document_process_content_id]
				,[document_type_id]
				,[description]
				,[mime_type]
				,[file_name]
				,[html_template_content]
				,[signing_guid]
				,[created_by]
				,[created_on]
				,[updated_by]
				,[updated_on]
			 FROM [dbo].[attached_documents]
			 WHERE 1 = 1 
			 ';

		IF @p_attached_document_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [attached_document_id] =  @p_attached_document_id
		';
		END  

		IF @p_document_process_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [document_process_id] = @p_document_process_id
		';
		END

		IF @p_signing_guid IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [signing_guid] = @p_signing_guid
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
				@p_attached_document_id		= @p_attached_document_id,
				@p_document_process_id		= @p_document_process_id,
				@p_signing_guid				= @p_signing_guid;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';

		SET @v_statement = @v_statement + N' ORDER BY [attached_document_id]
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';

		DECLARE @v_attached_docs_result [dbo].[tt_attached_documents];
		
		INSERT INTO @v_attached_docs_result
		EXEC sp_executeSQL @v_statement,
			@params						= @v_Params,				
			@p_attached_document_id		= @p_attached_document_id,
			@p_document_process_id		= @p_document_process_id,
			@p_signing_guid				= @p_signing_guid,
			@p_start_index				= @p_start_index,
			@p_page_size				= @p_page_size;	

		SELECT * FROM @v_attached_docs_result;

		IF @p_load_content = 1
		BEGIN
			SELECT 
				dpc.[document_process_content_id],
				dpc.[document_process_id],
				dpc.[type]
	   FROM [dbo].[document_process_contents] dpc
			INNER JOIN @v_attached_docs_result tt on tt.document_process_content_id = dpc.document_process_content_id;
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
