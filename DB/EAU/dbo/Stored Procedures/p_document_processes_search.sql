

-- =============================================
-- Author:		Georgiev
-- Create date: 5/12/2020 10:40:20 AM
-- Description:	Търси процеси на документи.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_processes_search]
	@p_document_process_id		BIGINT,
	@p_applicant_cin			INT,
	@p_service_id				INT,
	@p_request_id				NVARCHAR(50),
	@p_signing_guid				uniqueidentifier,
	@p_with_lock				BIT,
	@p_load_all_content			BIT,
	@p_load_attached_documents	BIT,
	@p_load_json_content		BIT,
	@p_load_xml_content			BIT,
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
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_processes_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_statement_where   NVARCHAR(1000);
		DECLARE @v_Params_count		NVARCHAR(1000);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
			@p_document_process_id		BIGINT,
			@p_applicant_cin			INT,
			@p_service_id				INT,
			@p_request_id				NVARCHAR(50),
			@p_signing_guid				uniqueidentifier';


		SET @v_statement = N'
			SELECT dp.[document_process_id]
				,dp.[applicant_id]
				,dp.[document_type_id]
				,dp.[service_id]
				,dp.[status]
				,dp.[mode]
				,dp.[type]
				,dp.[additional_data]
				,dp.[signing_guid]
				,dp.[error_message]
				,dp.[case_file_uri]
				,dp.[not_acknowledged_message_uri]
				,dp.[request_id]
				,dp.[created_on]
			 FROM [dbo].[document_processes] dp	
			 ';

		SET @v_statement_where = N' WHERE 1 = 1
		';

		IF @p_document_process_id IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND dp.[document_process_id] = @p_document_process_id
		';
		END 
		
		IF @p_applicant_cin IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND u.[cin] = @p_applicant_cin
		';
		END  

		IF @p_service_id IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND dp.[service_id] = @p_service_id
		';
		END 

		IF @p_request_id IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND dp.[request_id] = @p_request_id
		';
		END 

		IF @p_signing_guid IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND dp.[signing_guid] = @p_signing_guid
		';
		END 

		IF(@p_applicant_cin IS NOT NULL)
		BEGIN
			SET @v_statement = @v_statement + N' INNER JOIN [users].[users]	u ON u.[user_id] = dp.[applicant_id]
			';
		END		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + @v_statement_where + ' ) as t';

			SET @v_Params_count = N' @p_count		INT OUTPUT,
			' + @v_Params;
			
			EXEC sp_executeSQL @v_statement_count,
				@params						= @v_Params_count,	
				@p_count					= @p_count output,
				@p_document_process_id		= @p_document_process_id,
				@p_applicant_cin			= @p_applicant_cin,
				@p_service_id				= @p_service_id,
				@p_request_id				= @p_request_id,
				@p_signing_guid				= @p_signing_guid;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';
		
		IF(@p_with_lock IS NOT NULL AND @p_with_lock = 1)
		BEGIN
			SET @v_statement = @v_statement + N' WITH(UPDLOCK)
			';
		END 
				
		SET @v_statement = @v_statement + @v_statement_where;

		SET @v_statement = @v_statement + N' 
			ORDER BY [document_process_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';
		
		DECLARE @v_doc_processes_result [dbo].[tt_document_processes];

		INSERT INTO @v_doc_processes_result
		EXEC sp_executeSQL @v_statement,
			@params						= @v_Params,				
			@p_document_process_id		= @p_document_process_id,
			@p_applicant_cin			= @p_applicant_cin,
			@p_service_id				= @p_service_id,
			@p_request_id				= @p_request_id,
			@p_signing_guid				= @p_signing_guid,
			@p_start_index				= @p_start_index,
			@p_page_size				= @p_page_size;	

		SELECT * FROM @v_doc_processes_result;


		IF @p_load_attached_documents = 1
		BEGIN
			SELECT ad.* FROM [dbo].[attached_documents] ad
			INNER JOIN @v_doc_processes_result tt on tt.document_process_id = ad.document_process_id;
		END;

		IF @p_load_all_content = 1
		BEGIN
			SELECT dpc.document_process_content_id
				,dpc.document_process_id
				,dpc.type
				,dpc.text_content
			FROM [dbo].[document_process_contents] dpc
			INNER JOIN @v_doc_processes_result tt on tt.document_process_id = dpc.document_process_id;
		END
		ELSE
		BEGIN
			IF @p_load_json_content = 1 AND @p_load_xml_content = 1
			BEGIN
				SELECT dpc.document_process_content_id
					,dpc.document_process_id
					,dpc.type
					,dpc.text_content
				FROM [dbo].[document_process_contents] dpc
				INNER JOIN @v_doc_processes_result tt on tt.document_process_id = dpc.document_process_id
				WHERE dpc.[type] IN (1,2);
			END
			ELSE IF @p_load_json_content = 1
			BEGIN
				SELECT dpc.document_process_content_id
					,dpc.document_process_id
					,dpc.type
					,dpc.text_content
				FROM [dbo].[document_process_contents] dpc
				INNER JOIN @v_doc_processes_result tt on tt.document_process_id = dpc.document_process_id
				WHERE dpc.[type] = 1;
			END
			ELSE IF @p_load_xml_content = 1
			BEGIN
				SELECT dpc.document_process_content_id
					,dpc.document_process_id
					,dpc.type
					,dpc.text_content
				FROM [dbo].[document_process_contents] dpc
				INNER JOIN @v_doc_processes_result tt on tt.document_process_id = dpc.document_process_id
				WHERE dpc.[type] = 2;
			END;
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