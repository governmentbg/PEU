


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_authentications_update]
	@p_authentication_id	INT,
	@p_user_id				INT,
	@p_authentication_type	TINYINT,
	@p_password_hash		NVARCHAR(200),
	@p_username				NVARCHAR(100),
	@p_is_locked			BIT,
	@p_locked_until			DATETIME,
	@p_certificate_id		INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_authentication_id IS NULL OR @p_user_id IS NULL OR @p_authentication_type IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_authentications_update]';
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
		DECLARE @v_curr_user_id INT, @v_countCheck int;

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SELECT @v_countCheck = COUNT(1)
			FROM users.user_authentications ua
		WHERE 
			lower(ua.username) =  lower(@p_username)
		AND ua.authentication_id !=  @p_authentication_id; 

		IF @v_countCheck > 0
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] 102;
			RETURN -1;
		END

		UPDATE users.user_authentications
		SET 
				password_hash  = @p_password_hash,
				username       = @p_username,
				certificate_id = @p_certificate_id, 
				is_locked      = @p_is_locked,
				locked_until   = @p_locked_until,
				updated_by     = @v_curr_user_id,
				updated_on     = [dbo].[f_sys_get_time]()
		 WHERE 
				authentication_id = @p_authentication_id
			AND authentication_type = @p_authentication_type;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -3, '[p_user_authentications_update]';
			RETURN -1;
		END
				
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

