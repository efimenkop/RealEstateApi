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
        private const string ApiKey = "api-key";
        private const string SiteUrl = "http://www.zillow.com";
        private const string City = "Union";
        private const string State = "NJ";
        private const string Address = "2277 Copper Hill Dr";

        [Fact]
        public async Task GetSoldProperties_Returns_Results()
        {
            // Arrange
            var sut = await CreateSut();

            // Act
            var properties = await sut.GetSoldProperties(new GetSoldPropertiesRequest { Address = Address, City = City, Country = State, PageSize = 10 });

            // Assert
            Assert.Single(properties);
            Assert.Equal(40083537, properties.Single().Zpid);
        }

        private async Task<PropertyService> CreateSut()
        {
            var content = File.ReadAllText("example_response_success.xml");
            var serializerSettings = new XmlContentSerializerSettings { XmlNamespaces = new XmlSerializerNamespaces() };
            var xmlContentSerializer = new XmlContentSerializer(serializerSettings);
            var dto = await xmlContentSerializer.DeserializeAsync<SearchResults>(new StringContent(content));
            var mockZillowApi = new Mock<IZillowApi>();
            mockZillowApi.Setup(api => api.GetDeepSearchResults(It.IsAny<string>(), $"{City}, {State}", Address))
                .ReturnsAsync(dto);

            var settings = Options.Create(new ZillowApiSettings { ApiKey = ApiKey, SiteUrl = SiteUrl });

            return new PropertyService(mockZillowApi.Object, settings);
        }
    }
}
