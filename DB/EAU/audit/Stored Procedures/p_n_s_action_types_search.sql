





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [audit].[p_n_s_action_types_search]
	@p_action_type_id   INT,
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
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[audit].[p_n_s_action_types_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================

		SELECT @p_last_updated_on = MAX(last_updated_on)
		FROM nom.nomenclature_changes
		WHERE tablename = '[audit].[n_s_action_types]';
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
		@p_action_type_id   INT,
		@p_start_index		INT,
		@p_page_size		INT,
		@p_count			INT OUTPUT';

		SET @v_statement = N'
			SELECT a.[action_type_id]
				  ,a.[name]
				  ,a.[description]
			  FROM [audit].[n_s_action_types] a
			 WHERE 1 = 1 ';

		IF @p_action_type_id IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND a.[action_type_id] = @p_action_type_id';
		END  	

		IF @p_calculate_count = 1
		BEGIN
			
			SET @v_statement_count = N'
			SELECT @p_count = COUNT(*) FROM ( ' + @v_statement + ' ) as t';
			
			EXEC sp_executeSQL @v_statement_count,
				@params				 = @v_Params,				
				@p_action_type_id    = @p_action_type_id,
				@p_start_index		 = @p_start_index,
				@p_page_size		 = @p_page_size,
				@p_count			 = @p_count output;				
		END

		SET @v_statement += N' 
			ORDER BY [name], [action_type_id]
			OFFSET @p_start_index - 1 ROWS
			FETCH NEXT @p_page_size ROWS ONLY';

			
		
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@p_action_type_id	 = @p_action_type_id,
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