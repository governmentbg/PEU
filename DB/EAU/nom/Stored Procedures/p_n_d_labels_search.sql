



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_labels_search]
	@p_label_ids				NVARCHAR(MAX),
	@p_language_id				INT,
	@p_code						NVARCHAR(200),
	@p_value					NVARCHAR(MAX),
	@p_load_description			BIT,
	@p_load_only_untranslated	BIT,
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
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_labels_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[nom].[n_d_labels]' OR tablename = '[nom].[n_d_labels_i18n]';
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_label_ids_table	tt_integers;

		IF @p_label_ids IS NOT NULL AND rtrim(ltrim(@p_label_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_label_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_label_ids, ',');
		END

		SET @v_Params = N'
		@v_label_ids_table			tt_integers READONLY,
		@p_language_id				INT,
		@p_code						NVARCHAR(200),
		@p_value					NVARCHAR(MAX),
		@p_load_description			BIT,
		@p_load_only_untranslated	BIT,
		@p_force_translated			BIT,
		@p_start_index				INT,
		@p_page_size				INT,
		@p_count					INT OUT';

		SET @v_statement = N'
		SELECT l.[label_id]
			  ,(CASE WHEN li18n.label_id IS NULL THEN 0 ELSE 1 END) as is_translated
			  ,l.[code]
			  ,(CASE WHEN (@p_load_description = 1) 
					 THEN l.[description] 
				END) as description
			  ,(CASE WHEN (@p_force_translated = 1) 
					 THEN li18n.value 
					 ELSE dbo.f_sys_search_coalesce_i18n(li18n.value, l.value, NULL) 
				END) as value
			  ,(CASE WHEN (lang.code IS NOT NULL) 
					 THEN lang.code 
					 ELSE (SELECT code FROM nom.n_d_languages WHERE is_default = 1 ) 
				END) as language_code
		  FROM [nom].[n_d_labels] l
		  LEFT OUTER JOIN [nom].[n_d_labels_i18n] li18n 
		  LEFT OUTER JOIN nom.n_d_languages lang ON lang.language_id = li18n.language_id
		    ON li18n.label_id = l.label_id
		   AND li18n.[language_id] = @p_language_id
		   AND li18n.[is_last] = 1
		   AND li18n.[deactivation_ver_id] IS NULL
		 WHERE l.[is_last] = 1 
		   AND l.[deactivation_ver_id] IS NULL';

		IF @p_label_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' 
		   AND l.[label_id] in (SELECT * FROM @v_label_ids_table)';
		END  	
		
		IF @p_code IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND lower(l.code) LIKE concat(''%'',lower(@p_code), ''%'')';
		END
  
		IF @p_load_only_untranslated = 1
		BEGIN
			SET @v_statement += N' 
		   AND NOT EXISTS(SELECT i18n.label_id 
		                    FROM nom.n_d_labels_i18n i18n 
					       WHERE i18n.label_id = l.label_id and i18n.value != ''''  
					         AND i18n.language_id = @p_language_id)';
		END
  
	    IF @p_value IS NOT NULL
	    BEGIN
			/*първо изпълняваме QUERY-то както е до момента и после филтрираме по VALUE, 
			защото в някои случаи във value са merge-нати стойности от "default"-ия език и друг
			и трябва да правим проверки, за да знаем дали да филтрираме LABEL.value или label_i18n.value*/
			SET @v_statement = N' SELECT * 
								    FROM (' + @v_statement + ') vt 
								     WHERE LOWER(vt.value) LIKE CONCAT(''%'',LOWER(@p_value), ''%'')';
	    END	

		PRINT @v_statement

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
			@params						= @v_Params,
			@v_label_ids_table			= @v_label_ids_table,
			@p_language_id				= @p_language_id,
			@p_code						= @p_code,				
			@p_value					= @p_value,
			@p_load_description			= @p_load_description,
			@p_load_only_untranslated	= @p_load_only_untranslated,
			@p_force_translated			= @p_force_translated,
			@p_start_index				= @p_start_index,	
			@p_page_size				= @p_page_size,			
			@p_count					= @p_count output;
		END

		SET @v_statement = @v_statement + N' 
			ORDER BY [value]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';
		
		EXEC sp_executeSQL @v_statement,
			@params						= @v_Params,				
			@v_label_ids_table			= @v_label_ids_table,
			@p_language_id				= @p_language_id,
			@p_code						= @p_code,				
			@p_value					= @p_value,
			@p_load_description			= @p_load_description,
			@p_load_only_untranslated	= @p_load_only_untranslated,
			@p_force_translated			= @p_force_translated,
			@p_start_index				= @p_start_index,	
			@p_page_size				= @p_page_size,			
			@p_count					= @p_count output;
				
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