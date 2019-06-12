# GivePay Gateway C#

[![Build status](https://ci.appveyor.com/api/projects/status/t6ijhamg9mvk266r?svg=true)](https://ci.appveyor.com/project/willseward/givepay-gateway)


A library for interacting with FlatRatePay transaction APIs with the GivePay Gateway.

## Installation

Install the library with [Nuget](https://nuget.org/):

|Package|Status|
|:------|:-----:|
|GivePay.Gateway|[![NuGet version](https://badge.fury.io/nu/GivePay.Gateway.svg)](https://badge.fury.io/nu/GivePay.Gateway)|
|GivePay.Gateway.OAuth|[![NuGet version](https://badge.fury.io/nu/GivePay.Gateway.OAuth.svg)](https://badge.fury.io/nu/GivePay.Gateway.OAuth)|
|GivePay.RestSharp.Newtonsoft|[![NuGet version](https://badge.fury.io/nu/GivePay.RestSharp.Newtonsoft.svg)](https://badge.fury.io/nu/GivePay.RestSharp.Newtonsoft)|

## Usage

_Simple sale transaction_

```c#
using GivePay.Gateway.Builder;
using GivePay.Gateway.Shared;
using GivePay.Gateway.Transactions;
using GivePay.Gateway.Transactions.Client;

// 1. Configure the transaction API client. Use the values provided in the portal for the variables below.
var _client = new DefaultGatewayClientBuilder()
    .WithBaseUri(new Uri(baseUrl))
    .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
    .BuildTransactionClient();

// 2. Create the Sale request
var saleRequest = new SaleRequest
{
    Amount = new Amount
    {
        // The transaction amount, in USD cents
        BaseAmount = 1000
    },
    Card = new Card
    {
        CardPresent = false,
        // The card's CVV/CVC/CVV2
        Cvv = "xxx",
        ExpirationMonth = "12",
        ExpirationYear = "21",
        // The card number
        Pan = "5105xxxxxxxx5100"
    },
    Mid = _merchantId,
    Terminal = new Terminal
    {
        // The method ofentry
        EntryType = EntryType.Keypad,

        // The terminal's ID
        TerminalId = _terminalId,

        // The type of terminal used
        TerminalType = TerminalType.ECommerce
    },
    Payer = new Payer
    {
        BillingAddress = new Address
        {
            // Billing information can be included
            // for extra fraud protection
            PostalCode = "76132"
        }
    }
};

// 3. Make a call to the API
var response = await _client.AuthorizeAndCaptureAmountAsync(saleRequest);
Console.WriteLine(response.TransactionId);
```

_Voiding a transaction_

```c#
using GivePay.Gateway.Builder;
using GivePay.Gateway.Shared;
using GivePay.Gateway.Transactions;
using GivePay.Gateway.Transactions.Client;

var _client = new DefaultGatewayClientBuilder()
    .WithBaseUri(new Uri(baseUrl))
    .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
    .BuildTransactionClient();

var voidRequest = new VoidRequest
{
    Mid = _merchantId,  // Your Merchant ID
    Terminal = new Terminal
    {
        // The method of entry
        EntryType = EntryType.Keypad,

        // The terminal's ID
        TerminalId = _terminalId,

        // The type of terminal used
        TerminalType = TerminalType.ECommerce
    },

    // The transaction ID from the previous transaction
    TransactionId = "<transaction ID>"
};

var response = await _client.VoidTransactionAsync(voidRequest);
Console.WriteLine(response.TransactionId);
```

_ACH Transaction_

```c#
using GivePay.Gateway.Ach.Client;
using GivePay.Gateway.Ach.Models;
using GivePay.Gateway.Builder;
using GivePay.Gateway.Shared;

var _client = new DefaultGatewayClientBuilder()
    .WithBaseUri(new Uri(baseUrl))
    .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
    .BuildAchClient();

var ach = new AchRequest
{
    Amount = 1000,  // $10
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
        BankName = "My State Bank",
        AccountVerification = new AccountVerification
        {
            AccountHolderName = "John Doe"
        }
    }
};

var response = await _client.PostAchAsync(ach);
Console.WriteLine(response.TransactionReference);
```


## License

`givepay/givepay-gateway` is licensed under the GPLv3 license.