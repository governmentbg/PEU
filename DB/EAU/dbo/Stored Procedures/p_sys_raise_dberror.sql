CREATE PROCEDURE [dbo].[p_sys_raise_dberror]
	-- Add the parameters for the stored procedure here
	@p_ErrorID			INT,
	@p_Msg1				NVARCHAR(1000) = '',
	@p_Msg2				NVARCHAR(1000) = '',
	@p_Msg3				NVARCHAR(1000) = '',
	@p_Msg4				NVARCHAR(1000) = '',
	@p_Msg5				NVARCHAR(1000) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	
	DECLARE @ErrMsg		NVARCHAR(1000);
	DECLARE @ErrIDText	NVARCHAR(100);
	
	SET @ErrIDText = CAST(@p_ErrorID AS NVARCHAR(100));
	SET @ErrIDText = @ErrIDText + REPLICATE(' ', 10 - LEN(@ErrIDText));
	
	
	SELECT @ErrMsg = @ErrIDText + DESCRIPTION 
	FROM dbo.N_S_DBERRORS
	WHERE ERROR_ID = @p_ErrorID;
	
	
	DECLARE @tmp NVARCHAR(100);
	SET @tmp = 
		CASE WHEN @p_Msg1 IS NOT NULL AND @p_Msg1 <> '' THEN @p_Msg1 + ';' ELSE '' END +
		CASE WHEN @p_Msg2 IS NOT NULL AND @p_Msg2 <> '' THEN @p_Msg2 + ';' ELSE '' END +
		CASE WHEN @p_Msg3 IS NOT NULL AND @p_Msg3 <> '' THEN @p_Msg3 + ';' ELSE '' END +
		CASE WHEN @p_Msg4 IS NOT NULL AND @p_Msg4 <> '' THEN @p_Msg4 + ';' ELSE '' END +
		CASE WHEN @p_Msg5 IS NOT NULL AND @p_Msg5 <> '' THEN @p_Msg5 + ';' ELSE '' END ;
	
	IF @tmp = ''
		SET @tmp = NULL;
		
	SELECT @ErrMsg = dbo.f_Sys_sprintf(@ErrMsg, @tmp, ';');
	
	RAISERROR(@ErrMsg, 16, 1);
END



