using System;
using System.Threading.Tasks;
using GivePay.Gateway.Ach.Client;
using GivePay.Gateway.Ach.Models;
using GivePay.Gateway.Builder;
using GivePay.Gateway.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GivePay.Gateway.Tests
{
    [TestClass]
    public class AchTests
    {
        private IAchClient _client;

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
                .BuildAchClient();
        }

        [TestMethod]
        public async Task ValidAchPosts_Test()
        {
            var ach = new AchRequest
            {
                Amount = 1000,
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Account = new BankAccountInformation
                {
                    Aba = "031000503",
                    AccountNumber = "1412423",
                    BankName = "A bank",
                    AccountVerification = new AccountVerification
                    {
                        AccountHolderName = "john doe"
                    }
                }
            };

            var response = await _client.PostAchAsync(ach);
            Assert.IsNotNull(response.TransactionReference);
            Assert.IsNotNull(response.AuthCode);
        }

        [TestMethod]
        public async Task ThrowsExceptionOnBadAba_Test()
        {
            var ach = new AchRequest
            {
                Amount = 1000,
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Account = new BankAccountInformation
                {
                    Aba = "131000503",
                    AccountNumber = "1412423",
                    BankName = "No bank",
                    AccountVerification = new AccountVerification
                    {
                        AccountHolderName = "john doe"
                    }
                }
            };

            var ex = await Assert.ThrowsExceptionAsync<AchTransactionException>(async () =>
            {
                var response = await _client.PostAchAsync(ach);
            });

            Assert.AreEqual("GP04", ex.Code);
        }

        [TestMethod]
        public async Task ThrowsGP01OnBadMid_Test()
        {
            var ach = new AchRequest
            {
                Amount = 1000,
                Mid = _merchantId + "0",
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Account = new BankAccountInformation
                {
                    Aba = "031000503",
                    AccountNumber = "1412423",
                    BankName = "No bank",
                    AccountVerification = new AccountVerification
                    {
                        AccountHolderName = "john doe"
                    }
                }
            };

            var ex = await Assert.ThrowsExceptionAsync<AchTransactionException>(async () =>
            {
                var response = await _client.PostAchAsync(ach);
            });

            Assert.AreEqual("GP01", ex.Code);
        }

        [TestMethod]
        public async Task MakesAndFindsATxn_Test()
        {
            var ach = new AchRequest
            {
                Amount = 1000,
                Mid = _merchantId,
                Terminal = new Terminal
                {
                    EntryType = EntryType.Keypad,
                    TerminalId = _terminalId,
                    TerminalType = TerminalType.ECommerce
                },
                Account = new BankAccountInformation
                {
                    Aba = "031000503",
                    AccountNumber = "1412423",
                    BankName = "No bank",
                    AccountVerification = new AccountVerification
                    {
                        AccountHolderName = "john doe"
                    }
                }
            };

            var response = await _client.PostAchAsync(ach);
            Assert.IsNotNull(response);

            var inq = await _client.InquireAchAsync(_merchantId, _terminalId, response.TransactionReference);
            Assert.AreEqual("Queued for Capture", inq.Status);
        }

        [TestMethod]
        public async Task ThrowsGp05OnTxnNotFound_Test()
        {
            var ex = await Assert.ThrowsExceptionAsync<AchTransactionException>(async () =>
            {
                var inq = await _client.InquireAchAsync(_merchantId, _terminalId, "65954872-6d75-47c8-a887-080540be2c5e");
            });

            Assert.AreEqual("GP05", ex.Code);
        }
    }
}
