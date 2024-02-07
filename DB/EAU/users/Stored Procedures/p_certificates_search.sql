


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [users].[p_certificates_search]
	@p_certificate_ids		NVARCHAR(MAX),
	@p_cert_hash			NVARCHAR(100),
	@p_cert_sernum			NVARCHAR(100),
	@p_load_content			BIT
AS
BEGIN
	SET NOCOUNT ON;	
 
	IF (  @p_certificate_ids IS NULL AND @p_cert_hash  IS NULL AND @p_cert_sernum IS NULL ) 
	BEGIN
	  EXEC [dbo].[p_sys_raise_dberror] -1, '[p_user_authentications_search]';
	  RETURN -1;
	END
	ELSE
	BEGIN
	  BEGIN TRY
	  -- ===============================================================================
		
		DECLARE @v_statement		NVARCHAR(max);
		DECLARE @v_statement_count	NVARCHAR(max);
		DECLARE @v_Params			NVARCHAR(1000);
		DECLARE @v_ids_table		tt_integers;

		IF @p_certificate_ids IS NOT NULL AND rtrim(ltrim(@p_certificate_ids)) <> ''
		BEGIN
			INSERT INTO 
				@v_ids_table
			SELECT rtrim(ltrim(item)) FROM [dbo].[f_sys_split_string_to_int_numbers](@p_certificate_ids, ',');
		END

		SET @v_Params = N'
				@v_ids_table		TT_INTEGERS READONLY,
				@p_cert_hash		NVARCHAR(100),
				@p_cert_sernum		NVARCHAR(200),
				@p_load_content		BIT';


		SET @v_statement = N'SELECT 
				[certificate_id], [serial_number], 
				[issuer], [subject], [not_after], [not_before], [thumbprint], 			 
				[created_by], [created_on],
			  
				CASE   
					WHEN @p_load_content IS NOT NULL AND @p_load_content = 1 THEN c.[content]
					ELSE NULL
				END as content
			  			  
			FROM [users].[certificates] c
			WHERE 1 = 1 ';

		IF @p_certificate_ids IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND c.certificate_id in (SELECT * FROM @v_ids_table)
		';
		END
  
		IF @p_cert_hash IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND c.thumbprint = @p_cert_hash 
		';
		END

		IF @p_cert_sernum IS NOT NULL
		BEGIN
			SET @v_statement = @v_statement + N' AND c.serial_number = @p_cert_sernum 
		';
		END
  				
		EXEC sp_executeSQL @v_statement,
			@params				 = @v_Params,				
			@v_ids_table		 = @v_ids_table,
			@p_cert_hash		 = @p_cert_hash,
			@p_cert_sernum		 = @p_cert_sernum,
			@p_load_content		 = @p_load_content;
				
		-- ===============================================================================			
	END TRY
	   BEGIN CATCH

		-- ===============================================================================
		-- STANDARD ERROR HANDLING MODULE;
		
		-- Raise an error with the details of the exception
        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
        SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)
        
		-- RETURN -ERROR_NUMBER();
		-- ===============================================================================
		
	  END CATCH;
	END;
END
