using RealEstateApi.HttpClients;
using Refit;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;

namespace RealEstateApi.UnitTests
{
    public class XmlDeserializationTests
    {
        [Fact]
        public async Task CanDeserializeApiSuccessResponse()
        {
            // Arrange
            var content = File.ReadAllText("example_response_success.xml");
            var serializerSettings = new XmlContentSerializerSettings { XmlNamespaces = new XmlSerializerNamespaces() };
            var sut = new XmlContentSerializer(serializerSettings);

            // Act
            var dto = await sut.DeserializeAsync<SearchResults>(new StringContent(content));

            // Assert
            var properties = dto.Response.Results.Properties;
            Assert.NotNull(properties);
            Assert.Single(properties);
            Assert.Equal(48749425, properties.Single().Zpid);
        }

        [Fact]
        public async Task CanDeserializeApiErrorResponse()
        {
            // Arrange
            var content = File.ReadAllText("example_response_error.xml");
            var serializerSettings = new XmlContentSerializerSettings { XmlNamespaces = new XmlSerializerNamespaces() };
            var sut = new XmlContentSerializer(serializerSettings);

            // Act
            var dto = await sut.DeserializeAsync<SearchResults>(new StringContent(content));

            // Assert
            Assert.NotNull(dto.Message);
            Assert.Equal(6, dto.Message.Code);
            Assert.Equal("Error: this account is not authorized to execute this API call", dto.Message.Text);
        }
    }
}
