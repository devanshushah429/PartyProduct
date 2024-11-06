using PartyProduct.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Entities;

namespace PartyProduct.Controllers
{
    public class InvoiceWiseProductController : Controller
    {
        private readonly IInvoiceWiseProductsService _invoiceWiseProductService;
        private readonly IPartyWiseProductsService _partyWiseProductsService;

        public InvoiceWiseProductController(IInvoiceWiseProductsService invoiceWiseProductsService, IPartyWiseProductsService partyWiseProductsService)
        {
            _invoiceWiseProductService = invoiceWiseProductsService;
            _partyWiseProductsService = partyWiseProductsService;
            
        }

        #region Index Method
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int invoiceWiseProductID)
        {
            int? invoiceID = await _invoiceWiseProductService.DeleteInvoiceWiseProduct(invoiceWiseProductID);
            return RedirectToAction("EditInvoice", "Invoice", new { invoiceID = invoiceID });
        }
        #endregion

        #region Add Invoice Wise Product
        [HttpGet]
        public async Task<IActionResult> AddInvoiceWiseProduct(int invoiceID, int partyID)
        {
            List<PartyWiseProduct> list = await _partyWiseProductsService.GetAllProductOfPartyByID(partyID);
            List<int?> excludeProductsIDs = await _invoiceWiseProductService.GetAllID(invoiceID);
            ViewBag.Products = list.Where(x => !excludeProductsIDs.Contains(x.ProductID));
            ViewBag.InvoiceID = invoiceID;
            return View();
        }
        #endregion

        #region Add Invoice and save to database
        [HttpPost]
        public async Task<IActionResult> AddInvoiceWiseProduct(InvoiceWiseProduct model)
        {
            if (ModelState.IsValid)
            {
                await _invoiceWiseProductService.AddInvoiceWiseProduct(model);
                return RedirectToAction("Details", "Invoice", new { invoiceID = model.InvoiceID });
            }
            return View(model);
        }
        #endregion

        #region Open Edit Form
        [HttpGet]
        public async Task<IActionResult> Edit(int invoiceWiseProductID)
        {
            InvoiceWiseProduct invoiceWiseProduct = await _invoiceWiseProductService.GetDetails(invoiceWiseProductID);
            return View(invoiceWiseProduct);
        }
        #endregion

        #region Edit and save to database
        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceWiseProduct invoiceWiseProductModel)
        {
            await _invoiceWiseProductService.EditInvoiceWiseProduct(invoiceWiseProductModel);
            return RedirectToAction("Details", "Invoice", new { invoiceID = invoiceWiseProductModel.InvoiceID });
        }
        #endregion

    }
}
