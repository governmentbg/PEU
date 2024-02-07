CREATE PROCEDURE [dbo].[p_sys_cache_update_nom_changes] 
    @tableName NVARCHAR(450) 
AS

BEGIN 
    UPDATE [nom].[nomenclature_changes]
	SET
		[last_updated_on] = [dbo].[f_sys_get_time]()
	WHERE 
		[tablename] = @tableName;

	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO [nom].[nomenclature_changes] ([tablename], [last_updated_on])
		VALUES (@tableName, [dbo].[f_sys_get_time]());
	END;
END