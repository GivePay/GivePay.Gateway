namespace GivePay.Gateway
{
    internal static class Urls
    {
        public static string TransactionsApiV1Sale = "api/v1/transactions/sale";

        public static string TransactionsApiV1Authorize = "api/v1/transactions/authorize";

        public static string TransactionsApiV1Capture = "api/v1/transactions/capture";

        public static string TransactionsApiV1LookUp(string mid, string transactionId) => $"api/v1/transactions/lookup/{mid}/transactions/{transactionId}";

        public static string TransactionsApiV1Void = "api/v1/transactions/void";

        public static string TransactionsApiV1Tokenize = "api/v1/transactions/tokenize";

        public static string TokenEndpoint = "/connect/token";
    }
}
