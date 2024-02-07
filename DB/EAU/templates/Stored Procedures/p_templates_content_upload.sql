



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [templates].[p_templates_content_upload]
	@p_template_content_id	INT,
	@p_content				VARBINARY(MAX),
	@p_offset		 		INT,
	@p_length		 		INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_template_content_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_templates_content_create]';
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
		FROM [templates].[templates_content] 
		WHERE [template_content_id] = @p_template_content_id;

		IF @v_curr_content_length IS NULL
		BEGIN
			UPDATE [templates].[templates_content]
			SET 
				content		= @p_content,
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[template_content_id] = @p_template_content_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[p_templates_content_create]';
				RETURN -1;
			END;
		END;

		IF @v_curr_content_length IS NOT NULL AND @v_curr_content_length > 0
		BEGIN
			UPDATE [templates].[templates_content]
			SET 
				content.WRITE(@p_content, @p_offset, @p_length),
				updated_by	= @v_curr_user_id,
				updated_on	= [dbo].[f_sys_get_time]()
			WHERE
				[template_content_id] = @p_template_content_id;

			IF @@ROWCOUNT <> 1
			BEGIN
				EXEC [dbo].[p_sys_raise_dberror] -16, '[p_templates_content_create]';
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

