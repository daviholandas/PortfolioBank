namespace PortfolioBank.TradeCategorization;

public class TradeClassifyService : ITradeClassifyService
{
    private readonly IEnumerable<CategoryStrategy> _categoryStrategies;

    public TradeClassifyService(IEnumerable<CategoryStrategy> categoryStrategies)
        => _categoryStrategies = categoryStrategies;

    public IEnumerable<string> ClassifyTrades(IEnumerable<ITrade> trades)
    {
        foreach (var trade in trades)
        {
            var category = _categoryStrategies
                .FirstOrDefault(x => x.IsEligible(trade));

            if (category is null)
                throw new InvalidOperationException("The trade is not eligible for any category.");

            yield return category.CategoryName;
        }
    }
}