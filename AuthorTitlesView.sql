/*
Course: CS-342 Database Management Systems
Instructor: Professor Patrick Bailey
Student: Joseph Jinn
Date: 11-20-20
Assignment: Pubs Database Integration Application Project - Views

Note to Self:

If Intellisense is having issues...
https://www.tech-recipes.com/rx/71090/how-to-refresh-intellisense-cache-in-sql-server/

EDIT -> INTELLISENCE -> Refresh Local Cache
CTRL + SHIFT + R

Notes: 

Murach Chapter 13 on View - pages 396-415.

*/

USE pubs;
GO

DROP VIEW IF EXISTS [vAuthorTitles];
GO

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
			ON titleauthor.title_id = titles.title_id
	WHERE authors.au_id = '213-46-8915';
GO

SELECT * FROM [vAuthorTitles];
GO

---------------------------------------------------------------------------------------
-- Ignore this for now (won't work for updating the author ID in the WHERE clause).

UPDATE [vAuthorTitles]
SET au_id = '238-95-7766'
WHERE au_id = '238-95-7766';
GO

SELECT * FROM [vAuthorTitles];
GO

---------------------------------------------------------------------------------------

ALTER VIEW [vAuthorTitles]
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
			ON titleauthor.title_id = titles.title_id
	WHERE authors.au_id = '238-95-7766';
GO

SELECT * FROM [vAuthorTitles];
GO

---------------------------------------------------------------------------------------

DROP VIEW IF EXISTS [vAuthors];
GO

CREATE VIEW [vAuthors]
AS
SELECT *
	FROM authors;
GO

SELECT * FROM [vAuthors];
GO