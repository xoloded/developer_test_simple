using developer_test_simple.Interfaces;
using developer_test_simple.Models;
using System.Text.Json;
using System.Xml.Serialization;

namespace developer_test_simple.Services
{
    public class DkhProductService
    {
        private readonly IDkhClient _dkhClient;
        public DkhProductService(IDkhClient dkhClient)
        {
            _dkhClient = dkhClient;
        }

		public async Task<List<KeyValuePair<Product, Product>>> GetCommonProducts(string token)
		{
			var productsFromRsHughes = await _dkhClient.GetRSHughesProducts(token);
			var productsFromBanner = await _dkhClient.GetBannerProducts(token);

			var productsFromRsHughesWithUPC = new Dictionary<string,Product>();
			var productsFromRsHughesWithoutUPC = new Dictionary<ProductManufacturerMpnKey,Product>();

			foreach (var product in productsFromRsHughes.Products)
			{
				if (product.Upc != null)
				{
					productsFromRsHughesWithUPC.Add(product.Upc, product);
				}
				else if (product.Manufacturer != null && product.Mpn != null)
				{
					productsFromRsHughesWithoutUPC.Add(new ProductManufacturerMpnKey(product), product);
				}
			}

			var commonProducts = new List<KeyValuePair<Product, Product>>();
			foreach (var product in productsFromBanner.Products)
			{
				if (product.Upc != null && productsFromRsHughesWithUPC.TryGetValue(product.Upc, out Product rsHughesProduct))
				{
					commonProducts.Add(KeyValuePair.Create(product, rsHughesProduct));
				}
				else if (product.Manufacturer != null && product.Mpn != null && 
					productsFromRsHughesWithoutUPC.TryGetValue(new ProductManufacturerMpnKey(product), out rsHughesProduct))
				{
					commonProducts.Add(KeyValuePair.Create(product, rsHughesProduct));
				}
			}

			return commonProducts;
        }
	}
}