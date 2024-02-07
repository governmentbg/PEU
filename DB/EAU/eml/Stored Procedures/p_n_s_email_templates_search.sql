





-- =============================================
-- Author:		Georgiev
-- Create date: 5/8/2020 3:45:05 PM
-- Description:	Връща номенклатурата за имайл шаблони.
-- =============================================
CREATE PROCEDURE [eml].[p_n_s_email_templates_search]
	@p_start_index							INT,
	@p_page_size							INT,
	@p_calculate_count						BIT,
	@p_count								INT OUT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_start_index IS NULL 
		OR @p_page_size IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[eml].[p_n_s_email_templates_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		IF(@p_calculate_count IS NOT NULL AND @p_calculate_count	 = 1)
		BEGIN
			SELECT @p_count = COUNT(*)
			FROM eml.n_s_email_templates t;
		END;

		SELECT t.*
		FROM eml.n_s_email_templates as t
		ORDER BY t.template_id
		OFFSET @p_start_index - 1 ROWS
		FETCH NEXT @p_page_size ROWS ONLY;
				
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
