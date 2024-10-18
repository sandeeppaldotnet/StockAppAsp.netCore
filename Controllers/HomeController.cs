using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;
using System.Diagnostics;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        public HomeController(ILogger<HomeController> logger, FinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _logger = logger;
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }
            Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

            Dictionary<string, object>? responseDictionaryAAPL = await _finnhubService.GetStockPriceQuote("AAPL");

            Stock StockMSFT = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionaryAAPL["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionaryAAPL["h"].ToString()),
                LowestPrie = Convert.ToDouble(responseDictionaryAAPL["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionaryAAPL["o"].ToString())
            };

            Stock StockAAPL = new Stock()
            {
                StockSymbol ="AAPL",
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrie = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
            };
            StockList stock = new StockList();
            stock.StockAAPL = StockAAPL;
            stock.StockMSFT = StockMSFT;
            return View(stock);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
