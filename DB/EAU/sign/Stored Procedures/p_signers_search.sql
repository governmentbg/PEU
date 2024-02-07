

-- =============================================
-- Author:		Georgiev
-- Create date: 5/12/2020 9:23:15 AM
-- Description:	Търси записи от таблица [sign].[signers]
-- =============================================
CREATE PROCEDURE [sign].[p_signers_search]
	@p_signer_id		BIGINT,
	@p_process_ids		[dbo].[tt_guids] READONLY,
	@p_signing_channel	SMALLINT,
	@p_transaction_id	NVARCHAR(50),
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_start_index IS NULL 
		OR @p_page_size IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signers_search]';
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
			@p_signer_id		INT,
			@p_process_ids		[dbo].[tt_guids] READONLY,
			@p_signing_channel  SMALLINT,
			@p_transaction_id   NVARCHAR(50)';

		SET @v_statement = N'
			SELECT [signer_id]
			  ,[process_id]
			  ,[name]
			  ,[ident]
			  ,[order]
			  ,[status]
			  ,[signing_channel]
			  ,[transaction_id]
			  ,[additional_sign_data]
			  ,[reject_reson_label]
			 FROM [sign].[signers]
			 WHERE 1 = 1 
			 ';

		IF @p_signer_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [signer_id] =  @p_signer_id
		';
		END  

		IF (EXISTS (SELECT 1 FROM @p_process_ids))
		BEGIN
			SET @v_statement = @v_statement + N' AND [process_id] IN (SELECT [id] FROM @p_process_ids)
		';
		END

		IF @p_signing_channel IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [signing_channel] =  @p_signing_channel
		';
		END  

		IF @p_transaction_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND [transaction_id] =  @p_transaction_id
		';
		END  

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';

			SET @v_Params_count = N' @p_count		INT OUTPUT,
			' + @v_Params;
			
			EXEC sp_executeSQL @v_statement_count,
				@params				= @v_Params_count,	
				@p_count			= @p_count output,
				@p_signer_id		= @p_signer_id,
				@p_process_ids		= @p_process_ids,
				@p_signing_channel  = @p_signing_channel,
				@p_transaction_id   = @p_transaction_id;				
		END

		SET @v_Params = @v_Params + N',
				@p_start_index		INT,
				@p_page_size		INT';

		SET @v_statement = @v_statement + N' ORDER BY [signer_id]
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';


		EXEC sp_executeSQL @v_statement,
			@params				= @v_Params,				
			@p_signer_id		= @p_signer_id,
			@p_process_ids		= @p_process_ids,
			@p_signing_channel  = @p_signing_channel,
			@p_transaction_id   = @p_transaction_id,
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