


-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Създава запис документен процес.
-- =============================================
CREATE PROCEDURE [dbo].[p_document_processes_create]
	@p_document_process_id				INT OUT,
	@p_applicant_id						INT,
	@p_document_type_id					INT,
	@p_service_id						INT,
	@p_status							SMALLINT,
	@p_mode								SMALLINT,
	@p_type								SMALLINT,
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
 
	IF (@p_document_type_id IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_document_processes_create]';
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
		
		SELECT @p_document_process_id = NEXT VALUE FOR [dbo].[seq_document_processes];

		INSERT INTO [dbo].[document_processes]
           ([document_process_id]
           ,[applicant_id]
           ,[document_type_id]
           ,[service_id]
           ,[service_ver_id]
           ,[status]
           ,[mode]
           ,[type]
           ,[additional_data]
           ,[signing_guid]
           ,[error_message]
           ,[case_file_uri]
           ,[not_acknowledged_message_uri]
           ,[request_id]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
         VALUES
           (@p_document_process_id
           ,@p_applicant_id
           ,@p_document_type_id
           ,@p_service_id
		   ,dbo.f_get_n_d_services_ver_id(@p_service_id)
           ,@p_status
           ,@p_mode
           ,@p_type
           ,@p_additional_data
           ,@p_signing_guid
           ,@p_error_message
           ,@p_case_file_uri
		   ,@p_not_acknowledged_message_uri
		   ,@p_request_id
           ,@v_curr_user_id
           ,@v_Date
           ,@v_curr_user_id
           ,@v_Date);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_document_processes_create]';
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
        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int, @ErrNumber INT;
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY(), @ErrNumber = ERROR_NUMBER();

		IF @ErrNumber = 2601
		BEGIN						
			IF (SELECT CHARINDEX('uidx_document_processes_appl_svc_doctype_id', @ErrMsg)) > 0
				BEGIN
					EXEC [dbo].[p_sys_raise_dberror] 301;
					RETURN 301;
				END
			ELSE
				BEGIN
					RAISERROR(@ErrMsg, @ErrSeverity, 1)
				END
		END
		ELSE
		BEGIN
			RAISERROR(@ErrMsg, @ErrSeverity, 1)
		END;
        
		-- ===============================================================================
		
	  END CATCH;
	END;
END

GO

