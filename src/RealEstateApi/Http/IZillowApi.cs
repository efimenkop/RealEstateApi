using RealEstateApi.HttpClients;
using Refit;
using System.Threading.Tasks;

namespace RealEstateApi.Services
{
    public interface IZillowApi
    {
        [Get("/webservice/GetDeepSearchResults.htm")]
        Task<SearchResults> GetDeepSearchResults([AliasAs("zws-id")] string apiKey, string citystatezip, string address);
    }
}