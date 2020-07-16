using System;

namespace RealEstateApi.HttpClients
{
    [Serializable]
    public class ZillowApiException : Exception
    {
        public ZillowApiException(int code, string text)
            : base($"Zillow API returned error code: {code} - {text}")
        {
        }
    }
}
