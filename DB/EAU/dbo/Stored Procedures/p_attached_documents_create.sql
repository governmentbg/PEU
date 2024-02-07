


-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Създава запис за емайл съобщение.
-- =============================================
CREATE PROCEDURE [dbo].[p_attached_documents_create]
	@p_attached_document_id				INT OUT,
	@p_attached_document_guid			UNIQUEIDENTIFIER,
	@p_document_process_id				BIGINT,
	@p_document_process_content_id		BIGINT,
	@p_document_type_id					INT,
	@p_description						NVARCHAR(1000),
	@p_mime_type						NVARCHAR(100),
	@p_file_name						NVARCHAR(200),
	@p_html_template_content			NVARCHAR(max),
	@p_signing_guid						UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_attached_document_guid IS NULL   
		OR	@p_document_process_id IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_attached_documents_create]';
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
		
		SELECT @p_attached_document_id = NEXT VALUE FOR [dbo].[seq_attached_documents];

		INSERT INTO [dbo].[attached_documents]
           ([attached_document_id]
           ,[attached_document_guid]
           ,[document_process_id]
           ,[document_process_content_id]
           ,[document_type_id]
           ,[description]
           ,[mime_type]
           ,[file_name]
		   ,[html_template_content]
		   ,[signing_guid]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
        VALUES
           (@p_attached_document_id
           ,@p_attached_document_guid
           ,@p_document_process_id
           ,@p_document_process_content_id
           ,@p_document_type_id
           ,@p_description
           ,@p_mime_type
           ,@p_file_name
		   ,@p_html_template_content
		   ,@p_signing_guid
           ,@v_curr_user_id
           ,@v_Date
           ,@v_curr_user_id
           ,@v_Date);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_attached_documents_create]';
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

