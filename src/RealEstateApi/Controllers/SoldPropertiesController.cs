using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Contracts;
using RealEstateApi.HttpClients;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] GetSoldPropertiesRequest request)
        {
            var response = new GetSoldPropertiesResponse();

            try
            {
                response.Properties = await _service.GetSoldProperties(request);
            }
            catch (ZillowApiException e) when (e.CodeType == ResponseCodeType.Successful)
            {
                return Ok(response);
            }
            catch (ZillowApiException e) when (e.CodeType == ResponseCodeType.ClientError)
            {
                return BadRequest(e.Message);
            }

            return Ok(response);
        }
    }
}
