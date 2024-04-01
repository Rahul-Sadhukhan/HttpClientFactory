using System.Diagnostics.CodeAnalysis;

namespace WEIoT.EM.HttpClientFactory.TestAPI
{
    public static class RouteCollection
    {
        public static readonly string GetStoreProfile = "/api/stores";
    }
    [ExcludeFromCodeCoverage]
    public class StoreProfile
    {
        public string storeType { get; set; }
    }
    public class taskinfo
    {
        public string id { get; set; }
        public string partitionKey { get; set; }
    }
}
