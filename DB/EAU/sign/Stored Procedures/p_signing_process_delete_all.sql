

-- =============================================
-- Author:		Georgiev
-- Create date: 08.05.2020
-- Description:	Изтрива записи от таблици [sign].[signing_process] и [sign].[signers] за подадения списък с идентификатори на процеси.
-- =============================================
CREATE PROCEDURE [sign].[p_signing_process_delete_all]
	@p_process_ids	[dbo].[tt_guids] readonly
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF (NOT EXISTS (SELECT 1 FROM @p_process_ids)) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[sign].[p_signing_process_delete_all]';
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
		
		DECLARE @ids_count INT;

		SET @ids_count = (SELECT COUNT(*) FROM @p_process_ids);

		DELETE FROM [sign].[signers] 
		WHERE [process_id] IN (SELECT [id] FROM @p_process_ids);

		DELETE FROM [sign].[signing_processes]
		WHERE [process_id] IN (SELECT [id] FROM @p_process_ids);


		IF @@ROWCOUNT <> @ids_count
		BEGIN
			EXEC [dbo].[p_sys_raise_dberror] -16, '[sign].[p_signing_process_delete_all]';
			RETURN -1;
		END
		
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