


-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Създава запис за подписващ.
-- =============================================
CREATE PROCEDURE [sign].[p_signers_create]
	@p_signer_id	BIGINT OUT,
	@p_process_id	UNIQUEIDENTIFIER,
	@p_name			NVARCHAR(200),
	@p_ident		NVARCHAR(10),
	@p_order		SMALLINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_process_id IS NULL OR @p_order IS NULL OR @p_order < 0) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signers_create]';
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

		SELECT @p_signer_id = NEXT VALUE FOR [sign].[seq_signers];

		INSERT INTO [sign].[signers]
           ([signer_id]
           ,[process_id]
           ,[name]
           ,[ident]
           ,[order]
           ,[status]
           ,[signing_channel]
           ,[transaction_id]
           ,[additional_sign_data]
           ,[reject_reson_label]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
		VALUES (
			@p_signer_id
			, @p_process_id
			, @p_name
			, @p_ident
			, @p_order
			, 0
			, NULL
			, NULL
			, NULL
			, NULL
			, @v_curr_user_id
			, @v_date
			, @v_curr_user_id
			, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signers_create]';
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