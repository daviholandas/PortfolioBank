namespace PortfolioBank.TradeCategorization;

public class LowRiskCategory : CategoryStrategy
{
    public override string CategoryName => Category.LowRisk;

    public override bool IsEligible(ITrade trade)
    {
        return trade.Value < TradesValue && trade.ClientSector == Sector.Public;
    }
}

public class MediumRiskCategory : CategoryStrategy
{
    public override string CategoryName => Category.MediumRisk;

    public override bool IsEligible(ITrade trade)
    {
        return trade.Value >= TradesValue && trade.ClientSector == Sector.Public;
    }
}

public class HighRiskCategory : CategoryStrategy
{
    public override string CategoryName => Category.HighRisk;

    public override bool IsEligible(ITrade trade)
    {
        return trade.Value >= TradesValue && trade.ClientSector == Sector.Private;
    }
}