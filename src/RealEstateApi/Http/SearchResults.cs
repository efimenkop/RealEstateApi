using System;
using System.Xml.Serialization;

namespace RealEstateApi.HttpClients
{
    [XmlRoot("searchresults", Namespace = "http://www.zillow.com/static/xsd/SearchResults.xsd")]
    public class SearchResults
    {
        [XmlElement("response", Namespace = "", IsNullable = true)]
        public Response Response { get; set; }

        [XmlElement("message", Namespace = "")]
        public Message Message { get; set; }
    }

    public class Response
    {
        [XmlElement("results")]
        public Results Results { get; set; }
    }

    public class Results
    {
        [XmlElement("result")]
        public Property[] Properties { get; set; } = Array.Empty<Property>();
    }

    public class Message
    {
        [XmlElement("text")]
        public string Text { get; set; }

        [XmlElement("code")]
        public int Code { get; set; }
    }

    public class Property
    {
        [XmlElement("zpid")]
        public int Zpid { get; set; }

        [XmlElement("bedrooms")]
        public int? Bedrooms { get; set; }

        [XmlElement("bathrooms")]
        public decimal? Bathrooms { get; set; }

        [XmlElement("lastSoldDate")]
        public string LastSoldDate { get; set; }

        [XmlElement("lastSoldPrice")]
        public decimal? LastSoldPrice { get; set; }

        [XmlElement("finishedSqFt")]
        public string FinishedSqFt { get; set; }

        [XmlElement("address")]
        public Address Address { get; set; }
    }

    public class Address
    {
        [XmlElement("street")]
        public string Street { get; set; }

        [XmlElement("zipcode")]
        public string Zipcode { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }
    }
}

