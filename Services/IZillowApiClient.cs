using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IZillowClient
    {
        Task<IEnumerable<Property>> GetSearchResults(string country, string city);
    }
}