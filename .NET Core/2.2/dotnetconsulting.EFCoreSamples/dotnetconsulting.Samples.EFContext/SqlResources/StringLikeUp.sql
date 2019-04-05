-- ResourceName dotnetconsulting.Samples.EFContext.SqlResources.StringLikeUp.sql

CREATE OR ALTER FUNCTION [dbo].[StringLike]
(
	@String VARCHAR(2000),
	@Pattern VARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	DECLARE @result BIT;

	IF @String LIKE @Pattern
		SET @result = 1;
	ELSE
		SET @result =  0;

	return @result;
END