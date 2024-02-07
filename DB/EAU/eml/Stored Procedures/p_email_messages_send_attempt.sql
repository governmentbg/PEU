




-- =============================================
-- Author:		Georgiev
-- Create date: 08.05.2020
-- Description:	Обновява статуса на емайл съобщение.
-- =============================================
CREATE PROCEDURE [eml].[p_email_messages_send_attempt]
	@p_email_id			INT,
	@p_is_send			BIT,
	@p_is_failed_out	BIT  OUT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_email_id IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[eml].[p_email_messages_send_attempt]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY

		DECLARE @v_HasOuterTransaction BIT = CASE WHEN @@TRANCOUNT > 0 THEN 1 ELSE 0 END;
		DECLARE @v_RollbackPoint NCHAR(32);
	   
		IF @v_HasOuterTransaction = 0
		BEGIN
		  SET @v_RollbackPoint = REPLACE(CONVERT(NCHAR(36), NEWID()), N'-', N'');
		  BEGIN TRANSACTION @v_RollbackPoint;
		END;
		
		-- ===============================================================================		
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3);
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();
		
		UPDATE eml.email_messages 
		SET
			status				= CASE WHEN @p_is_send = 1 THEN 2 
									   WHEN @p_is_send = 0 AND try_count = 1 THEN 3 
									   ELSE 1 END,
			try_count				= CASE WHEN @p_is_send = 1 THEN try_count ELSE try_count - 1 END,
            send_date				= CASE WHEN @p_is_send = 1 THEN @v_Date ELSE NULL END,
            do_not_process_before	= CASE WHEN @p_is_send = 1 THEN do_not_process_before ELSE DATEADD(minute, 10, @v_Date) END,
			UPDATED_BY = @v_curr_user_id,
			UPDATED_ON = @v_Date
		WHERE 
			email_id = @p_email_id
		AND status = 1;

		SELECT @p_is_failed_out = CONVERT(BIT, (CASE WHEN STATUS = 3 THEN 1 ELSE 0 END)) 
		FROM eml.email_messages
		WHERE email_id = @p_email_id;
		
		-- ===============================================================================			
		
		IF @v_HasOuterTransaction = 0
		BEGIN
		  COMMIT TRANSACTION;
		END;
	  END TRY
	  BEGIN CATCH
		IF XACT_STATE() = 1 AND @v_HasOuterTransaction = 0
		BEGIN
		  ROLLBACK TRANSACTION @v_RollbackPoint;
		END;

		-- ===============================================================================
		-- STANDARD ERROR HANDLING MODULE;
		
		-- Raise an error with the details of the exception
        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

		-- ===============================================================================
		
	  END CATCH;
	END;
END

