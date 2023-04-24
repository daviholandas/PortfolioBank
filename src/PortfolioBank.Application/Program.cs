using System.Text.Json;
using PortfolioBank.TradeCategorization;

await using var stream = File.OpenRead("./Assets/trades.json");

var trades = await JsonSerializer.DeserializeAsync<IEnumerable<Trade>>(stream);

if (trades is null || !trades.Any())
    throw new InvalidOperationException("The trades list is empty.");

var categories = new List<CategoryStrategy>
{   
    new LowRiskCategory(),
    new MediumRiskCategory(),
    new HighRiskCategory()
};

var classifier = new TradeClassifyService(categories);

var result = classifier
    .ClassifyTrades(trades)
    .ToList();

Console.WriteLine("Trades categorized:");
Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));