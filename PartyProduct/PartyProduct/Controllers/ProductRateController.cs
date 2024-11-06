using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Entities;

namespace PartyProduct.Controllers
{
    public class ProductRateController : Controller
    {
        private readonly IProductRatesService _productRatesService;

        public ProductRateController(IProductRatesService productRatesService)
        {
            _productRatesService = productRatesService;
        }

        #region Get All the product rates
        [Route("ProductRate")]
        [HttpGet]
        public async Task<IActionResult> Index(string productName, DateTime? priceAppliedDate)
        {
            List<ProductRate> list = await _productRatesService.GetAllProductRates();

            #region Searching
            if (!string.IsNullOrEmpty(productName))
            {
                list = list.Where(pr => pr.Product.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (priceAppliedDate.HasValue)
            {
                list = list.Where(pr => pr.PriceAppliedDate.Value.Date == priceAppliedDate.Value.Date).ToList();
            }
            #endregion

            ViewBag.ProductName = productName;
            ViewBag.PriceAppliedDate = priceAppliedDate?.ToString("yyyy-MM-dd");

            return View(list);
        }
        #endregion

    }
}
