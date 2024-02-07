

-- =============================================
-- Author:		Georgiev
-- Create date: 5/11/2020 6:12:47 PM
-- Description:	Качва съдържание за документен процес/прикачен файл.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_process_contents_upload]
	@p_document_process_content_id	BIGINT,
	@p_content						VARBINARY(MAX)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_document_process_content_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_process_contents_upload]';
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
		DECLARE @v_curr_user_id INT, @v_curr_content_length INT, @v_content_length_after_update INT;
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SELECT @v_curr_content_length = DATALENGTH(content) 
		FROM  [dbo].[document_process_contents]
		WHERE [document_process_content_id] = @p_document_process_content_id;

		IF @v_curr_content_length IS NULL OR @p_content IS NULL
		BEGIN
			UPDATE [dbo].[document_process_contents]
			SET 
				content		= @p_content,
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[document_process_content_id] = @p_document_process_content_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_process_contents_upload]';
				RETURN -1;
			END;
		END;

		IF @v_curr_content_length IS NOT NULL AND @v_curr_content_length > 0 AND @p_content IS NOT NULL
		BEGIN
			UPDATE [dbo].[document_process_contents]
			SET 
				content.WRITE(@p_content, null, null),
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[document_process_content_id] = @p_document_process_content_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_process_contents_upload]';
				RETURN -1;
			END;
		END;

		SELECT @v_content_length_after_update = DATALENGTH(content) 
		FROM  [dbo].[document_process_contents]
		WHERE [document_process_content_id] = @p_document_process_content_id;

		IF @v_content_length_after_update is null
		BEGIN
			SELECT @v_content_length_after_update = 0;
		END

		BEGIN
			UPDATE [dbo].[document_process_contents]
			SET 
				length = @v_content_length_after_update
			WHERE
				[document_process_content_id] = @p_document_process_content_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_process_contents_upload]';
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

