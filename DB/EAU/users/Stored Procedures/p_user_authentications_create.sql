

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_authentications_create]
	@p_authentication_id	INT OUT,
	@p_user_id				INT,
	@p_authentication_type	TINYINT,
	@p_password_hash		NVARCHAR(200),
	@p_username				NVARCHAR(100),
	@p_certificate_id		INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_user_id IS NULL OR @p_authentication_type IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_authentications_create]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_countCheck int;

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();

		SELECT @v_countCheck = COUNT(1)
		FROM users.user_authentications ua
		WHERE 
   			lower(ua.username) =  lower(@p_username)
  		AND authentication_type = @p_authentication_type
		AND is_active = 1; 

		IF @v_countCheck > 0
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] 102;
			RETURN -1;
		END

		SELECT @p_authentication_id = NEXT VALUE FOR [users].[seq_user_authentications];

		INSERT INTO [users].[user_authentications] (
			[authentication_id], [user_id], 
			[authentication_type], [password_hash], [username], [certificate_id], 
			[is_locked], [locked_until], [is_active], 
			[created_by], [created_on], [updated_by], [updated_on]
		)
		VALUES (
			@p_authentication_id, @p_user_id,
			@p_authentication_type, @p_password_hash, @p_username, @p_certificate_id,
			0, null, 1,			
			@v_curr_user_id, @v_date, @v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -3, '[p_user_authentications_create]';
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

