namespace PortfolioBank.TradeCategorization;

public interface ITradeClassifyService
{
    IEnumerable<string> ClassifyTrades(IEnumerable<ITrade> trades);
}