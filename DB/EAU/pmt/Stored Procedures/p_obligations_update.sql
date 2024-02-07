






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [pmt].[p_obligations_update]
						@p_obligation_id					BIGINT,
						@p_status							TINYINT,					
						@p_amount							MONEY,
						@p_discount_amount					MONEY,
						@p_bank_name						NVARCHAR(500),
						@p_bic								NVARCHAR(8),
						@p_iban								NVARCHAR(22),
						@p_payment_reason					NVARCHAR(2000),
						@p_pep_cin							NVARCHAR(100),
						@p_expiration_date					DATETIME,
						@p_applicant_id						INT,
						@p_obliged_person_name				NVARCHAR(150),
						@p_obliged_person_ident				NVARCHAR(100),
						@p_obliged_person_ident_type		TINYINT,
						@p_obligation_date					DATE,
						@p_obligation_identifier			NVARCHAR(300),
						@p_type								TINYINT,
						@p_service_instance_id				BIGINT,
						@p_service_id						INT,
						@p_additional_data					NVARCHAR(MAX)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_obligation_id IS NULL OR
		@p_status IS NULL OR
		@p_amount IS NULL OR
		@p_discount_amount IS NULL OR
		@p_bic IS NULL OR
		@p_iban IS NULL OR
		@p_payment_reason IS NULL OR
		@p_expiration_date IS NULL OR
		@p_obligation_date IS NULL OR
		@p_obligation_identifier IS NULL OR
		@p_service_id IS NULL OR
		@p_type IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_obligations_update]';
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
		DECLARE @v_curr_user_id INT, @v_service_ver_id BIGINT, @v_service_instance_ver_id BIGINT;
		SELECT @v_service_ver_id = s.service_ver_id  FROM nom.n_d_services s WHERE s.service_id = @p_service_id AND s.IS_LAST = 1;
		SELECT @v_service_instance_ver_id = s.service_instance_ver_id  FROM dbo.service_instances s WHERE s.service_instance_id = @p_service_instance_id AND s.IS_LAST = 1;

		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;
		
		UPDATE [pmt].[obligations]
		SET
		  [status] = @p_status
		  ,[amount] = @p_amount
		  ,[discount_amount] = @p_discount_amount
		  ,[bank_name] = @p_bank_name
		  ,[bic] = @p_bic
		  ,[iban] = @p_iban
		  ,[payment_reason] = @p_payment_reason
		  ,[pep_cin] = @p_pep_cin
		  ,[expiration_date] = @p_expiration_date
		  ,[applicant_id] = @p_applicant_id
		  ,[obliged_person_name] = @p_obliged_person_name
		  ,[obliged_person_ident] = @p_obliged_person_ident
		  ,[obliged_person_ident_type] = @p_obliged_person_ident_type
		  ,[obligation_date] = @p_obligation_date
		  ,[obligation_identifier] = @p_obligation_identifier
		  ,[type] = @p_type
		  ,[service_instance_id] = @p_service_instance_id
		  ,[service_instance_ver_id] = @v_service_instance_ver_id
		  ,[service_id] = @p_service_id
		  ,[service_ver_id] = @v_service_ver_id
		  ,[additional_data] = @p_additional_data
		  ,[updated_by]= @v_curr_user_id
		  ,[updated_on]= [dbo].[f_sys_get_time]()
		WHERE [obligation_id] = @p_obligation_id;
			

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[p_obligations_update]';
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