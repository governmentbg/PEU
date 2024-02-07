

-- =============================================
-- Author:		Georgiev
-- Create date: 5/11/2020 6:12:47 PM
-- Description:	Качва съдържанието за подписване на процес.
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_content_upload]
	@p_process_id	UNIQUEIDENTIFIER,
	@p_content		VARBINARY(MAX)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_process_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_content_upload]';
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
		DECLARE @v_curr_user_id INT, @v_curr_content_length INT;
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SELECT @v_curr_content_length = DATALENGTH(content) 
		FROM  [sign].[signing_processes]
		WHERE [process_id] = @p_process_id;

		IF @v_curr_content_length IS NULL OR @p_content IS NULL
		BEGIN
			UPDATE [sign].[signing_processes]
			SET 
				content		= @p_content,
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[process_id] = @p_process_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signing_process_content_upload]';
				RETURN -1;
			END;
		END;

		IF @v_curr_content_length IS NOT NULL AND @v_curr_content_length > 0 AND @p_content IS NOT NULL
		BEGIN
			UPDATE [sign].[signing_processes]
			SET 
				content.WRITE(@p_content, null, null),
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[process_id] = @p_process_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signing_process_content_upload]';
				RETURN -1;
			END;
		END;
											
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

		-- ?????
		--IF XACT_STATE() = -1 AND @v_HasOuterTransaction = 0
		--BEGIN
		--  ROLLBACK TRANSACTION @v_RollbackPoint;
		--END;

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