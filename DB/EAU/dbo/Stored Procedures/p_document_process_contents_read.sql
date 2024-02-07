


-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Изчита съдържанието на документен процес/прикачен файл.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_process_contents_read]
	@p_document_process_content_id    bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (@p_document_process_content_id IS NULL) -- PARAMETER ERROR
	BEGIN
		EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_process_contents_read]';
		RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  
	  -- ===============================================================================			
	  
	  SELECT [content]	
		FROM  [dbo].[document_process_contents]
		WHERE  [document_process_content_id] = @p_document_process_content_id
		
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

