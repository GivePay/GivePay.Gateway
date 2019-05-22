using System;
using System.Threading.Tasks;
using GivePay.Gateway.Builder;
using GivePay.Gateway.Shared;
using GivePay.Gateway.Transactions;
using GivePay.Gateway.Transactions.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GivePay.Gateway.Tests
{
    [TestClass]
    public class TransactionTests
    {
        private ITransactionClient _client;

        private IConfigurationRoot _configuration;

        private string _terminalId;

        private string _merchantId;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var clientId = _configuration["Gateway:OAuth:ClientId"];
            var clientSecret = _configuration["Gateway:OAuth:ClientSecret"];
            var authority = _configuration["Gateway:OAuth:Authority"];
            var scopes = _configuration["Gateway:OAuth:Scopes"];
            var baseUrl = _configuration["Gateway:BaseUrl"];

            _terminalId = _configuration["Gateway:TID"];
            _merchantId = _configuration["Gateway:MID"];

            _client = new DefaultGatewayClientBuilder()
                .WithBaseUri(new Uri(baseUrl))
                .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
                .BuildTransactionClient();
        }

        [TestMethod]
        public async Task AuthorizationCompletes_Test()
        {
            var authorization = new AuthorizeRequest
            {
                Amount = new Amount
                {
                    BaseAmount = 1000
                },
                Card = new Card
                {
                    CardPresent = false,
                    Cvv = "123",
                    ExpirationMonth = "12",
                    ExpirationYear = "21",
                    Pan = "5105105105105100"
                },
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Payer = new Payer
                {
                    BillingAddress = new Address
                    {
                        PostalCode = "76132"
                    }
                }
            };

            var response = await _client.AuthorizeAmountAsync(authorization);
            Assert.IsNotNull(response.TransactionId);
            Assert.IsNotNull(response.AuthCode);
        }

        [TestMethod]
        public async Task AuthorizeAndCapture_Test()
        {
            var authorization = new AuthorizeRequest
            {
                Amount = new Amount
                {
                    BaseAmount = 1000
                },
                Card = new Card
                {
                    CardPresent = false,
                    Cvv = "123",
                    ExpirationMonth = "12",
                    ExpirationYear = "21",
                    Pan = "5105105105105100"
                },
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Payer = new Payer
                {
                    BillingAddress = new Address
                    {
                        PostalCode = "76132"
                    }
                },
                Order = new Order
                {
                    OrderNumber = "testing"
                }
            };

            var response = await _client.AuthorizeAmountAsync(authorization);

            var capture = new CaptureRequest
            {
                TransactionId = response.TransactionId,
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                }
            };

            var captureResponse = await _client.CaptureAmountAsync(capture);
            Assert.IsNotNull(capture.TransactionId);
        }

        [TestMethod]
        public async Task VoidCompletes_Test()
        {
            var authorization = new AuthorizeRequest
            {
                Amount = new Amount
                {
                    BaseAmount = 1000
                },
                Card = new Card
                {
                    CardPresent = false,
                    Cvv = "123",
                    ExpirationMonth = "12",
                    ExpirationYear = "21",
                    Pan = "5105105105105100"
                },
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Payer = new Payer
                {
                    BillingAddress = new Address
                    {
                        PostalCode = "76132"
                    }
                }
            };

            var response = await _client.AuthorizeAmountAsync(authorization);
            Assert.IsNotNull(response.TransactionId);
            Assert.IsNotNull(response.AuthCode);

            var voidRequest = new VoidRequest
            {
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                TransactionId = response.TransactionId
            };

            var voidResponse = await _client.VoidTransactionAsync(voidRequest);
            Assert.IsNotNull(voidResponse.TransactionId);
        }
    }
}
