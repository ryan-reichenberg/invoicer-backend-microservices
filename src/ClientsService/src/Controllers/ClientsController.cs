using Microsoft.AspNetCore.Mvc;

namespace ClientsService.Controllers
{
    public class ClientsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}