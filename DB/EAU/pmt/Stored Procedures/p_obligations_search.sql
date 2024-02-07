






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_obligations_search]
	@p_obligations_search_ids			[pmt].[tt_obligations_search_ids] READONLY,
	@p_is_active						BIT,
	@p_type								TINYINT,
	@p_obligation_ids					NVARCHAR(MAX),
	@p_statuses							NVARCHAR(MAX),
	@p_applicant_id						INT,
	@p_is_applicant_anonimous			BIT,
	@p_service_instance_id				BIGINT,
	@p_with_lock						BIT,
	@p_load_payment_requests			BIT,
	@p_start_index						INT,
	@p_page_size						INT,
	@p_calculate_count					BIT,
	@p_count							INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_page_size IS NULL AND @p_start_index IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_obligations_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement						NVARCHAR(max);
		DECLARE @v_statement_count					NVARCHAR(max);
		DECLARE @v_Params							NVARCHAR(1000);
		DECLARE @v_obligation_ids_table				tt_integers;
		DECLARE @v_status_ids_table					tt_integers;

		IF @p_obligation_ids IS NOT NULL AND rtrim(ltrim(@p_obligation_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_obligation_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_obligation_ids, ',');
		END

		IF @p_statuses IS NOT NULL AND rtrim(ltrim(@p_statuses)) <> ''
		BEGIN
			INSERT INTO 
				@v_status_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_statuses, ',');
		END

		SET @v_Params = N'
				@p_obligations_search_ids			[pmt].[tt_obligations_search_ids] READONLY,
				@p_type								TINYINT,
				@v_obligation_ids_table				TT_INTEGERS READONLY,
				@v_status_ids_table					TT_INTEGERS READONLY,
				@p_applicant_id						INT,
				@p_service_instance_id				BIGINT,
				@p_start_index						INT,
				@p_page_size						INT,
				@p_count							INT OUTPUT';

		SET @v_statement = N'SELECT 
							o.[obligation_id]
							,o.[status]
							,o.[amount]
							,o.[discount_amount]
							,o.[bank_name]
							,o.[bic]
							,o.[iban]
							,o.[payment_reason]
							,o.[pep_cin]
							,o.[expiration_date]
							,o.[applicant_id]
							,o.[obliged_person_name]
							,o.[obliged_person_ident]
							,o.[obliged_person_ident_type]
							,o.[obligation_date]
							,o.[obligation_identifier]
							,o.[type]
							,o.[service_instance_id]
							,o.[service_id]
							,o.[additional_data]
						FROM [pmt].[obligations] o ';

		
		IF @p_with_lock IS NOT NULL AND @p_with_lock = 1
		BEGIN
			SET @v_statement = @v_statement + N' 
						WITH(UPDLOCK) ';
		END

		IF (EXISTS (SELECT 1 FROM @p_obligations_search_ids))
		BEGIN
			SET @v_statement = @v_statement + N' INNER JOIN @p_obligations_search_ids oids
												 ON oids.[obligation_date] = o.[obligation_date]
												 AND oids.[obligation_identifier] = o.[obligation_identifier]
		';
		END

		SET @v_statement = @v_statement + N' 
						WHERE 1 = 1 ';

		IF @p_type IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[type] = @p_type
		';
		END  

	    IF @p_is_active IS NOT NULL AND @p_is_active = 1
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[expiration_date] >=  CONVERT(DATE, [dbo].[f_sys_get_time]())
		';
		END

		IF @p_obligation_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[obligation_id] in (SELECT * FROM @v_obligation_ids_table)
		';
		END

		IF @p_statuses IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[status] in (SELECT * FROM @v_status_ids_table)
		';
		END

		IF @p_applicant_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[applicant_id] = @p_applicant_id
		';
		END  

		IF @p_is_applicant_anonimous = 1
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[applicant_id] IS NULL
		';
		END  
		
		IF @p_service_instance_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND o.[service_instance_id] = @p_service_instance_id
		';
		END  

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params								= @v_Params,	
				@p_obligations_search_ids			= @p_obligations_search_ids,
				@p_type								= @p_type,
				@v_obligation_ids_table				= @v_obligation_ids_table,
				@v_status_ids_table					= @v_status_ids_table,
				@p_applicant_id						= @p_applicant_id,
				@p_service_instance_id				= @p_service_instance_id,
				@p_start_index						= @p_start_index,
				@p_page_size						= @p_page_size,
				@p_count							= @p_count output;				
		END

		SET @v_statement = @v_statement + N' ORDER BY [obligation_id] DESC
											  OFFSET @p_start_index - 1 ROWS
												FETCH NEXT @p_page_size ROWS ONLY';
		
		DECLARE @v_obligations_result [pmt].[tt_obligations];

		INSERT INTO @v_obligations_result
		EXEC sp_executeSQL @v_statement,
			@params								= @v_Params,
			@p_obligations_search_ids			= @p_obligations_search_ids,
			@p_type								= @p_type,
			@v_obligation_ids_table				= @v_obligation_ids_table,
			@v_status_ids_table					= @v_status_ids_table,
			@p_applicant_id						= @p_applicant_id,
			@p_service_instance_id				= @p_service_instance_id,
			@p_start_index						= @p_start_index,
			@p_page_size						= @p_page_size,
			@p_count							= @p_count output;	

		SELECT * FROM @v_obligations_result;

		IF @p_load_payment_requests = 1
		BEGIN
			SELECT 
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
				INNER JOIN @v_obligations_result tt ON tt.obligation_id = pr.obligation_id
				ORDER BY [pay_date] DESC;
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