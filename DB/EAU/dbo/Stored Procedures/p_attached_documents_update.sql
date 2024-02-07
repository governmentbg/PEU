

-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Редакция на мета данни за прикачен документ.
-- =============================================
CREATE PROCEDURE [dbo].[p_attached_documents_update]
	@p_attached_document_id			BIGINT,
	@p_description					NVARCHAR(1000),
	@p_mime_type					NVARCHAR(100),
	@p_file_name					NVARCHAR(200),
	@p_document_process_content_id	BIGINT,
	@p_html_template_content		NVARCHAR(max),
	@p_signing_guid					UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_attached_document_id IS NULL 
		OR (@p_signing_guid IS NOT NULL AND (@p_html_template_content IS NULL OR @p_html_template_content = N''))
		) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_attached_documents_update]';
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
		DECLARE @v_curr_user_id INT, @v_Date DATETIMEOFFSET(3);

		SET @v_Date = [dbo].[f_sys_get_time]();
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		UPDATE [dbo].[attached_documents]
		SET [description] = @p_description
			,[mime_type] = @p_mime_type
			,[file_name] = @p_file_name
			,[document_process_content_id] = @p_document_process_content_id
			,[html_template_content] = @p_html_template_content
			,[signing_guid] = @p_signing_guid
			,[updated_by] = @v_curr_user_id
			,[updated_on] = @v_Date
	    WHERE 
			[attached_document_id] = @p_attached_document_id;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_attached_documents_update]';
			RETURN -1;
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

