namespace PortfolioBank.TradeCategorization.UnitTests;

public class TradeClassifyServiceTests
{
    [Fact]
    public void ClassifyTrades_ReceiveAListOfTrades_ShouldReturnAListOfTradeCategorized()
    {
        // Arrange
        var trades = new List<ITrade>
        {
            new Trade(1_000_000, Sector.Private),
            new Trade(2_000_000, Sector.Public),
            new Trade(3_000, Sector.Public)
        };

        var categories = new List<CategoryStrategy>
        {   
            new LowRiskCategory(),
            new MediumRiskCategory(),
            new HighRiskCategory()
        };

        var sut = new TradeClassifyService(categories);

        // Act
        var result = sut.ClassifyTrades(trades).ToList();
        
        // Assert
        Assert.Equal(Category.HighRisk, result[0]);
        Assert.Equal(Category.MediumRisk, result[1]);
        Assert.Equal(Category.LowRisk, result[2]);
    }
    
    [Fact]
    public void ClassifyTrades_ReceiveAListOfTrades_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var trades = new List<ITrade>
        {
            new Trade(1_000, Sector.Private),
        };

        var categories = new List<CategoryStrategy>
        {   
            new LowRiskCategory(),
            new MediumRiskCategory(),
            new HighRiskCategory()
        };

        var sut = new TradeClassifyService(categories);
        
        // Act && Assert
        var ex = Assert.Throws<InvalidOperationException>(() => sut.ClassifyTrades(trades).ToList());
        Assert.Equal("The trade is not eligible for any category.", ex.Message);
    }
}