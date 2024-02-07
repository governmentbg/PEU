



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [nom].[p_n_d_services_create]
	@p_service_id										INT OUT,
	@p_group_id											INT,
	@p_name												NVARCHAR(1000),
	@p_doc_type_id										INT,
	@p_sunau_service_uri								NVARCHAR(100),	
	@p_initiation_type_id								TINYINT,
	@p_result_document_name								NVARCHAR(1000),
	@p_description										NVARCHAR(MAX),
	@p_explanatory_text_service							NVARCHAR(MAX),
	@p_explanatory_text_fulfilled_service				NVARCHAR(MAX),
	@p_explanatory_text_refused_or_terminated_service	NVARCHAR(MAX),
	@p_order_number										INT,
	@p_adm_structure_unit_name							NVARCHAR(500),
	@p_attached_documents_description					NVARCHAR(MAX),
	@p_additional_configuration							NVARCHAR(MAX),
	@p_service_url										NVARCHAR(1000),
	@p_is_active										BIT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (@p_name IS NULL OR 
	    @p_sunau_service_uri IS NULL OR
		@p_initiation_type_id IS NULL OR
		@p_order_number IS NULL OR
		@p_is_active IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[nom].[p_n_d_services_create]';
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
		DECLARE @v_curr_user_id INT, @v_date DATETIMEOFFSET(3), @v_service_ver_id BIGINT;
		
		SET @v_Date = [dbo].[f_sys_get_time]();		
		SELECT @p_service_id = NEXT VALUE FOR [nom].[seq_n_d_services];
		EXEC [dbo].[p_versions_get_next] @p_version_id = @v_service_ver_id OUTPUT;
		EXEC [dbo].[p_sys_get_current_user] @p_userid_out = @v_curr_user_id OUTPUT;

		INSERT INTO [nom].[n_d_services]
           ([service_id]
           ,[service_ver_id]
           ,[group_id]
           ,[group_ver_id]
           ,[name]
		   ,[doc_type_id]
           ,[sunau_service_uri]
           ,[initiation_type_id]
           ,[result_document_name]
           ,[description]
		   ,[explanatory_text_service]
		   ,[explanatory_text_fulfilled_service]
		   ,[explanatory_text_refused_or_terminated_service]
           ,[order_number]
           ,[adm_structure_unit_name]
           ,[attached_documents_description]
		   ,[additional_configuration]
		   ,[service_url]
		   ,[is_active]
           ,[is_last]
           ,[deactivation_ver_id]
           ,[created_by]
           ,[created_on]
           ,[updated_by]
           ,[updated_on])
     VALUES
           (@p_service_id
           ,@v_service_ver_id
           ,@p_group_id
           ,[dbo].[f_get_n_d_service_groups_id](@p_group_id)
           ,@p_name
		   ,@p_doc_type_id
           ,@p_sunau_service_uri
           ,@p_initiation_type_id
           ,@p_result_document_name
           ,@p_description
		   ,@p_explanatory_text_service
		   ,@p_explanatory_text_fulfilled_service
		   ,@p_explanatory_text_refused_or_terminated_service
           ,@p_order_number
           ,@p_adm_structure_unit_name
           ,@p_attached_documents_description
		   ,@p_additional_configuration
		   ,@p_service_url
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
			EXEC [dbo].[p_sys_raise_dberror] -16, '[nom].[p_n_d_services_create]';
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