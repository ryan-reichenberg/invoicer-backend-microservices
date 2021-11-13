using System;
using System.Collections.Generic;
using Hangfire;
using Microsoft.Extensions.Configuration;
using PaymentsService.Domain;
using Stripe;

namespace PaymentsService.Services
{
    public class StripeService : IStripeService
    {
        private const double InvoiceApplicationRate = 0.5; 
        private IConfiguration _configuration;
        
        public StripeService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("stripe:apiKey");
            _configuration = configuration;
        }
        public Payout SendPayoutToAccount()
        {
            // Get Paid invoices awaiting payout -> Invoice service
            // Hard code for now, this will all be in the db :)
            var options = new PayoutCreateOptions
            {
                Amount = 1000,
                Currency = "aud",
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = "accountId";

            var service = new PayoutService();
            var payout = service.Create(options);
            // Send event
            return payout;
        }

        public PaymentIntent CreatePaymentCharge(string currency, long amount, string accountId)
        {
            var service = new PaymentIntentService();
            var createOptions = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Amount = amount,
                Currency = currency,
                ApplicationFeeAmount = (long)(amount * InvoiceApplicationRate),
                TransferData = new PaymentIntentTransferDataOptions
                {
                    Destination = accountId,
                },
            };
            var intent = service.Create(createOptions);
            // send event here
            return intent;
        }

        public string CreateStripeAccount(UserDetails details)
        {

            var options = new AccountCreateOptions
            {
                Type = details.Type,
                BusinessType =  details.BusinessType,
                Email = details.Email,
                Country = details.Country,
                Capabilities = new AccountCapabilitiesOptions
                {
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions
                    {
                        Requested = true,
                    }, 
                    Transfers = new AccountCapabilitiesTransfersOptions
                    {
                        Requested = true,
                    },
                },
                Settings = new AccountSettingsOptions
                {
                    Payouts = new AccountSettingsPayoutsOptions
                    {
                        Schedule = new AccountSettingsPayoutsScheduleOptions
                        {
                            Interval = "manual",
                        },
                    },
                },
            };

            var accountService = new AccountService();
            var account = accountService.Create(options);
            
            var accountLinkCreateOptions = new AccountLinkCreateOptions
            {
                Account = account.Id,
                RefreshUrl = "https://localhost:3000/reauth",
                ReturnUrl = "https://localhost:3000/return",
                Type = "account_onboarding",
            };
            var accountLinkService = new AccountLinkService();
            var accountLink = accountLinkService.Create(accountLinkCreateOptions);
            return accountLink.Url;
        }

        public string ReauthenticateStripe()
        {
            throw new NotImplementedException();
        }
    }
}