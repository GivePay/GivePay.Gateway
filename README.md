# GivePay Gateway C#

[![Build Status](https://travis-ci.org/GivePay/GivePay.Gateway.svg?branch=master)](https://travis-ci.org/GivePay/GivePay.Gateway)

A library for interacting with FlatRatePay transactions APIs with the GivePay Gateway.

## Installation

Install the library with [Nuget](https://nuget.org/):

|Package|Status|
|:------|:-----:|
|GivePay.Gateway|[![NuGet version](https://badge.fury.io/nu/GivePay.Gateway.svg)](https://badge.fury.io/nu/GivePay.Gateway)|
|GivePay.Gateway.OAuth|[![NuGet version](https://badge.fury.io/nu/GivePay.Gateway.OAuth.svg)](https://badge.fury.io/nu/GivePay.Gateway.OAuth)|
|GivePay.RestSharp.Newtonsoft|[![NuGet version](https://badge.fury.io/nu/GivePay.RestSharp.Newtonsoft.svg)](https://badge.fury.io/nu/GivePay.RestSharp.Newtonsofth)|

## Usage

Simple sale transaction example:

```c#
using GivePay.Gateway.Transactions;
using GivePay.Gateway.Transactions.Client;

var _client = new DefaultGatewayClientBuilder()
    .WithBaseUri(new Uri(baseUrl))
    .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
    .Build();

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

var response = await _client.AuthorizeAndCaptureAmountAsync(saleRequest);
Console.WriteLine(response.TransactionId);
```

Voiding a transaction

```c#
using GivePay.Gateway.Transactions;
using GivePay.Gateway.Transactions.Client;

var _client = new DefaultGatewayClientBuilder()
    .WithBaseUri(new Uri(baseUrl))
    .WithOAuthCredentials(new Uri(authority), clientId, clientSecret, scopes)
    .Build();

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

## License

`givepay/givepay-gateway` is licensed under the GPLv3 license.