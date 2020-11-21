/*
Course: CS-342 Database Management Systems
Instructor: Professor Patrick Bailey
Student: Joseph Jinn
Date: 11-20-20
Assignment: Pubs Database Integration Application Project - Stored Procedure

Note to Self:

If Intellisense is having issues...
https://www.tech-recipes.com/rx/71090/how-to-refresh-intellisense-cache-in-sql-server/

EDIT -> INTELLISENCE -> Refresh Local Cache
CTRL + SHIFT + R

Notes: 

Murach Chapter 15 on Stored Procedures - pages 396-415.

*/

USE pubs;
GO

---------------------------------------------------------------------------------------

DROP PROC IF EXISTS [proc_ainfo];
GO

---------------------------------------------------------------------------------------

CREATE PROC [proc_ainfo]
	@AuthorFirstName VARCHAR(20),
	@AuthorLastName VARCHAR(40),
	@AuthorNewAddress VARCHAR(40)
AS
IF EXISTS (SELECT * 
				FROM authors 
				WHERE authors.au_fname = @AuthorFirstName 
					AND authors.au_lname = @AuthorLastName)
	BEGIN
	UPDATE dbo.authors
		SET address = @AuthorNewAddress
		WHERE authors.au_fname = @AuthorFirstName 
			AND authors.au_lname = @AuthorLastName;
	END
ELSE
	THROW 50001, 'Not a valid author!', 1;
GO

---------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------

BEGIN TRY
	DECLARE @AuFirst VARCHAR(20);
	SET @AuFirst = 'Joseph';
	DECLARE @AuLast VARCHAR(40);
	SET @AuLast = 'Jinn';
	DECLARE @AuAddress VARCHAR(40);
	SET @AuAddress = 'New Address';

	EXEC [proc_ainfo] 
		@AuthorFirstName = @AuFirst, 
		@AuthorLastName = @AuLast, 
		@AuthorNewAddress = @AuAddress;
END TRY
BEGIN CATCH
	PRINT 'An error occurred.';
	PRINT 'Message: ' + CONVERT(VARCHAR, ERROR_MESSAGE());
	IF ERROR_NUMBER() >= 50000
		PRINT 'This is a custom error message.';
END CATCH
GO

---------------------------------------------------------------------------------------

SELECT * FROM authors;

--INSERT INTO authors
--	(au_id, au_lname, au_fname, phone, address, city, state, zip, contract)
--	VALUES
--	('616-29-8402', 'Jinn', 'Joseph', '616-295-8402', '2266 Burton Pointe Blvd SE', 'Grand Rapids', 'MI', '49546', 1)
--GO
