


-- =============================================
-- Author:		Georgiev
-- Create date: 6/15/2020 10:40:20 AM
-- Description:	Търси процеси по подписване.
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_search]
	@p_process_id		UNIQUEIDENTIFIER,
	@p_status			SMALLINT,
	@p_with_tx_lock		BIT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_start_index IS NULL 
		OR @p_page_size IS NULL 
		OR (@p_process_id IS NULL AND @p_status IS NULL)) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_statement_where   NVARCHAR(100);
		DECLARE @v_Params_count		NVARCHAR(1000);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
			@p_process_id	UNIQUEIDENTIFIER,
			@p_status		SMALLINT';


		SET @v_statement = N'
			SELECT 
			  [process_id]
			  ,[callback_client_config_id]
			  ,[format]
			  ,[status]
			  ,[level]
			  ,[type]
			  ,[digest_method]
			  ,[additional_data]
			  ,[rejected_callback_url]
			  ,[completed_callback_url]
			  ,[file_name]
			  ,[content_type]
			  ,null as [content]
			  ,c.http_client_name as callback_http_client_name
			 FROM [sign].[signing_processes] as sp' 
			 + (CASE 
				WHEN(@p_with_tx_lock = 1) 
					THEN N' WITH(UPDLOCK)
					' 
				ELSE N'
				' END) +
			 N'INNER JOIN [sign].[n_s_callback_clients_config] as c
				ON (c.callback_client_id = sp.callback_client_config_id) ';

		SET @v_statement_where = N'
			 WHERE 1=1 
		';

		IF @p_process_id IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND [process_id] = @p_process_id
		';
		END 
		
		IF @p_status IS NOT NULL
		BEGIN
			SET @v_statement_where = @v_statement_where + N' AND [status] =  @p_status
		';
		END  

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + @v_statement_where + ' ) as t';

			SET @v_Params_count = N' @p_count		INT OUTPUT,
			' + @v_Params;
			
			EXEC sp_executeSQL @v_statement_count,
				@params				= @v_Params_count,	
				@p_count			= @p_count output,
				@p_process_id		= @p_process_id,
				@p_status			= @p_status;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';
		

		SET @v_statement = @v_statement + @v_statement_where;

		SET @v_statement = @v_statement + N' 
			ORDER BY [process_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';

		--PRINT @v_statement;

		EXEC sp_executeSQL @v_statement,
			@params				= @v_Params,				
			@p_process_id		= @p_process_id,
			@p_status			= @p_status,
			@p_start_index		= @p_start_index,
			@p_page_size		= @p_page_size;	

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