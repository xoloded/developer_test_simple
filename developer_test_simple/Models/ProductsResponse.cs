using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace developer_test_simple.Models
{
	[XmlRoot("ProductsResponse")]
	public class ProductsResponse
    {
        [XmlElement("product")]
        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }
    }
}