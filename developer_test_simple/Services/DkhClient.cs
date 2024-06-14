using System.Net;
using System.Text.Json;
using System.Xml.Serialization;
using developer_test_simple.Interfaces;
using developer_test_simple.Models;

namespace developer_test_simple.Services
{
    public class DkhClient : IDkhClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private const string _productsFromRSHughesEndpoint = "https://dkh-c-testing-api.staging.dkhdev.com/products/json";
        private const string _productsFromBannerEndpoint = "https://dkh-c-testing-api.staging.dkhdev.com/products/xml";

        public async Task<ProductsResponse> GetRSHughesProducts(string token)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, _productsFromRSHughesEndpoint);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.SendAsync(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception("Unable to get products from RSHughes");
			}
			var responseMessage = await response.Content.ReadAsStringAsync();
            try
            {
				var productsFromRsHughes = JsonSerializer.Deserialize<ProductsResponse>(responseMessage);
				return productsFromRsHughes;
			}
            catch (Exception ex)
            {
                throw new Exception("Unable to deserialize products from RSHughes");
            }	
        }
        public async Task<ProductsResponse> GetBannerProducts(string token)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, _productsFromBannerEndpoint);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.SendAsync(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception("Unable to get products from Banner");
			}
			var responseMessage = await response.Content.ReadAsStreamAsync();
			var serializer = new XmlSerializer(typeof(ProductsResponse));
			try
			{
				var productsFromRsHughes = (ProductsResponse)serializer.Deserialize(responseMessage);
				return productsFromRsHughes;
			}
			catch (Exception ex)
			{
				throw new Exception("Unable to deserialize products from Banner");
			}
		}
    }
}