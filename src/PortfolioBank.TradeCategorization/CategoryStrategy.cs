namespace PortfolioBank.TradeCategorization;

public abstract class CategoryStrategy
{
    public virtual string CategoryName { get; } = string.Empty;

    protected double TradesValue  
        => 1_000_000;
    
    public abstract bool IsEligible(ITrade trade);
}

public struct Category
{
    public const string LowRisk = "LOWRISK";
    public const string MediumRisk = "MEDIUMRISK";
    public const string HighRisk = "HIGHRISK";
}

public struct Sector
{
    public const string Public = "Public";
    public const string Private = "Private";
}