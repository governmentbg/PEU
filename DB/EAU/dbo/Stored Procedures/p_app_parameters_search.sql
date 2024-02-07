



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_app_parameters_search]
	@p_functionality_id INT,
	@p_code				NVARCHAR(100),
	@p_code_is_exact	BIT,
	@p_description		NVARCHAR(500),
	@p_is_system		BIT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT,
	@p_last_updated_on	DATETIMEOFFSET(3) OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
		IF (@p_page_size IS NULL AND @p_start_index  IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_app_parameters_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[dbo].[app_parameters]';
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
		@p_functionality_id INT,
		@p_code				NVARCHAR(100),
		@p_description		NVARCHAR(500),
		@p_is_system		BIT,
		@p_start_index		INT,
		@p_page_size		INT,
		@p_count			INT OUTPUT';

		SET @v_statement = N'
			SELECT a.[app_param_id]
				  ,a.[functionality_id]
				  ,a.[code]
				  ,a.[description]
				  ,a.[is_system]
				  ,a.[param_type]
				  ,a.[value_datetime]
				  ,a.[value_interval]
				  ,a.[value_string]
				  ,a.[value_int]
				  ,a.[value_hour]
			  FROM [dbo].[app_parameters] a
   LEFT OUTER JOIN [dbo].[n_s_functionalities] f ON f.functionality_id = a.functionality_id
			 WHERE a.[is_last] = 1 
			   AND a.[deactivation_ver_id] IS NULL';

		IF @p_functionality_id IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND a.[functionality_id] = @p_functionality_id';
		END  	
		
		
		IF @p_code IS NOT NULL
		BEGIN
			IF @p_code_is_exact = 1
			BEGIN
				 SET @v_statement += N' 
				AND a.[code] = @p_code';
			END
			ELSE
			BEGIN
				SET @v_statement += N' 
			   AND lower(a.[code]) LIKE concat(''%'',lower(@p_code), ''%'')';

		   END
		END  	
		
		IF @p_description IS NOT NULL
		BEGIN
			SET @v_statement += N' 
			AND lower(a.[description]) LIKE concat(''%'',lower(@p_description), ''%'')'
		END  	
		
		IF @p_is_system IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND a.[is_system] = @p_is_system';
		END  		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@p_functionality_id  = @p_functionality_id,
				@p_code				 = @p_code,
				@p_description		 = @p_description,
				@p_is_system		 = @p_is_system,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement += N' 
			ORDER BY [functionality_id], [code], [app_param_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';

			
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@p_functionality_id  = @p_functionality_id,
			@p_code				 = @p_code,
			@p_description		 = @p_description,
			@p_is_system		 = @p_is_system,
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