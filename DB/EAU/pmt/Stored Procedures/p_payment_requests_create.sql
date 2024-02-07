








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_payment_requests_create]
				@p_payment_request_id				BIGINT OUT,
				@p_registration_data_id				INT,
				@p_registration_data_type			INT,
				@p_status							TINYINT,					
				@p_obligation_id					BIGINT,
				@p_obliged_person_name				NVARCHAR(150),
				@p_obliged_person_ident				NVARCHAR(100),
				@p_obliged_person_ident_type		TINYINT,
				@p_send_date						DATETIME,
				@p_pay_date							DATETIME,
				@p_external_portal_payment_number	NVARCHAR(100),
				@p_amount							MONEY,
				@p_additional_data					NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_registration_data_id IS NULL OR
		@p_registration_data_type IS NULL OR
		@p_status IS NULL OR
		@p_obligation_id IS NULL OR
		@p_amount IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[pmt].[p_payment_requests_create]';
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
		SELECT @p_payment_request_id = NEXT VALUE FOR [pmt].[seq_payment_requests];

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		INSERT INTO [pmt].[payment_requests]
           ([payment_request_id]
		  ,[registration_data_id]
		  ,[registration_data_type]
		  ,[status]
		  ,[obligation_id]
		  ,[obliged_person_name]
		  ,[obliged_person_ident]
		  ,[obliged_person_ident_type]
		  ,[send_date]
		  ,[pay_date]
		  ,[external_portal_payment_number]
		  ,[amount]
		  ,[additional_data]

		  ,[created_by]
		  ,[created_on]
		  ,[updated_by]
		  ,[updated_on])
     VALUES
           (@p_payment_request_id
		    ,@p_registration_data_id
			,@p_registration_data_type
			,@p_status
			,@p_obligation_id
			,@p_obliged_person_name
			,@p_obliged_person_ident
			,@p_obliged_person_ident_type
			,@p_send_date
			,@p_pay_date
			,@p_external_portal_payment_number
			,@p_amount
			,@p_additional_data

			,@v_curr_user_id --<created_by, int,>
			,@v_date			--<created_on, datetimeoffset(3),>
			,@v_curr_user_id --<updated_by, int,>
			,@v_date			--<updated_on, datetimeoffset(3),>
		   );



		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[pmt].[p_payment_requests_create]';
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