namespace StocksApp.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }
        public double CurrentPrice { get; set; }
        public double LowestPrie { get; set; }
        public double HighestPrice { get; set; }
        public double OpenPrice { get; set; }
    }
    public class StockList
    {
        public Stock StockAAPL { get; set; }
        public Stock StockMSFT { get; set; }
    }
}
