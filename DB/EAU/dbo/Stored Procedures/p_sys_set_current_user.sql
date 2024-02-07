
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_sys_set_current_user]
	-- Add the parameters for the stored procedure here
	@p_ClientID		NVARCHAR(100),
	@p_ProxyUserID	NVARCHAR(100) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @v_CtxInfo		VARBINARY(128);
	DECLARE @v_ClientProxyData	VARBINARY(8);
	
	IF @p_ProxyUserID IS NOT NULL 
		SET @v_ClientProxyData = CAST(CAST(@p_ClientID AS INT) AS VARBINARY(4)) + CAST(CAST(@p_ProxyUserID AS INT) AS VARBINARY(4));
	ELSE
		SET @v_ClientProxyData = CAST(CAST(@p_ClientID AS INT) AS VARBINARY(4)) + CAST(CAST(-1 AS INT) AS VARBINARY(4));
		
	SET @v_CtxInfo = CONTEXT_INFO();
	
	IF @v_CtxInfo IS NULL 
		SET @v_CtxInfo = @v_ClientProxyData;
	ELSE 
		SET @v_CtxInfo = @v_ClientProxyData + SUBSTRING(@v_CtxInfo, 9 ,120);
	
	SET CONTEXT_INFO @v_CtxInfo
	
END

