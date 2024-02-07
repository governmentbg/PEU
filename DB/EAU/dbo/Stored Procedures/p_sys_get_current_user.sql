CREATE PROCEDURE [dbo].[p_sys_get_current_user]
	@p_userid_out		INT		OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @p_userid_out = CAST(CAST(CONTEXT_INFO() AS VARBINARY(4)) AS INTEGER);
	
END

