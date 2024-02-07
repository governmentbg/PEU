


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_user_processes_create]
	@p_user_id				INT,
	@p_process_guid			UNIQUEIDENTIFIER,
	@p_process_type			TINYINT,
	@p_invalid_after		DATETIME,
	@p_process_id			INT OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_user_id IS NULL OR @p_process_guid IS NULL OR @p_process_type IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_processes_create]';
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

		-- деактивиране на неприключили процеси за потребителя от същия тип
		UPDATE [users].[user_processes]
		SET status     = 3, -- Отказан
			updated_by = @v_curr_user_id,
			updated_on = @v_Date
		WHERE 
			user_id = @p_user_id
		AND process_type = @p_process_type
		AND status = 0;

		SELECT @p_process_id = NEXT VALUE FOR [users].[seq_user_processes];

		INSERT INTO [users].[user_processes] (
			[process_id], [process_guid], [process_type], [user_id], [invalid_after], [status], 
			[created_by], [created_on], [updated_by], [updated_on]
		)
		VALUES (
			@p_process_id, @p_process_guid, @p_process_type, @p_user_id, @p_invalid_after, 1, -- Неприключил
			@v_curr_user_id, @v_date, @v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_user_processes_create]';
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

