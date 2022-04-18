using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using InvoicingService.Domain;
using InvoicingService.DTO;
using InvoicingService.Messages.Commands;
using InvoicingService.Messages.Queries;
using InvoicingService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public InvoicesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("/user/{Id}")]
        public async Task<IActionResult> GetInvoicesForUser(GetInvoicesForUserQuery query)
        {
            var invoices = await _queryDispatcher.QueryAsync(query);
            return Ok(invoices);
        }
        
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetInvoice(GetInvoiceByIdQuery query)
        {
            var invoice = await _queryDispatcher.QueryAsync(query);
            return Ok(invoice);
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoice([FromQuery] GetInvoicesByQuery query)
        {
            var invoice = await _queryDispatcher.QueryAsync(query);
            return Ok(invoice);
        }
        
        [HttpPost]
        public IActionResult CreateNewInvoice(CreateNewInvoiceCommand command)
        {
            _commandDispatcher.SendAsync(command);
            return NoContent();
        }
        
        [HttpPut]
        public IActionResult UpdateInvoice(UpdateInvoiceCommand command)
        {
            _commandDispatcher.SendAsync(command);
            return NoContent();
        }
        
        [HttpPut("status/{Status}")]
        public IActionResult UpdateInvoice(Invoice invoice, InvoiceStatus status)
        {
            return Ok();
        }
    }
}