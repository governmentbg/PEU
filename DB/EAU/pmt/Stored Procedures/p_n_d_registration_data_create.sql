






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_n_d_registration_data_create]
	@p_registration_data_id		INT OUT,
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
 
	IF (@p_type IS NULL OR 
		 @p_cin IS NULL OR 
		 @p_secret_word IS NULL OR 
		 @p_validity_period IS NULL OR
		 @p_portal_url IS NULL OR
		(@p_type = 1 AND @p_email IS NULL) OR
		(@p_type = 2 AND (@p_notification_url IS NULL OR @p_service_url IS NULL OR @p_description IS NULL))) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[pmt].[p_n_d_registration_data_create]';
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
		SELECT @p_registration_data_id = NEXT VALUE FOR [pmt].[seq_n_d_registration_data];
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		INSERT INTO [pmt].[n_d_registration_data]
           ([registration_data_id]
		  ,[type]
		  ,[description]
		  ,[cin]
		  ,[email]
		  ,[secret_word]
		  ,[validity_period]
		  ,[portal_url]
		  ,[notification_url]
		  ,[service_url]

		  ,[created_by]
		  ,[created_on]
		  ,[updated_by]
		  ,[updated_on])
     VALUES
           (@p_registration_data_id
           ,@p_type
		   ,@p_description
		   ,@p_cin
		   ,@p_email
           ,@p_secret_word
           ,@p_validity_period
           ,@p_portal_url
           ,@p_notification_url
		   ,@p_service_url

           ,@v_curr_user_id --<created_by, int,>
           ,@v_date			--<created_on, datetimeoffset(3),>
           ,@v_curr_user_id --<updated_by, int,>
           ,@v_date			--<updated_on, datetimeoffset(3),>
		   );



		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[pmt].[p_n_d_registration_data_create]';
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