using System;

namespace RealEstateApi.HttpClients
{
    [Serializable]
    public class ZillowApiException : Exception
    {
        public ResponseCodeType CodeType { get; }

        public ZillowApiException(int code, string text)
            : base($"Zillow API returned error code: {code} - {text}")
        {
            CodeType = code switch
            {
                1 => ResponseCodeType.ClientError,
                2 => ResponseCodeType.ClientError,
                3 => ResponseCodeType.ServerError,
                4 => ResponseCodeType.ServerError,
                500 => ResponseCodeType.ClientError,
                501 => ResponseCodeType.ClientError,
                502 => ResponseCodeType.Successful,
                503 => ResponseCodeType.ClientError,
                504 => ResponseCodeType.Successful,
                505 => ResponseCodeType.ServerError,
                506 => ResponseCodeType.ClientError,
                507 => ResponseCodeType.ClientError,
                508 => ResponseCodeType.Successful,
                _ => ResponseCodeType.ServerError,
            };
        }
    }

    public enum ResponseCodeType
    {
        Successful,
        ClientError,
        ServerError
    }
}
