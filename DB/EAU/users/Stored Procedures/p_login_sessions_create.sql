
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_login_sessions_create]
	@p_id					BIGINT OUT,
	@p_login_session_id		UNIQUEIDENTIFIER,
	@p_user_session_id		UNIQUEIDENTIFIER,
	@p_user_id				INT,
	@p_login_date			DATETIMEOFFSET(3),
	@p_logout_date			DATETIMEOFFSET(3),
	@p_ip_address			VARBINARY(16),
	@p_authentication_type	TINYINT,
	@p_certificate_id		INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_login_session_id IS NULL OR @p_user_session_id IS NULL OR @p_user_id IS NULL OR @p_authentication_type IS NULL OR @p_login_date IS NULL 
		OR @p_ip_address IS NULL OR @p_authentication_type IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_login_sessions_create]';
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

		INSERT INTO [users].[login_sessions] (
			[login_session_id], [user_session_id], [user_id], 
			[login_date], [logout_date], [ip_address], [authentication_type], [certificate_id], 
			[created_by], [created_on], [updated_by], [updated_on]
		)
		VALUES (
			@p_login_session_id, @p_user_session_id, @p_user_id,
			@p_login_date, @p_logout_date, @p_ip_address, @p_authentication_type, @p_certificate_id,
			@v_curr_user_id, @v_date, @v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_login_sessions_create]';
			RETURN -1;
		END;

		SET @p_id = @@IDENTITY;
		
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

