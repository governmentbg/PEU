
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_users_create]	
	@p_email		nvarchar(200),
	@p_status		smallint,
	@p_user_id		int out,
	@p_user_ver_id	bigint out,
	@p_cin			int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_email IS NULL OR @p_email = '' ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_users_create]';
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

		IF EXISTS (
			SELECT 1 FROM [users].[users]
			WHERE LOWER([email]) = LOWER(@p_email)
		)
		BEGIN
		  EXEC [dbo].[p_sys_raise_dberror] 101;
		  RETURN -1;
		END;

		DECLARE @v_user_ver_id BIGINT, @v_curr_user_id INT, @v_date DATETIMEOFFSET(3);

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();

		SELECT @p_user_id = NEXT VALUE FOR [users].[seq_users];
		SELECT @p_cin = NEXT VALUE FOR [users].[seq_users_cin];

		INSERT INTO [users].[users] (
			[user_id], 
			[cin],
			[email], 
			[status], 
			[is_system], 
			[created_by], 
			[created_on], 
			[updated_by], 
			[updated_on]
		)
		VALUES (
			@p_user_id,
			@p_cin,
			@p_email,
			@p_status,
			0,
			@v_curr_user_id,
			@v_date,
			@v_curr_user_id,
			@v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -3, '[p_users_create]';
			RETURN -1;
		END

		EXEC [users].[p_users_h_create] 
			@p_user_id		= @p_user_id, 
			@p_user_ver_id	= @v_user_ver_id OUTPUT,
			@p_operation_id	= 1	-- create user 
							
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

