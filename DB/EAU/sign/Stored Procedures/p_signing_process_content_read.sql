


-- =============================================
-- Author:		Georgiev
-- Create date: 15.06.2020
-- Description:	Изчита съдържанието за подписване от процеса.
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_content_read]
	@p_process_id    UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF (@p_process_id IS NULL) -- PARAMETER ERROR
	BEGIN
		EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_content_read]';
		RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  
	  -- ===============================================================================			
	  
	  SELECT [content]	
		FROM  [sign].[signing_processes]
		WHERE  [process_id] = @p_process_id
		
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