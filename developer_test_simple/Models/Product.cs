using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace developer_test_simple.Models
{
	public class Product
    {
        [XmlElement("upc")]
        [JsonPropertyName("upc")]
        public string Upc { get; set; }

        [XmlElement("itemCode")]
        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; }

		[XmlElement("name")]
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[XmlElement("manufacturer")]
		[JsonPropertyName("manufacturer")]
		public string Manufacturer { get; set; }

		[XmlElement("mpn")]
		[JsonPropertyName("mpn")]
		public string Mpn { get; set; }

		[XmlElement("price")]
		[JsonPropertyName("price")]
		public double Price { get; set; }

		[XmlElement("brand")]
		[JsonPropertyName("brand")]
		public string Brand { get; set; }

	}
}