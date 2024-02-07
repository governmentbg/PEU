



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_languages_search]
	@p_language_id		INT,
	@p_code				NVARCHAR(50),
	@p_name				NVARCHAR(100),
	@p_is_active		BIT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT,
	@p_last_updated_on	datetimeoffset(3) OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_languages_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		SELECT @p_last_updated_on = last_updated_on 
		FROM nom.nomenclature_changes
		WHERE tablename = '[nom].[n_d_languages]';
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
		@v_language_id		INT,
		@v_code				NVARCHAR(50),
		@v_name				NVARCHAR(100),
		@v_is_active		BIT,
		@p_start_index		INT,
		@p_page_size		INT,
		@p_count			INT OUT';

		

		SET @v_statement = N'
		SELECT [language_id]
			  ,[code]
			  ,[name]
			  ,[is_active]
			  ,[is_default]
		  FROM [nom].[n_d_languages]
		 WHERE 1 = 1';

		IF @p_language_id IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND [language_id] = @v_language_id
		';
		END  	
		----------------------------------------------------------------
		IF @p_code IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND [code] = @v_code
		';
		END  	
		----------------------------------------------------------------
		IF @p_name IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND [name] = @v_name
		';
		END  	
		----------------------------------------------------------------
		IF @p_is_active IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND [is_active] = @v_is_active
		';
		END  	
		----------------------------------------------------------------
		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@v_language_id		 = @p_language_id,
				@v_code				 = @p_code,
				@v_name				 = @p_name,
				@v_is_active		 = @p_is_active,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement = @v_statement + N' 
			ORDER BY [code]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';

			
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_language_id		 = @p_language_id,
			@v_code				 = @p_code,
			@v_name				 = @p_name,
			@v_is_active		 = @p_is_active,
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