

-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Редакция на запис в таблицата [sign].[signing_processes].
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_update]
	@p_process_id		UNIQUEIDENTIFIER,
	@p_status			SMALLINT,
	@p_additional_data	NVARCHAR(max)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_process_id IS NULL OR @p_status IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_update]';
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
		DECLARE @v_curr_user_id INT, @v_Date DATETIMEOFFSET(3);

		SET @v_Date = [dbo].[f_sys_get_time]();
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		UPDATE [sign].[signing_processes]
	    SET [status] = @p_status
		  , [additional_data] = @p_additional_data
		  , [updated_by] = @v_curr_user_id
		  , [updated_on] = @v_Date
	    WHERE [process_id] = @p_process_id;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signing_process_update]';
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