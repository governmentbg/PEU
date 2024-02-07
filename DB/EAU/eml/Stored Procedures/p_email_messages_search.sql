




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [eml].[p_email_messages_search]
	@p_priority   							SMALLINT,
	@p_status								SMALLINT,
	@p_is_do_not_process_before_expired		BIT,
	@p_start_index							INT,
	@p_page_size							INT,
	@p_calculate_count						BIT,
	@p_count								INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_start_index IS NULL 
		OR @p_page_size IS NULL OR
		(@p_priority IS NULL AND @p_status IS NULL AND @p_is_do_not_process_before_expired IS NULL)) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[eml].[p_email_messages_search]';
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
			@p_priority								SMALLINT,
			@p_status								SMALLINT,
			@p_is_do_not_process_before_expired		BIT
		';

		SET @v_statement = N'
			SELECT [email_id]
				  ,[priority]
				  ,[status]
				  ,[try_count]
				  ,[send_date]
				  ,[subject]
				  ,[body]
				  ,[is_body_html]
				  ,[do_not_process_before]
				  ,[sending_provider_name]
				  ,[recipients]
				  ,[created_by]
				  ,[created_on]
				  ,[updated_by]
				  ,[updated_on]
			 FROM [eml].[email_messages]
			 WHERE 1 = 1 
			 ';

		IF @p_priority IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [priority] = @p_priority
		';
		END 
		
		IF @p_status IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [status] =  @p_status
		';
		END  

		IF @p_is_do_not_process_before_expired IS NOT NULL
		BEGIN
			IF @p_is_do_not_process_before_expired = 1
				SET @v_statement = @v_statement + N'  AND ([do_not_process_before] IS NULL OR [do_not_process_before] <= dbo.f_sys_get_time())
				';
			ELSE
				SET @v_statement = @v_statement + N' AND ([do_not_process_before] IS NOT NULL AND [do_not_process_before] > dbo.f_sys_get_time())
				';
		END 

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';

			SET @v_Params_count = N' @p_count		INT OUTPUT,
			' + @v_Params;
			
			EXEC sp_executeSQL @v_statement_count,
				@params									= @v_Params_count,	
				@p_count								= @p_count output,
				@p_priority								= @p_priority,
				@p_status								= @p_status,
				@p_is_do_not_process_before_expired		= @p_is_do_not_process_before_expired;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';

		SET @v_statement = @v_statement + N' ORDER BY [email_id]
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params									= @v_Params,				
			@p_priority								= @p_priority,
			@p_status								= @p_status,
			@p_is_do_not_process_before_expired		= @p_is_do_not_process_before_expired,
			@p_start_index							= @p_start_index,
			@p_page_size							= @p_page_size;	
				
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
