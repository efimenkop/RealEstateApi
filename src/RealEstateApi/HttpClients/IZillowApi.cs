using RealEstateApi.HttpClients;
using Refit;
using System.Threading.Tasks;

namespace RealEstateApi.Services
{
    public interface IZillowApi
    {
        [Get("/webservice/GetSearchResults.htm")]
        Task<SearchResults> GetSearchResults([AliasAs("zws-id")] string apiKey, string citystatezip, string address);
    }
}