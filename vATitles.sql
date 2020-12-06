/*
Course: CS-342 Database Management Systems
Instructor: Professor Patrick Bailey
Student: Yena Kim
Date: 12-06-20
Assignment: Pubs Database Integration Application Project - Views

Notes: 

Murach Chapter 13 on View - pages 396-415.

*/

USE pubs;
GO

DROP VIEW IF EXISTS [vATitles];
GO

CREATE VIEW [vATitles]
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

SELECT * FROM vATitles

