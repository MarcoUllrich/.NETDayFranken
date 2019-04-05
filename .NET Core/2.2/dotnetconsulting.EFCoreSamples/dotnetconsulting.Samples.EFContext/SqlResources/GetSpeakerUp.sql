CREATE PROCEDURE dnc.usp_GetSpeaker
	@SearchTerm VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SET @SearchTerm = REPLACE(@SearchTerm, '*','%');
	SET @SearchTerm = REPLACE(@SearchTerm, '?','_');

	SELECT TOP 10 * FROM [dnc].[Speakers] WHERE 
		Homepage LIKE @SearchTerm OR
		Infos LIKE @SearchTerm OR 
		[Name] LIKE @SearchTerm;
END