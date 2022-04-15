using System;
using InvoicingService.Domain;
using InvoicingService.DTO;
using InvoicingService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private IInvoiceRepository _invoiceRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("/user/{Id}")]
        public IActionResult GetInvoicesForUser(Guid id)
        {
            return Ok();
        }
        
        [HttpGet("{Id}")]
        public IActionResult GetInvoice(Guid id)
        {
            var invoice = _invoiceRepository.GetByIdAsync(id.ToString());
            return Ok(invoice);
        }
        
        [HttpPost]
        public IActionResult CreateNewInvoice(InvoiceDto invoice)
        {
            var createdInvoice = _invoiceRepository.SaveAsync(invoice);
            return Ok(createdInvoice);
        }
        
        [HttpPut]
        public IActionResult UpdateInvoice(Invoice invoice)
        {
            return Ok();
        }
        
        [HttpPut("status/{Status}")]
        public IActionResult UpdateInvoice(Invoice invoice, InvoiceStatus status)
        {
            return Ok();
        }
    }
}