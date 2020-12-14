USE [pubs]
GO
/****** Object:  UserDefinedDataType [dbo].[empid]    Script Date: 12/14/2020 8:04:18 AM ******/
CREATE TYPE [dbo].[empid] FROM [char](9) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[id]    Script Date: 12/14/2020 8:04:18 AM ******/
CREATE TYPE [dbo].[id] FROM [varchar](11) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[tid]    Script Date: 12/14/2020 8:04:18 AM ******/
CREATE TYPE [dbo].[tid] FROM [varchar](6) NOT NULL
GO
/****** Object:  View [dbo].[titleview]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[titleview]
AS
select title, au_ord, au_lname, price, ytd_sales, pub_id
from authors, titles, titleauthor
where authors.au_id = titleauthor.au_id
   AND titles.title_id = titleauthor.title_id

GO
/****** Object:  View [dbo].[vAuthors]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vAuthors]
AS
SELECT *
	FROM authors;
GO
/****** Object:  View [dbo].[vAuthorTitles]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vAuthorTitles]
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
/****** Object:  StoredProcedure [dbo].[byroyalty]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[byroyalty] @percentage int
AS
select au_id from titleauthor
where titleauthor.royaltyper = @percentage

GO
/****** Object:  StoredProcedure [dbo].[prAddToOrder]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[prAddToOrder]
@inIncrease int
,@inTitleId varchar(12)
,@outTotalAdd int OUTPUT
AS
BEGIN 
	DECLARE @returnValue int
		,@ERR_MSG AS NVARCHAR(4000) 
		,@ERR_STA AS SMALLINT 

	SET @returnValue = 0
	SET NOCOUNT ON
	BEGIN TRY
		UPDATE dbo.sales 
			SET qty = qty + @inIncrease
			WHERE title_id = @inTitleId
		SELECT @outTotalAdd = @inIncrease * @@ROWCOUNT
		IF @outTotalAdd = 0
		  THROW 50001, 'Nothing added', 1
		SET @returnValue = 0 --it worked
	END TRY
	BEGIN CATCH
		 SELECT @ERR_MSG = ERROR_MESSAGE(),
		 @ERR_STA = ERROR_STATE()
 
		 SET @ERR_MSG= 'Error occurred while retrieving the data from database: ' + @ERR_MSG;
 
		 THROW 50001, @ERR_MSG, @ERR_STA;
		 SET @returnValue = 1
	END CATCH
	RETURN @returnValue
END
GO
/****** Object:  StoredProcedure [dbo].[proc_ainfo]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------

CREATE PROC [dbo].[proc_ainfo]
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
/****** Object:  StoredProcedure [dbo].[reptq1]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[reptq1] AS
select 
	case when grouping(pub_id) = 1 then 'ALL' else pub_id end as pub_id, 
	avg(price) as avg_price
from titles
where price is NOT NULL
group by pub_id with rollup
order by pub_id

GO
/****** Object:  StoredProcedure [dbo].[reptq2]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[reptq2] AS
select 
	case when grouping(type) = 1 then 'ALL' else type end as type, 
	case when grouping(pub_id) = 1 then 'ALL' else pub_id end as pub_id, 
	avg(ytd_sales) as avg_ytd_sales
from titles
where pub_id is NOT NULL
group by pub_id, type with rollup

GO
/****** Object:  StoredProcedure [dbo].[reptq3]    Script Date: 12/14/2020 8:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[reptq3] @lolimit money, @hilimit money,
@type char(12)
AS
select 
	case when grouping(pub_id) = 1 then 'ALL' else pub_id end as pub_id, 
	case when grouping(type) = 1 then 'ALL' else type end as type, 
	count(title_id) as cnt
from titles
where price >@lolimit AND price <@hilimit AND type = @type OR type LIKE '%cook%'
group by pub_id, type with rollup

GO
