using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        // GET
        public async Task<IActionResult> GetAllProjectsForUser(Guid id)
        {
            return Ok();
        }

        public async Task<IActionResult> PostNewProject()
        {
            return Accepted();
        }
        public async Task<IActionResult> PutCompleteProject()
        {
            return Accepted();
        }
        public async Task<IActionResult> PutUpdateProject()
        {
            return Accepted();
        }
    }
}