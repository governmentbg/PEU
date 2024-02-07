



-- =============================================
-- Author:		Georgiev
-- Create date: 08.05.2020
-- Description:	Създава запис за емайл съобщение.
-- =============================================
CREATE PROCEDURE [eml].[p_email_messages_create]
	@p_email_id					INT OUT,
	@p_priority					SMALLINT,
	@p_try_count				INT,
	@p_subject					NVARCHAR(500),
	@p_body						NVARCHAR(max),
	@p_is_body_html				BIT,
	@p_sending_provider_name	NVARCHAR(20),
	@p_recipients				NVARCHAR(max)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_priority IS NULL  
		OR	@p_subject IS NULL 
		OR	@p_body IS NULL  
		OR	@p_is_body_html IS NULL  
		OR	@p_sending_provider_name IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_templates_create]';
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
		
		SELECT @p_email_id = NEXT VALUE FOR [eml].[seq_email_messages];

		INSERT INTO [eml].[email_messages]
           ([email_id]
           ,[priority]
           ,[status]
           ,[try_count]
           ,[send_date]
           ,[subject]
           ,[body]
           ,[is_body_html]
           ,[do_not_process_before]
           ,[sending_provider_name]
           ,[recipients]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
		VALUES
           (@p_email_id
           ,@p_priority
           ,1 --Pendiing
           ,@p_try_count
           ,NULL
           ,@p_subject
           ,@p_body
           ,@p_is_body_html
           ,@v_Date
           ,@p_sending_provider_name
           ,@p_recipients
           ,@v_curr_user_id
           ,@v_Date
           ,@v_curr_user_id
           ,@v_Date);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[eml].[p_email_messages_create]';
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

