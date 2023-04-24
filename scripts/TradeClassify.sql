CREATE TABLE Trades (
    Id INT IDENTITY PRIMARY KEY,
    Value FLOAT NOT NULL,
    ClientSector VARCHAR(10) NOT NULL
);


CREATE TABLE TradeCategories (
     Id INT IDENTITY PRIMARY KEY,
     Name VARCHAR(50) NOT NULL,
     MinValue FLOAT NOT NULL,
     MaxValue FLOAT NULL,
     ClientSector VARCHAR(10) NOT NULL
);


CREATE PROCEDURE CategorizeTrades
    AS
BEGIN
    SET NOCOUNT ON;

CREATE TABLE #TradeCategories (
    Id INT NOT NULL,
    Name VARCHAR(50) NOT NULL
);

DECLARE @Id INT, @Value FLOAT, @ClientSector VARCHAR(10), @CategoryName VARCHAR(50);

    DECLARE TradesCursor CURSOR FOR SELECT Id, Value, ClientSector FROM Trades;
OPEN TradesCursor;

FETCH NEXT FROM TradesCursor INTO @Id, @Value, @ClientSector;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @CategoryName = NULL;

    DECLARE @MinValue FLOAT, @MaxValue FLOAT;
    DECLARE CategoryCursor CURSOR FOR SELECT MinValue, MaxValue, Name FROM TradeCategories WHERE ClientSector = @ClientSector;
OPEN CategoryCursor;

FETCH NEXT FROM CategoryCursor INTO @MinValue, @MaxValue, @CategoryName;
WHILE @@FETCH_STATUS = 0 AND @CategoryName IS NULL
BEGIN
    IF (@MinValue IS NULL OR @Value > @MinValue) AND (@MaxValue IS NULL OR @Value <= @MaxValue)
BEGIN
    SET @CategoryName = Name;
END

FETCH NEXT FROM CategoryCursor INTO @MinValue, @MaxValue, @CategoryName;
END

CLOSE CategoryCursor;
DEALLOCATE CategoryCursor;

INSERT INTO #TradeCategories (Id, Name) VALUES (@Id, @CategoryName);

FETCH NEXT FROM TradesCursor INTO @Id, @Value, @ClientSector;
END

CLOSE TradesCursor;
DEALLOCATE TradesCursor;

SELECT T.Id, T.Value, T.ClientSector, C.Name AS CategoryName
FROM Trades T
     JOIN #TradeCategories TC ON T.Id = TC.Id
     JOIN TradeCategories C ON TC.Name = C.Name;
END
