





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_srv_instances_create]
	@p_srv_instance_id				      BIGINT OUT,
	@p_status							  TINYINT,
	@p_applicant_id						  INT,
	@p_service_instance_date			  DATETIME,
	@p_service_id					      INT,
	@p_case_file_uri					  NVARCHAR(100),
	@p_additional_data					  NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_status IS NULL OR 
	     @p_applicant_id IS NULL OR 
		 @p_service_instance_date IS NULL OR 
		 @p_service_id IS NULL OR
		 @p_case_file_uri IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_srv_instance_create]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_srv_instance_ver_id BIGINT, @v_service_ver_id BIGINT;
		
		SET @v_Date = [dbo].[f_sys_get_time]();		
		SELECT @p_srv_instance_id = NEXT VALUE FOR [dbo].[seq_service_instances];
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_srv_instance_ver_id OUTPUT;

		SELECT @v_service_ver_id = s.service_ver_id  FROM nom.n_d_services s WHERE s.service_id = @p_service_id AND s.IS_LAST = 1

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
     VALUES
           (@p_srv_instance_id
           ,@v_srv_instance_ver_id
           ,@p_status
		   ,@p_applicant_id
		   ,@p_service_instance_date
           ,@p_service_id
           ,@v_service_ver_id
           ,@p_case_file_uri
           ,@p_additional_data
           ,1				--<is_last, bit,>
           ,null			--<deactivation_ver_id, int,>
           ,@v_curr_user_id --<created_by, int,>
           ,@v_date			--<created_on, datetimeoffset(3),>
           ,@v_curr_user_id --<updated_by, int,>
           ,@v_date			--<updated_on, datetimeoffset(3),>
		   );



		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[dbo].[p_srv_instance_create]';
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