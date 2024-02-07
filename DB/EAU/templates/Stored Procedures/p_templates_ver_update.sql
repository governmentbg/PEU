




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [templates].[p_templates_ver_update]
	@p_template_id			INT,
	@p_template_ver_id		BIGINT OUT,
	@p_name					NVARCHAR(200),
	@p_status				TINYINT,
	@p_is_in_use			BIT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_template_id IS NULL OR @p_name IS NULL OR @p_status IS NULL OR @p_is_in_use IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_templates_ver_update]';
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
		EXEC [dbo].[p_versions_get_next] @p_version_id = @p_template_ver_id OUTPUT;

		-- деактивиране на съществуващата последна версия
		UPDATE [templates].[templates_ver]
		SET
			[name]		= @p_name, 
			[status]	= @p_status, 
			[is_in_use]	= @p_is_in_use, 
			[is_last]	= 0,
			[deactivation_ver_id]	= @p_template_ver_id,
			[updated_by]= @v_curr_user_id, 
			[updated_on]= @v_Date
		WHERE	
			[template_id] = @p_template_id
		AND [is_last] = 1
		AND [deactivation_ver_id] IS NULL;

		-- създаване на нова версия
		INSERT INTO [templates].[templates_ver] (
			[template_id], [template_ver_id], [name], [status], [is_in_use], 
			[is_last], [deactivation_ver_id],
			[created_by], [created_on], [updated_by], [updated_on]
		)
		VALUES (
			@p_template_id, @p_template_ver_id, @p_name, @p_status, @p_is_in_use, 
			1, null,
			@v_curr_user_id, @v_date, @v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_templates_ver_update]';
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

