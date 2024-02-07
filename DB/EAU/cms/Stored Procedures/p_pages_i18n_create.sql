



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [cms].[p_pages_i18n_create]
	@p_page_id							INT,
	@p_language_id						INT,
	@p_title							NVARCHAR(200),
	@p_content							NVARCHAR(MAX)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_page_id IS NULL OR
		@p_language_id IS NULL OR
		(@p_title IS NULL AND @p_content IS NULL)) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[cms].[p_pages_i18n_create]';
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
		
		SET @v_Date = [dbo].[f_sys_get_time]();		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		INSERT INTO [cms].[pages_i18n]
           ([page_id]
		  ,[language_id]
		  ,[title]
		  ,[content]
		  ,[created_by]
		  ,[created_on]
		  ,[updated_by]
		  ,[updated_on])
     VALUES
           (@p_page_id
		   ,@p_language_id
           ,@p_title
           ,@p_content
           ,@v_curr_user_id --<created_by, int,>
           ,@v_date			--<created_on, datetimeoffset(3),>
           ,@v_curr_user_id --<updated_by, int,>
           ,@v_date			--<updated_on, datetimeoffset(3),>
		   );

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[cms].[p_pages_i18n_create]';
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