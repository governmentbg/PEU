






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_srv_instances_update]
	@p_srv_instance_id					  BIGINT,
	@p_status							  INT,
	@p_additional_data			          NVARCHAR(MAX)
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_srv_instance_id IS NULL  
		OR @p_status IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_srv_instance_update]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_srv_instance_ver_id BIGINT;
		SET @v_Date = [dbo].[f_sys_get_time]();

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_srv_instance_ver_id OUTPUT;

		-- деактивиране на съществуващата последна версия
		UPDATE [dbo].[service_instances]
		   SET [is_last]							= 0
			  ,[deactivation_ver_id]				= @v_srv_instance_ver_id			  
			  ,[updated_by]							= @v_curr_user_id
			  ,[updated_on]							= @v_Date
		 WHERE [service_instance_id] = @p_srv_instance_id
			AND [is_last] = 1
			AND [deactivation_ver_id] IS NULL
			AND dbo.f_has_user_access_to_row(created_by) = CAST (1 AS BIT);

		-- създаване на нова версия
		INSERT INTO [dbo].[service_instances]
           ([service_instance_id]
		  ,[service_instance_ver_id]
		  ,[status]
		  ,[applicant_id]
		  ,[service_instance_date]
		  ,[service_id]
		  ,[service_ver_id]
		  ,[case_file_uri]
		  ,[additional_data]
		  ,[is_last]
		  ,[deactivation_ver_id]
		  ,[created_by]
		  ,[created_on]
		  ,[updated_by]
		  ,[updated_on])
		  SELECT 
				@p_srv_instance_id, 
				@v_srv_instance_ver_id, 
				@p_status,
				[applicant_id],
				[service_instance_date],
				[service_id],
				[service_ver_id],
				[case_file_uri],
				@p_additional_data,
				1, 
				null,
				@v_curr_user_id,
				@v_date,
				@v_curr_user_id,
				@v_date
		FROM [dbo].[service_instances]
		WHERE [service_instance_id] = @p_srv_instance_id
		AND [deactivation_ver_id] = @v_srv_instance_ver_id;


		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_srv_instance_update]';
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