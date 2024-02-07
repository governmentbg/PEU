



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_services_search]
	@p_ids				NVARCHAR(MAX),
	@p_language_id		INT,
	@p_force_translated	BIT,
	@p_start_index		INT,
	@p_page_size		INT,
	@p_calculate_count	BIT,
	@p_count			INT OUT,
	@p_last_updated_on	DATETIMEOFFSET(3) OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF ( @p_page_size IS NULL AND @p_start_index  IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_services_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[nom].[n_d_services]' OR tablename = '[nom].[n_d_services_i18n]'
		   OR tablename = '[nom].[n_d_service_declarations]' OR tablename = '[nom].[n_d_service_delivery_channels]'	
		   OR tablename = '[nom].[n_d_service_document_types]' OR tablename = '[nom].[n_d_service_terms]'	
		   OR tablename = '[nom].[n_d_declarations]' OR tablename = '[nom].[n_s_delivery_channels]'	
		   OR tablename = '[nom].[n_s_document_types]';
		
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
			SELECT s.[service_id]
				  ,(CASE WHEN si18n.service_id IS NULL THEN 0 ELSE 1 END) as is_translated
				  ,[group_id]
				  ,(CASE WHEN (@p_force_translated = 1) 
				         THEN si18n.name 
						 ELSE dbo.f_sys_search_coalesce_i18n(si18n.name, s.name, NULL) 
					END) as name
				  ,[doc_type_id]
				  ,[sunau_service_uri]
				  ,[initiation_type_id]
				  ,[result_document_name]
				  ,(CASE WHEN (@p_force_translated = 1) 
						 THEN si18n.description
						 ELSE dbo.f_sys_search_coalesce_i18n(si18n.description, s.description, NULL) 
					END) as description
				  ,[explanatory_text_service]
				  ,[explanatory_text_fulfilled_service]
				  ,[explanatory_text_refused_or_terminated_service]
				  ,[order_number]
				  ,[adm_structure_unit_name]
				  ,(CASE WHEN (@p_force_translated = 1) 
						 THEN si18n.attached_documents_description
						 ELSE dbo.f_sys_search_coalesce_i18n(si18n.attached_documents_description, s.attached_documents_description, NULL) 
					END)	 as attached_documents_description	  
				  ,[additional_configuration]
				  ,(CASE WHEN (lang.code IS NOT NULL) THEN lang.code ELSE (SELECT code FROM nom.n_d_languages WHERE is_default = 1 ) END) as language_code
				  ,s.[service_url]
				  ,s.is_active
				  ,s.updated_on
			  FROM [nom].[n_d_services] s
	LEFT OUTER JOIN [nom].[n_d_services_i18n] si18n
	LEFT OUTER JOIN nom.n_d_languages lang ON lang.language_id = si18n.language_id 
				 ON si18n.service_id = s.service_id
			    AND si18n.[language_id] = @p_language_id
			    AND si18n.[is_last] = 1
			    AND si18n.[deactivation_ver_id] IS NULL
			  WHERE s.[is_last] = 1 
			    AND s.[deactivation_ver_id] IS NULL';

		IF @p_ids IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND s.[service_id] in (SELECT * FROM @v_ids_table)';
		END  		

		

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			PRINT @v_statement_count
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@v_ids_table		 = @v_ids_table,
				@p_language_id		 = @p_language_id,
				@p_force_translated	 = @p_force_translated,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement += N' 
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