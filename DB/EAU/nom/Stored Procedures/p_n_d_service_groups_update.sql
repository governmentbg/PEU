



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_service_groups_update]
	@p_group_id			INT,
	@p_name				NVARCHAR(1000),
	@p_order_number		INT,
	@p_icon_name		VARCHAR(100),
	@p_is_active		BIT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_group_id IS NULL OR
		@p_name IS NULL OR 
	    @p_order_number IS NULL OR
		@p_is_active IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_service_groups_update]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_group_ver_id BIGINT;
				
		SET @v_Date = [dbo].[f_sys_get_time]();	
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_group_ver_id OUTPUT;

		-- деактивиране на съществуващата последна версия
		UPDATE [nom].[n_d_service_groups]
		   SET [is_last]				= 0
			  ,[deactivation_ver_id]	= @v_group_ver_id	  
			  ,[updated_by]				= @v_curr_user_id
			  ,[updated_on]				= @v_Date
		 WHERE [group_id]				= @p_group_id
		   AND [is_last]				= 1
		   AND [deactivation_ver_id]	IS NULL;

		-- създаване на нова версия
		INSERT INTO [nom].[n_d_service_groups]
           ([group_id]
           ,[group_ver_id]
           ,[name]
           ,[order_number]
           ,[icon_name]
		   ,[is_active]
           ,[is_last]
           ,[deactivation_ver_id]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
     VALUES
           (@p_group_id
           ,@v_group_ver_id
           ,@p_name
           ,@p_order_number
           ,@p_icon_name
		   ,@p_is_active
           ,1				--<is_last, bit,>
           ,null			--<deactivation_ver_id, int,>
           ,@v_curr_user_id --<created_by, int,>
           ,@v_date			--<created_on, datetimeoffset(3),>
           ,@v_curr_user_id --<updated_by, int,>
           ,@v_date			--<updated_on, datetimeoffset(3),>
		   );

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[nom].[p_n_d_service_groups_update]';
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