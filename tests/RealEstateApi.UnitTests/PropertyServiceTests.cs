using Microsoft.Extensions.Options;
using Moq;
using RealEstateApi.HttpClients;
using RealEstateApi.Services;
using Refit;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;

namespace RealEstateApi.UnitTests
{
    public class PropertyServiceTests
    {
        [Fact]
        public async Task GetSoldProperties_Returns_Results()
        {
            // Arrange
            var content = File.ReadAllText("example_response_success.xml");
            var serializerSettings = new XmlContentSerializerSettings { XmlNamespaces = new XmlSerializerNamespaces() };
            var xmlContentSerializer = new XmlContentSerializer(serializerSettings);
            var dto = await xmlContentSerializer.DeserializeAsync<SearchResults>(new StringContent(content));
            var mockZillowApi = new Mock<IZillowApi>();
            mockZillowApi.Setup(api => api.GetSearchResults(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(dto);
            var settings = Options.Create(new ZillowApiSettings { ApiKey = "ApiKey", SiteUrl = "http://test.com" });
            var sut = new PropertyService(mockZillowApi.Object, settings);

            // Act
            var result = await sut.GetSoldProperties(new GetSoldPropertiesRequest());

            // Assert
            var properties = dto.Response.Results.Properties;
            Assert.NotNull(properties);
            Assert.Single(properties);
            Assert.Equal(48749425, properties.Single().Zpid);
        }
    }
}
