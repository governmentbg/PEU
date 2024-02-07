

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_users_update]
	@p_user_id		INT,	
	@p_user_ver_id	BIGINT OUT,
	@p_email		NVARCHAR(200),
	@p_status		SMALLINT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_user_id IS NULL OR @p_email IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_users_update]';
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
		DECLARE @v_user_ver_id bigint, @v_curr_user_id INT, @v_countCheck int;

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SELECT @v_countCheck = COUNT(1)
		FROM users.users u
		WHERE lower(u.email) =  lower(@p_email)
			AND u.user_id != @p_user_id; 

		IF @v_countCheck > 0
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] 101;
			RETURN -1;
		END

		UPDATE users.users
		SET 
			[email]			= @p_email,
			[status]		= @p_status,
			[updated_by]	= @v_curr_user_id,
			[updated_on]	= [dbo].[f_sys_get_time]()
		WHERE 
			[user_id]		= @p_user_id;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -3, '[p_users_update]';
			RETURN -1;
		END

		EXEC [users].[p_users_h_create] 
			@p_user_id		= @p_user_id, 
			@p_user_ver_id	= @v_user_ver_id,
			@p_operation_id	= 2	-- update
					
		SET @p_user_ver_id = @v_user_ver_id;
		
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

