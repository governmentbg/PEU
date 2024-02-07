

-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Създава запис за съдържание на документен процес/прикачен файл към док. процес.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_process_contents_create]
	@p_document_process_content_id	BIGINT OUT,
	@p_document_process_id			BIGINT,
	@p_type							SMALLINT,
	@p_text_content					NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_document_process_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_process_contents_create]';
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

		SELECT @p_document_process_content_id = NEXT VALUE FOR [dbo].[seq_document_process_contents];

		INSERT INTO [dbo].[document_process_contents]
           ([document_process_content_id]
           ,[document_process_id]
           ,[type]
           ,[content]
		   ,[text_content]
		   ,[length]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
		VALUES (
			@p_document_process_content_id
			, @p_document_process_id
			, @p_type
			, NULL
			, @p_text_content
			, case when @p_text_content is NULL then 0 else LEN(@p_text_content) end 
			, @v_curr_user_id
			, @v_date
			, @v_curr_user_id
			, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_process_contents_create]';
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