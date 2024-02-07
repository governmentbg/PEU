





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [rebus].[p_message_move]
	@p_src_queue_table					  NVARCHAR(100),
	@p_dst_queue_table					  NVARCHAR(100),
	@p_batch_size						  INT = 1000,
	@p_id								  INT = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
 
	IF ( @p_src_queue_table IS NULL OR 
	     @p_dst_queue_table IS NULL) -- PARAMETER ERROR
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[dbo].[p_message_move]';
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
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);

		SET @v_Params = N'
		@p_batch_size	INT,
		@p_id INT';

		SET @v_statement = N'
			WITH TopCTE AS 
			(
				SELECT TOP(@p_batch_size) S.*
				FROM	' + @p_src_queue_table + ' S WITH (ROWLOCK, READPAST, READCOMMITTEDLOCK)
				WHERE 1 = 1';
				

		IF @p_id IS NOT NULL
		BEGIN
			SET @v_statement += N' 
		   AND S.[ID] = @p_id';
		END  	
		
		SET @v_statement += N'
				ORDER
				BY		[priority] DESC,
						[visible] ASC,
						[id] ASC
			)
			INSERT INTO ' + @p_dst_queue_table + '
			SELECT priority, expiration, visible, headers, body FROM (
			DELETE FROM TopCTE
				OUTPUT deleted.*
			) AS T;';
		

		EXEC sp_executeSQL @v_statement,
			@params			= @v_Params,
			@p_batch_size	= @p_batch_size,
			@p_id			= @p_id;

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