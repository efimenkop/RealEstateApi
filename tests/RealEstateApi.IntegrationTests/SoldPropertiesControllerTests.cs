using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace RealEstateApi.IntegrationTests
{
    public class SoldPropertiesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public SoldPropertiesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("Seattle", "WA", "2114 Bigelow Ave")]
        public async Task Get_EndpointsReturnSuccess(string city, string country, string address)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = BuildUrl(city, country, address);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData(null, "WA", "2114 Bigelow Ave")]
        [InlineData("Seattle", null, "2114 Bigelow Ave")]
        [InlineData("Seattle", "WA", null)]
        public async Task Get_EndpointsReturnBadRequest(string city, string country, string address)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = BuildUrl(city, country, address);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private string BuildUrl(string city, string country, string address)
        {
            return $"/soldproperties?city={Uri.EscapeUriString(city ?? string.Empty)}&country={Uri.EscapeUriString(country ?? string.Empty)}&address={Uri.EscapeUriString(address ?? string.Empty)}";
        }
    }
}
