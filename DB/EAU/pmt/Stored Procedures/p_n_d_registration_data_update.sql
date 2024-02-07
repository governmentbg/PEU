





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_n_d_registration_data_update]
	@p_registration_data_id		INT,
	@p_type						INT,
	@p_description				NVARCHAR(2000),
	@p_cin						NVARCHAR(100),
	@p_email					NVARCHAR(200),
	@p_secret_word				NVARCHAR(200),
	@p_validity_period			time(7),
	@p_portal_url				NVARCHAR(1000),
	@p_notification_url			NVARCHAR(1000),
	@p_service_url				NVARCHAR(1000)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
 IF (@p_registration_data_id IS NULL OR
		 @p_type IS NULL OR 
		 @p_cin IS NULL OR 
		 @p_secret_word IS NULL OR 
		 @p_validity_period IS NULL OR
		 @p_portal_url IS NULL OR
		(@p_type = 1 AND @p_email IS NULL) OR
		(@p_type = 2 AND (@p_notification_url IS NULL OR @p_service_url IS NULL OR @p_description IS NULL))) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_n_d_registration_data_update]';
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
		DECLARE @v_curr_user_id INT;
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		
		UPDATE [pmt].[n_d_registration_data]
		SET
			[type] = @p_type,
			[description] = @p_description,
			[cin] = @p_cin,
			[email] = @p_email,
			[secret_word] = @p_secret_word,
			[validity_period] = @p_validity_period,
			[portal_url] = @p_portal_url,
			[notification_url] = @p_notification_url,
			[service_url] = @p_service_url,
			[updated_by]= @v_curr_user_id, 
			[updated_on]= [dbo].[f_sys_get_time]()
		WHERE [registration_data_id] = @p_registration_data_id;
			

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_n_d_registration_data_update]';
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