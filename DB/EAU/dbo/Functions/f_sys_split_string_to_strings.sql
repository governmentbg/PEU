

CREATE FUNCTION [dbo].[f_sys_split_string_to_strings] 
( 
   @p_Text NVARCHAR(MAX), 
   @p_Separator NVARCHAR(255)
) 
 RETURNS @ReturnData TABLE (Item NVARCHAR(1000))
AS
BEGIN
   DECLARE	@p_Pos INT,
			@p_Next INT,
			@p_SeparatorLen INT = LEN(@p_Separator);
   
   IF @p_Text IS NULL OR @p_Separator IS NULL OR @p_Text = '' OR @p_Separator = ''
	RETURN; 

   SELECT @p_Pos = CHARINDEX(@p_Separator, @p_Text, 1);

   IF(@p_Pos = 0)
   BEGIN
	INSERT INTO @ReturnData SELECT @p_Text;
	RETURN
   END
   
   INSERT INTO @ReturnData
      SELECT SUBSTRING(@p_Text, 1, @p_Pos - 1);
   
   SELECT @p_Pos = @p_Pos + @p_SeparatorLen;

   WHILE (1 = 1)
   BEGIN

      SELECT @p_Next = CHARINDEX(@p_Separator, @p_Text, @p_Pos);
      
      IF (@p_Next = 0) BREAK

      INSERT INTO @ReturnData
         SELECT SUBSTRING(@p_Text, @p_Pos, @p_Next - @p_Pos);

      SELECT @p_Pos = @p_Next + @p_SeparatorLen;
   END

   INSERT INTO @ReturnData
      SELECT SUBSTRING(@p_Text, @p_Pos, LEN(@p_Text) - 1);

   RETURN;
END