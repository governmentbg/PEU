

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [audit].[p_log_actions_create]
	@p_log_action_id		BIGINT OUT,
	@p_log_action_date		DATETIME,
	@p_object_type_id		TINYINT,
	@p_action_type_id		TINYINT,
	@p_functionality_id		TINYINT,
	@p_login_session_id		UNIQUEIDENTIFIER,	
	@p_key					NVARCHAR(100),
	@p_user_id				INT,
	@p_user_email			NVARCHAR(100),
	@p_ip_address			VARBINARY(16),
	@p_additional_data		NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_log_action_date IS NULL OR @p_object_type_id IS NULL OR @p_action_type_id IS NULL OR
		 @p_functionality_id IS NULL OR @p_login_session_id IS NULL OR 
		 ( @p_user_id IS NULL AND @p_user_email IS NULL ) OR
		 @p_ip_address IS NULL  ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_log_actions_create]';
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
		DECLARE  @v_user_id int, @v_curr_user_id INT, @v_date DATETIMEOFFSET(3);

		SET @v_user_id = COALESCE(@p_user_id, (SELECT [user_id] FROM users.[users] WHERE email = @p_user_email));

		IF @v_user_id IS NULL
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_log_actions_create]';
			RETURN -1;
		END

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();		
		SELECT @p_log_action_id = NEXT VALUE FOR [audit].[seq_log_actions];

		INSERT INTO [audit].[log_actions] (
			[log_action_id], [log_action_date], [object_type_id], [action_type_id], [functionality_id], [key], 
			[login_session_id], [user_id], [ip_address], [additional_data], 
			[created_by], [created_on]
		)
		VALUES (
			@p_log_action_id, @p_log_action_date, @p_object_type_id, @p_action_type_id, @p_functionality_id, @p_key, 
			@p_login_session_id, @v_user_id, @p_ip_address, @p_additional_data, 			
			@v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_log_actions_create]';
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

