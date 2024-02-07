







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_payment_requests_search]
	@p_payment_request_ids				NVARCHAR(MAX),
	@p_obligation_ids					NVARCHAR(MAX),
	@p_registration_data_id				INT,
	@p_external_portal_number			NVARCHAR(100),
	@p_start_index						INT,
	@p_page_size						INT,
	@p_calculate_count					BIT,
	@p_count							INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ((@p_payment_request_ids IS NULL AND @p_obligation_ids IS NULL AND @p_registration_data_id IS NULL AND @p_external_portal_number IS NULL) 
		OR @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_payment_requests_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement					NVARCHAR(max);
		DECLARE @v_statement_count				NVARCHAR(max);
		DECLARE @v_Params						NVARCHAR(1000);
		DECLARE @v_payment_request_ids_table	tt_integers;
		DECLARE @v_obligation_ids_table			tt_integers;

		IF @p_payment_request_ids IS NOT NULL AND rtrim(ltrim(@p_payment_request_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_payment_request_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_payment_request_ids, ',');
		END

				IF @p_obligation_ids IS NOT NULL AND rtrim(ltrim(@p_obligation_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_obligation_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_obligation_ids, ',');
		END

		SET @v_Params = N'
				@v_payment_request_ids_table	TT_INTEGERS READONLY,
				@v_obligation_ids_table			TT_INTEGERS READONLY,
				@p_registration_data_id			INT,
				@p_external_portal_number		NVARCHAR(100),
				@p_start_index					INT,
				@p_page_size					INT,
				@p_count						INT OUTPUT';

		SET @v_statement = N'SELECT 
							  pr.[payment_request_id]
							  ,pr.[registration_data_id]
							  ,pr.[registration_data_type]
							  ,pr.[status]
							  ,pr.[obligation_id]
							  ,pr.[obliged_person_name]
							  ,pr.[obliged_person_ident]
							  ,pr.[obliged_person_ident_type]
							  ,pr.[send_date]
							  ,pr.[pay_date]
							  ,pr.[external_portal_payment_number]
							  ,pr.[amount]
							  ,pr.[additional_data]
						FROM [pmt].[payment_requests] pr
						WHERE 1 = 1 ';


		IF @p_payment_request_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND pr.[payment_request_id] in (SELECT * FROM @v_payment_request_ids_table)
		';
		END

		IF @p_obligation_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND pr.[obligation_id] in (SELECT * FROM @v_obligation_ids_table)
		';
		END

		IF @p_registration_data_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND pr.[registration_data_id] = @p_registration_data_id
		';
		END

		IF @p_external_portal_number IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND LOWER(pr.[external_portal_payment_number]) LIKE LOWER(@p_external_portal_number)
		';
		END

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params							= @v_Params,
				@v_payment_request_ids_table	= @v_payment_request_ids_table,
				@v_obligation_ids_table			= @v_obligation_ids_table,
				@p_registration_data_id			= @p_registration_data_id,
				@p_external_portal_number		= @p_external_portal_number,
				@p_start_index					= @p_start_index,
				@p_page_size					= @p_page_size,
				@p_count						= @p_count output;				
		END

		SET @v_statement = @v_statement + N' ORDER BY payment_request_id
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params							= @v_Params,	
			@v_payment_request_ids_table	= @v_payment_request_ids_table,
			@v_obligation_ids_table			= @v_obligation_ids_table,
			@p_registration_data_id			= @p_registration_data_id,
			@p_external_portal_number		= @p_external_portal_number,
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