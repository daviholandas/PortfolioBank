CREATE DATABASE PortfolioBank;

CREATE TABLE Trades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Value DECIMAL(18,2) NOT NULL,
    ClientSector NVARCHAR(10) NOT NULL
);

CREATE PROCEDURE ClassifyTrades
AS
BEGIN
    CREATE TABLE #RiskCategories (
      TradeValue DECIMAL(18,2),
      ClientSector NVARCHAR(10),
      Category NVARCHAR(10)
    );

    INSERT INTO #RiskCategories (TradeValue, ClientSector, Category)
    SELECT Value, ClientSector,
           CASE
               WHEN Value < 1000000 AND ClientSector = 'Public' THEN 'LOWRISK'
               WHEN Value >= 1000000 AND ClientSector = 'Public' THEN 'MEDIUMRISK'
               WHEN Value >= 1000000 AND ClientSector = 'Private' THEN 'HIGHRISK'
               END AS Category
    FROM Trades;

    SELECT * FROM #RiskCategories;

    DROP TABLE #RiskCategories;
END;
