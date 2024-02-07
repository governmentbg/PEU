


-- =============================================
-- Author:		Georgiev
-- Create date: 11.05.2020
-- Description:	Създава запис за процес по подписване.
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_create]
	@p_process_id				UNIQUEIDENTIFIER,
	@p_format					SMALLINT,
	@p_file_name				NVARCHAR(200),
	@p_content_type				NVARCHAR(200),
	@p_level					SMALLINT,
	@p_type						SMALLINT,
	@p_digest_method			SMALLINT,
	@p_rejected_callback_url	NVARCHAR(300),
	@p_completed_callback_url	NVARCHAR(300),
	@p_additional_data			NVARCHAR(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_process_id IS NULL 
		OR @p_format IS NULL
		OR @p_level IS NULL
		OR @p_type IS NULL
		OR @p_digest_method IS NULL
	   	OR @p_file_name IS NULL
	    OR @p_file_name = ''
	    OR @p_content_type IS NULL
	    OR @p_content_type = ''
	    OR @p_rejected_callback_url IS NULL
		OR @p_rejected_callback_url = ''
		OR @p_completed_callback_url IS NULL
	    OR @p_completed_callback_url = '') -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_create]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_callback_client_id INT;
		
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		SET @v_Date = [dbo].[f_sys_get_time]();

		-- определя http client nama-a отговорен за обратното известяване.
		SELECT @v_callback_client_id = callback_client_id
		FROM [sign].[n_s_callback_clients_config]
		WHERE LEN(@p_completed_callback_url) > LEN(base_url)
			AND LOWER(substring(@p_completed_callback_url, 0, LEN(base_url) + 1)) = LOWER(base_url);

		INSERT INTO [sign].[signing_processes]
           ([process_id]
           ,[callback_client_config_id]
           ,[format]
           ,[status]
           ,[level]
           ,[type]
           ,[digest_method]
           ,[additional_data]
           ,[rejected_callback_url]
           ,[completed_callback_url]
           ,[file_name]
           ,[content_type]
           ,[content]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
		VALUES (
			@p_process_id
			, @v_callback_client_id
			, @p_format
			, 1 -- в процес
			, @p_level
			, @p_type
			, @p_digest_method
			, @p_additional_data
			, @p_rejected_callback_url
			, @p_completed_callback_url
			, @p_file_name
			, @p_content_type
			, NULL
			, @v_curr_user_id
			, @v_date
			, @v_curr_user_id
			, @v_date
		);

		IF @@ROWCOUNT <> 1
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signing_process_create]';
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