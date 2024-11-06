using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServiceContracts;
using Entities;

namespace PartyProduct.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoicesService _invoiceService;
        public InvoiceController(IInvoicesService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        #region Display the List of All Invoices
        public async Task<IActionResult> Index()
        {
            List<Invoice> list = await _invoiceService.GetAllInvoices();
            return View(list);
        }
        #endregion

        #region Display the Details of Invoice
        public async Task<IActionResult> Details(int invoiceID)
        {
            Invoice invoice = await _invoiceService.GetInvoiceDetails(invoiceID);
            return View(invoice);
        }
        #endregion

        #region Open Add Invoice Page using PartyID
        [HttpGet]
        public async Task<IActionResult> AddInvoiceUsingPartyID(int partyID)
        {
            Invoice model = new Invoice();
            model.PartyID = partyID;
            model.InvoiceWiseProducts = await _invoiceService.GetInvoiceWiseProducts(partyID);
            return View("AddInvoice", model);
        }
        #endregion

        #region Edit the invoice using InvoiceID
        [HttpGet]
        public async Task<IActionResult> EditInvoice(int invoiceID)
        {
            Invoice model = await _invoiceService.GetInvoiceDetails(invoiceID);
            return View(model);
        }
        #endregion

        #region Save for Edit the invoice using InvoiceID
        [HttpPost]
        public async Task<IActionResult> EditInvoice(Invoice model)
        {
            if (ModelState.IsValid)
            {
                //await _invoiceService.EditInvoice(model);
                return RedirectToAction("Index");
            }
            return View("EditInvoice", model);
        }
        #endregion

        #region Save The invoice in database table
        [HttpPost]
        public async Task<IActionResult> AddInvoice(Invoice model)
        {
            if (ModelState.IsValid)
            {
                if (model.InvoiceID == null)
                {
                    await _invoiceService.AddInvoice(model);
                }
                else
                {
                    //await _invoiceService.EditInvoice(model);
                }
                return RedirectToAction("PartyWiseInvoice", "Party", new { partyID = model.PartyID });
            }
            return View("AddEditInvoice", model);
        }
        #endregion

        #region Generate Invoice PDF
        public async Task<IActionResult> InvoicePDF(int invoiceID)
        {
            Invoice invoice = await _invoiceService.GetInvoiceDetails(invoiceID);
            return new ViewAsPdf("InvoicePDF", invoice)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Right = 20,
                    Bottom = 20,
                    Left = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
        #endregion


    }
}
