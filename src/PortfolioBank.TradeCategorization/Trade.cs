namespace PortfolioBank.TradeCategorization;

public record Trade(double Value, string ClientSector)
    : ITrade;