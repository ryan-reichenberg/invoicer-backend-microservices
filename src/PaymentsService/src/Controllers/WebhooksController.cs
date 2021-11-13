using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;

namespace PaymentsService.Controllers
{
    public class WebhooksController : Controller
    {
        private readonly ILogger<WebhooksController> logger;

        public WebhooksController(
            ILogger<WebhooksController> logger
        ) {
            this.logger = logger;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> ProcessWebhookEvent()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_69LgyN3DVzueMAvBo9fPmV7hewkC69Xz";

            // Verify webhook signature and extract the event.
            // See https://stripe.com/docs/webhooks/signatures for more information.
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded) {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    logger.LogInformation($"{paymentIntent}");
                }

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogInformation(e.ToString());
                return BadRequest();
            }
        }
    }
}