using RealEstateApi.HttpClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApi.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetSoldProperties(GetSoldPropertiesRequest request);
    }
}
