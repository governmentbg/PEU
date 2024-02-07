





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_app_parameters_update]
	@p_code					NVARCHAR(100),
	@p_value_date_time		DATETIMEOFFSET(3),
	@p_value_interval		DATETIME,
	@p_value_string			NVARCHAR(2000),
	@p_value_int			INT,
	@p_value_hour			TIME(7)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_code IS NULL 
	OR (@p_value_date_time IS NULL 
		AND @p_value_hour IS NULL 
		AND @p_value_int IS NULL
		AND @p_value_interval IS NULL
		AND @p_value_string IS NULL)
	) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_app_parameters_update]';
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
		DECLARE @v_curr_user_id INT, @v_Date DATETIMEOFFSET(3), @v_app_param_ver_id BIGINT;

		SET @v_Date = [dbo].[f_sys_get_time]();
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_app_param_ver_id OUTPUT;

		-- деактивиране на съществуващата последна версия
		UPDATE [dbo].[app_parameters]
		SET
			[is_last]	= 0,
			[deactivation_ver_id]	= @v_app_param_ver_id,
			[updated_by]= @v_curr_user_id, 
			[updated_on]= @v_Date
		WHERE	
			[code] = @p_code
		AND [is_last] = 1
		AND [deactivation_ver_id] IS NULL;

		-- създаване на нова версия
		INSERT INTO [dbo].[app_parameters] (
				  [app_param_id],
				  [app_param_ver_id],
				  [functionality_id],
				  [code],
				  [description],
				  [is_system],
				  [param_type],
				  [value_datetime],
				  [value_interval],
				  [value_string],
				  [value_int],
				  [value_hour],
				  [is_last],
				  [deactivation_ver_id],
				  [created_by],
				  [created_on],
				  [updated_by],
				  [updated_on]
		)
		SELECT 
				[app_param_id], 
				@v_app_param_ver_id, 
				[functionality_id],
				[code],
				[description],
				[is_system],
				[param_type],
				@p_value_date_time, 
				@p_value_interval,
				@p_value_string,
				@p_value_int,
				@p_value_hour,
				1, 
				null,
				@v_curr_user_id,
				@v_date,
				@v_curr_user_id,
				@v_date
		FROM [dbo].[app_parameters]
		WHERE [code] = @p_code
		AND [deactivation_ver_id] = @v_app_param_ver_id
		AND [is_system] = 0;

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_app_parameters_update]';
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