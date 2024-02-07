﻿



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [templates].[p_templates_content_create]
	@p_template_content_id	INT OUT,
	@p_template_id			INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_template_id IS NULL ) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_templates_content_create]';
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

		INSERT INTO [templates].[templates_content] (
			[template_id], [created_by], [created_on], [updated_by], [updated_on]
		)
		VALUES (
			@p_template_id, @v_curr_user_id, @v_date, @v_curr_user_id, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_templates_content_create]';
			RETURN -1;
		END;

		SET @p_template_content_id = SCOPE_IDENTITY();
		
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

