
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_users_h_create]
	@p_user_id		INT,
	@p_user_ver_id	BIGINT OUT,
	@p_operation_id	SMALLINT	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_user_id IS NULL OR @p_operation_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_users_h_create]';
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
		DECLARE @v_user_ver_id BIGINT, @v_curr_user_id INT, @v_date DATETIMEOFFSET(3);
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_user_ver_id OUTPUT;

		IF ( @p_operation_id = 2 )
		BEGIN
			UPDATE users.users_h
			SET 
			  is_last = 0,
			  deactivation_ver_id = @v_user_ver_id,
			  updated_by = @v_curr_user_id,
			  updated_on = @v_date
			WHERE user_id = @p_user_id
			  AND is_last = 1					-- последната версия
			  AND deactivation_ver_id IS NULL;	-- последната версия не е деактивирана
		END;

		INSERT INTO [users].[users_h] (
			[user_id], 
			[user_ver_id], 
			[cin],
			[email], 
			[status], 
			[is_system], 
			[is_last], 
			[deactivation_ver_id], 
			[created_by], 
			[created_on], 
			[updated_by], 
			[updated_on]
		)
		SELECT
			u.user_id,
			@v_user_ver_id,
			u.cin,
			u.email,
			u.status,
			u.is_system,
			1,
			NULL,
			@v_curr_user_id,
			@v_date,
			@v_curr_user_id,
			@v_date
		FROM users.users u
		WHERE u.user_id = @p_user_id;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_users_h_create]';
			RETURN -1;
		END
		
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

