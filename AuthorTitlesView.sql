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

DROP VIEW IF EXISTS [vAuthorTitles]
GO

CREATE VIEW [vAuthorTitles]
AS
SELECT 
	authors.au_fname, 
	authors.au_lname, 
	titles.title
	FROM authors 
		INNER JOIN titleauthor
			ON authors.au_id = titleauthor.au_id
		INNER JOIN titles
			ON titleauthor.title_id = titles.title_id
GO

SELECT * FROM [vAuthorTitles]
GO