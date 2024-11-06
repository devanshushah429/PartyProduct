using PartyProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Entities;

namespace PartyProduct.Controllers
{
    public class PartyWiseProductController : Controller
    {
        private readonly IPartyWiseProductsService _partyWiseProductsService;
        public PartyWiseProductController(IPartyWiseProductsService partyWiseProductsService)
        {
            _partyWiseProductsService = partyWiseProductsService;
        }

        #region Get the products as per party
        [HttpGet]
        public async Task<List<PartyWiseProduct>> GetProductsForParty(int id)
        {
            List<PartyWiseProduct> partyWiseProducts = await _partyWiseProductsService.GetAllProductOfPartyByID(id);
            return partyWiseProducts;

        }
        #endregion

        #region Open form to add Party Wise Product
        [HttpGet]
        public async Task<IActionResult> AddPartyWiseProduct(int id)
        {
            List<Product?> availableProducts = await _partyWiseProductsService.GetProductIDsOfParty(id);
            ViewBag.PartyID = id;
            return View(availableProducts);
        }
        #endregion

        #region Add the Product for party to partywise product
        [HttpPost]
        public IActionResult AddPartyWiseProduct(int partyID, int productID)
        {
            PartyWiseProduct partyWiseProductModel = new PartyWiseProduct() { ProductID = productID, PartyID = partyID };
            _partyWiseProductsService.AddPartyWiseProduct(partyWiseProductModel);
            return RedirectToAction("Details", "Party", new { partyID = partyID });
        }
        #endregion

    }
}
