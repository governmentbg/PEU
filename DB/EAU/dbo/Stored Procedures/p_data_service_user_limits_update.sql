





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_data_service_user_limits_update]
	@p_user_limit_id					  INT,
	@p_service_limit_id				      INT,
	@p_user_id							  INT,
	@p_requests_interval				  DATETIME,
	@p_requests_number					  INT,
	@p_status							  INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_user_limit_id IS NULL OR 
		@p_service_limit_id IS NULL OR 
	    @p_user_id IS NULL OR 
		@p_requests_interval IS NULL OR 
		@p_requests_number IS NULL OR
		@p_status IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_data_service_user_limits_update]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_user_limit_ver_id BIGINT, @v_service_limit_ver_id BIGINT;
		SET @v_Date = [dbo].[f_sys_get_time]();
		SELECT @v_service_limit_ver_id = sl.service_limit_ver_id  FROM dbo.data_service_limits sl WHERE sl.service_limit_id = @p_service_limit_id AND sl.IS_LAST = 1

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_user_limit_ver_id OUTPUT;

		-- деактивиране на съществуващата последна версия
		UPDATE [dbo].[data_service_user_limits]
		   SET [is_last]							= 0
			  ,[deactivation_ver_id]				= @v_user_limit_ver_id			  
			  ,[updated_by]							= @v_curr_user_id
			  ,[updated_on]							= @v_Date
		 WHERE [user_limit_id] = @p_user_limit_id
			AND [is_last] = 1
			AND [deactivation_ver_id] IS NULL;

		-- създаване на нова версия
		INSERT INTO [dbo].[data_service_user_limits]
           ([user_limit_id]
		  ,[user_limit_ver_id]
		  ,[service_limit_id]
		  ,[service_limit_ver_id]
		  ,[user_id]
		  ,[requests_interval]
		  ,[requests_number]
		  ,[status]
		  ,[is_last]
		  ,[deactivation_ver_id]
		  ,[created_by]
		  ,[created_on]
		  ,[updated_by]
		  ,[updated_on])
		VALUES
           (@p_user_limit_id
           ,@v_user_limit_ver_id
           ,@p_service_limit_id
           ,@v_service_limit_ver_id
		   ,@p_user_id
           ,@p_requests_interval
           ,@p_requests_number
		   ,@p_status
           ,1				--<is_last, bit,>
           ,null			--<deactivation_ver_id, int,>
           ,@v_curr_user_id --<created_by, int,>
           ,@v_date			--<created_on, datetimeoffset(3),>
           ,@v_curr_user_id --<updated_by, int,>
           ,@v_date			--<updated_on, datetimeoffset(3),>
		   );



		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_data_service_user_limits_update]';
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