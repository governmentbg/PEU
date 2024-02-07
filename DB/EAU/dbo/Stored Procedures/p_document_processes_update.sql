





-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Редакция на документен процес.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_processes_update]
	@p_document_process_id				BIGINT,
	@p_status							SMALLINT,
	@p_additional_data					NVARCHAR(max),
	@p_signing_guid						uniqueidentifier,
	@p_error_message					NVARCHAR(250),
	@p_case_file_uri					NVARCHAR(100),
	@p_not_acknowledged_message_uri		NVARCHAR(500),
	@p_request_id						NVARCHAR(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_document_process_id IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_processes_update]';
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

		UPDATE [dbo].[document_processes]
	    SET [status] = @p_status
		  ,[additional_data] = @p_additional_data
		  ,[signing_guid] = @p_signing_guid
		  ,[error_message] = @p_error_message
		  ,[case_file_uri] = @p_case_file_uri
		  ,[not_acknowledged_message_uri] = @p_not_acknowledged_message_uri
		  ,[updated_by] = @v_curr_user_id
		  ,[updated_on] = @v_Date
		  ,[request_id] = @p_request_id
	    WHERE [document_process_id] = @p_document_process_id

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_processes_update]';
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

