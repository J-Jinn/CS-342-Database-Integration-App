/*
Course: CS-342 Database Management Systems
Instructor: Professor Patrick Bailey
Student: Joseph Jinn
Date: 12-14-20
Assignment: Pubs Database Integration Application Project - Stored Procedure(s) and View(s)

Note to Self:

If Intellisense is having issues...
https://www.tech-recipes.com/rx/71090/how-to-refresh-intellisense-cache-in-sql-server/

EDIT -> INTELLISENCE -> Refresh Local Cache
CTRL + SHIFT + R
*/

USE pubs;
GO

---------------------------------------------------------------------------------------
-- Drop the view if it already exists.

DROP PROC IF EXISTS [proc_ainfo];
GO

---------------------------------------------------------------------------------------
-- Create stored procedure to update author's personal information.

CREATE PROC [proc_ainfo]
	@AuthorID NVARCHAR(11),
	@AttributeToChange NVARCHAR(20),
	@AuthorNewData NVARCHAR(50)
AS
BEGIN
	DECLARE @DynamicSQL NVARCHAR(500);
	DECLARE @RETURNVALUE INT;
	SET @RETURNVALUE = -1;

	IF EXISTS (SELECT * 
				FROM authors 
				WHERE authors.au_id = @AuthorID)
		BEGIN
			-- Dynamic SQL so we can change column name AND values dynamically.
			SET @dynamicSQL = N'UPDATE dbo.authors SET dbo.authors.' + @AttributeToChange + ' = ''' + @AuthorNewData + 
			''' WHERE authors.au_id = ''' + @AuthorID + ''';'
			PRINT @DynamicSQL;

			EXECUTE sp_Executesql @DynamicSQL;
			SET @RETURNVALUE = 0;
		END
	ELSE
		BEGIN
			;THROW 50001, 'Not a valid author!', 1;
			SET @RETURNVALUE = -1;
		END
	RETURN @RETURNVALUE;
END
GO

---------------------------------------------------------------------------------------
-- Test stored procedure works.

BEGIN TRY
	DECLARE @AuID NVARCHAR(11);
	SET @AuID = '616-29-8402';

	DECLARE @AuAttribute NVARCHAR(20);
	SET @AuAttribute = 'contract';

	DECLARE @AuNewData NVARCHAR(50);
	SET @AuNewData = '0';

	DECLARE @RETURNVALUE INT;

	EXEC @RETURNVALUE = [proc_ainfo]
		@AuthorID = @AuID,
		@AttributeToChange = @AuAttribute,
		@AuthorNewData = @AuNewData;
		PRINT 'Return Value: ' + CONVERT(VARCHAR, @RETURNVALUE);
END TRY
BEGIN CATCH
	PRINT 'An error occurred.';
	PRINT 'Message: ' + CONVERT(VARCHAR, ERROR_MESSAGE());
	IF ERROR_NUMBER() >= 50000
		PRINT 'This is a custom error message.';
END CATCH
GO

SELECT * FROM authors;
GO

---------------------------------------------------------------------------------------

--INSERT INTO authors
--	(au_id, au_lname, au_fname, phone, address, city, state, zip, contract)
--	VALUES
--	('616-29-8402', 'Jinn', 'Joseph', '616-295-8402', '2266 Burton Pointe Blvd SE', 'Grand Rapids', 'MI', '49546', 1)
--GO

---------------------------------------------------------------------------------------
-- Drop the view if it already exists.

DROP VIEW IF EXISTS [vAuthorTitles];
GO

---------------------------------------------------------------------------------------
-- Create view to list all titles by selected author.

CREATE VIEW [vAuthorTitles]
AS
SELECT 
	authors.au_id,
	authors.au_fname, 
	authors.au_lname,
	authors.address,
	authors.city,
	authors.state,
	authors.zip,
	titles.title,
	titles.pubdate,
	titles.price
	FROM authors 
		INNER JOIN titleauthor
			ON authors.au_id = titleauthor.au_id
		INNER JOIN titles
			ON titleauthor.title_id = titles.title_id;
GO

---------------------------------------------------------------------------------------
-- Test that the view works.

SELECT * FROM [vAuthorTitles];
GO

---------------------------------------------------------------------------------------
-- Drop the view if it already exists.

DROP VIEW IF EXISTS [vAuthors];
GO

---------------------------------------------------------------------------------------
-- Create view to list all authors.

CREATE VIEW [vAuthors]
AS
SELECT *
	FROM authors;
GO

---------------------------------------------------------------------------------------
-- Test that the view works.

SELECT * FROM [vAuthors];
GO