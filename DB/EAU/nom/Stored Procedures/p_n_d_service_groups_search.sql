



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_service_groups_search]
	@p_ids						NVARCHAR(MAX),
	@p_language_id				INT,
	@p_force_translated			BIT,
	@p_start_index				INT,
	@p_page_size				INT,	
	@p_calculate_count			BIT,
	@p_count					INT OUT,
	@p_last_updated_on			DATETIMEOFFSET(3) OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_service_groups_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[nom].[n_d_service_groups]' OR tablename = '[nom].[n_d_service_groups_i18n]';
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		tt_integers;

		IF @p_ids IS NOT NULL AND rtrim(ltrim(@p_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_ids, ',');
		END

		SET @v_Params = N'
		@v_ids_table		TT_INTEGERS READONLY,
		@p_language_id		INT,
		@p_force_translated BIT,
		@p_start_index		INT,
		@p_page_size		INT,
		@p_count			INT OUTPUT';

		SET @v_statement = N'
		SELECT g.[group_id]
			  ,(CASE WHEN (gi18n.group_id IS NULL) 
					 THEN 0 
					 ELSE 1 
				END) as is_translated
			  ,(CASE WHEN (@p_force_translated = 1) 
					 THEN gi18n.name 
					 ELSE dbo.f_sys_search_coalesce_i18n(gi18n.name, g.name, NULL) 
				END) as name
			  ,g.is_active
			  ,g.order_number
			  ,g.icon_name
			  ,(CASE WHEN (lang.code IS NOT NULL) 
					 THEN lang.code 
					 ELSE (SELECT code FROM nom.n_d_languages WHERE is_default = 1 ) 
				END) as language_code
			  ,g.updated_on
		  FROM [nom].[n_d_service_groups] g
		  LEFT OUTER JOIN [nom].[n_d_service_groups_i18n] gi18n 
		  LEFT OUTER JOIN nom.n_d_languages lang ON lang.language_id = gi18n.language_id 
		    ON gi18n.group_id = g.group_id
		   AND gi18n.[language_id] = @p_language_id
		   AND gi18n.[is_last] = 1
		   AND gi18n.[deactivation_ver_id] IS NULL
		 WHERE g.[is_last] = 1 
		   AND g.[deactivation_ver_id] IS NULL';

		IF @p_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND g.[group_id] in (SELECT * FROM @v_ids_table)
		';
		END  		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@v_ids_table		 = @v_ids_table,
				@p_language_id		 = @p_language_id,
				@p_force_translated	 = @p_force_translated,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement = @v_statement + N' 
			ORDER BY [order_number]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_ids_table		 = @v_ids_table,
			@p_language_id		 = @p_language_id,
			@p_force_translated	 = @p_force_translated,
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