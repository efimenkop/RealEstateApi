using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Contracts;
using RealEstateApi.Services;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SoldPropertiesController : ControllerBase
    {
        private readonly IPropertyService _service;

        public SoldPropertiesController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<GetSoldPropertiesResponse> Get([FromQuery] GetSoldPropertiesRequest request)
        {
            var properties = await _service.GetSoldProperties(request);

            return new GetSoldPropertiesResponse { Properties = properties };
        }
    }
}
