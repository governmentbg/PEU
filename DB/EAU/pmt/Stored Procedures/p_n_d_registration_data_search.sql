





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_n_d_registration_data_search]
	@p_registration_data_ids		NVARCHAR(MAX),
	@p_type							INT,
	@p_cin							NVARCHAR(100),
	@p_start_index					INT,
	@p_page_size					INT,
	@p_calculate_count				BIT,
	@p_count						INT OUT,
	@p_last_updated_on				DATETIMEOFFSET(3) OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_n_d_registration_data_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[pmt].[n_d_registration_data]';

		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		tt_integers;

		IF @p_registration_data_ids IS NOT NULL AND rtrim(ltrim(@p_registration_data_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_registration_data_ids, ',');
		END

		SET @v_Params = N'
				@v_ids_table		TT_INTEGERS READONLY,
				@p_type				INT,
				@p_cin				NVARCHAR(100),
				@p_start_index		INT,
				@p_page_size		INT,
				@p_count			INT OUTPUT';

		SET @v_statement = N'SELECT 
						  r.[registration_data_id],
						  r.[type],
						  r.[description],
						  r.[cin],
						  r.[email],
						  r.[secret_word],
						  r.[validity_period],
						  r.[portal_url],
						  r.[notification_url],
						  r.[service_url]
						FROM [pmt].[n_d_registration_data] r
						WHERE 1 = 1 ';

		IF @p_registration_data_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND r.[registration_data_id] in (SELECT * FROM @v_ids_table)
		';
		END

		IF @p_type IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND r.[type] = @p_type
		';
		END  	

		IF @p_cin IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND r.[cin] = @p_cin
		';
		END  	

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,	
				@v_ids_table		 = @v_ids_table,
				@p_type				 = @p_type,
				@p_cin				 = @p_cin,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement = @v_statement + N' ORDER BY [registration_data_id]
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,	
			@v_ids_table		 = @v_ids_table,
			@p_type				 = @p_type,
			@p_cin				 = @p_cin,
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