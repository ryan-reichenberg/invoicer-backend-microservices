using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentsService.Domain;
using PaymentsService.Services;

namespace PaymentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private IStripeService _stripeService;

        public PaymentsController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("payment")]
        public async Task<IActionResult> CreatePaymentIntent(PaymentDetails details)
        {
            var intent = _stripeService.CreatePaymentCharge(details.Currency, details.Amount, details.AccountId);
            return Ok(intent.ClientSecret);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> CreateStripeAccount(UserDetails details)
        {
            var url = _stripeService.CreateStripeAccount(details);
            return Ok(url);
        }
        
        [HttpPost("reauthenticate")]
        public async Task<IActionResult> ReauthenticateStripe(UserDetails details)
        {
            /**
             * We should store the stripe account id in the db
             * If we reauth the user should already be authenticated -> we can fetch the stripe account id from the db
             * since we will know who the user is.
             */
            var url = _stripeService.CreateStripeAccount(details);
            return Ok(url);
        }
    }
}