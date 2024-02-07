





-- =============================================
-- Author:		Georgiev
-- Create date: 5/8/2020 1:47:57 PM
-- Description:	Изчита чакащите имайл съобщения по индекс.
-- =============================================
CREATE PROCEDURE [eml].[p_email_messages_pending]
	@p_MaxFetched   	INT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (@p_MaxFetched IS NULL) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[eml].[p_email_messages_pending]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
	SELECT
	TOP(@p_MaxFetched) *
	FROM eml.email_messages e WITH(UPDLOCK READPAST)
    WHERE 
  		(e.do_not_process_before IS NULL OR 
    	e.do_not_process_before <= dbo.f_sys_get_time())
      AND	STATUS = 1
    ORDER BY e.priority DESC, e.email_id ASC
				
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
