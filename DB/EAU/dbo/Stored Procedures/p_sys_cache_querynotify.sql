


CREATE PROCEDURE [dbo].[p_sys_cache_querynotify] 
	@p_Tablename	NVARCHAR(100)
AS
BEGIN
	/*
	must not have return. not works with query notifications
	for more info http://msdn.microsoft.com/en-us/library/ms181122.aspx
	*/
	select tablename, last_updated_on 
	from [nom].[nomenclature_changes] where tablename = @p_Tablename

END;
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[p_sys_cache_querynotify] TO [eau_sql_dependency]
    AS [dbo];

