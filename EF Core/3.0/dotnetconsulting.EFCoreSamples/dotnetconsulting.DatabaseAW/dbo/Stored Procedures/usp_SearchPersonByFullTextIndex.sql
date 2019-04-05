create   proc usp_SearchPersonByFullTextIndex
	@SearchTerm VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT BusinessEntityID AS 'Id', 
	CONCAT(FirstName,' ', LastName, ', ', ISNULL(MiddleName, '-')) AS DisplayString
	FROM Person.Person
	WHERE CONCAT(',',FirstName,',', LastName, ',') Like @SearchTerm
END
