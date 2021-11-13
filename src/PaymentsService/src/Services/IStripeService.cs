using Hangfire;
using PaymentsService.Domain;
using Stripe;

namespace PaymentsService.Services
{
    public interface IStripeService
    {
        string CreateStripeAccount(UserDetails details);
        string ReauthenticateStripe();

        public PaymentIntent CreatePaymentCharge(string currency, long amount, string accountId);
        
        [AutomaticRetry(Attempts = 3)]
        public Payout SendPayoutToAccount();
    }
}