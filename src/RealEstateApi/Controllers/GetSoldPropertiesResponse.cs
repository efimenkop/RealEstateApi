using RealEstateApi.HttpClients;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateApi.Contracts
{
    public class GetSoldPropertiesResponse
    {
        public IEnumerable<Property> Properties { get; set; } = Enumerable.Empty<Property>();
    }
}
