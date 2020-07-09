using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstate.Contracts;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IZillowClient _zillowClient;

        public PropertyController(ILogger<PropertyController> logger, IZillowClient zillowClient)
        {
            _logger = logger;
            _zillowClient = zillowClient;
        }

        [HttpGet]
        [Route("sold")]
        public async Task<IEnumerable<Property>> GetSoldProperties([FromQuery]GetSoldPropertiesRequest request)
        {
            var result = await _zillowClient.GetSearchResults(request.Country, request.City);

            return result;
        }
    }
}
