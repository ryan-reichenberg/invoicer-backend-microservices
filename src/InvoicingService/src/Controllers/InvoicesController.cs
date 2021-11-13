using System;
using InvoicingService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        [HttpGet("/user/{Id}")]
        public IActionResult GetInvoicesForUser(Guid id)
        {
            return Ok();
        }
        
        [HttpGet("{Id}")]
        public IActionResult GetInvoice(Guid id)
        {
            return Ok();
        }
        
        [HttpPost]
        public IActionResult CreateNewInvoice(Invoice invoice)
        {
            return Ok();
        }
        
        [HttpPut]
        public IActionResult UpdateInvoice(Invoice invoice)
        {
            return Ok();
        }
    }
}