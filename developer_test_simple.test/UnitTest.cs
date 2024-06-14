using developer_test_simple.Models;
using developer_test_simple.Services;
using Newtonsoft.Json.Linq;

namespace developer_test_simple.test
{
    public class Tests
	{
		[Test]
		public async Task ShouldReturnCommonProducts()
		{
			var bannerProducts = new ProductsResponse
			{
				Products = new List<Product>
					{
						new Product
						{
							Upc = "a",
							ItemCode = "a",
							Manufacturer = "a",
							Mpn = "a"
						},
						new Product
						{
							Upc = "a1",
							ItemCode = "a1",
							Manufacturer = "a1",
							Mpn = "a1"
						},
						new Product
						{
							Upc = null,
							ItemCode = "b",
							Manufacturer = "b",
							Mpn = "b"
						}
					}
			};
			var rsHughesProducts = new ProductsResponse
			{
				Products = new List<Product>
					{
						new Product
						{
							Upc = "a2",
							ItemCode = "a2",
							Manufacturer = "a2",
							Mpn = "a2"
						},
						new Product
						{
							Upc = "a",
							ItemCode = "a",
							Manufacturer = "a",
							Mpn = "a"
						},
						new Product
						{
							Upc = null,
							ItemCode = "b",
							Manufacturer = "b",
							Mpn = "b"
						}
					}
			};
			var dkhProductService = new DkhProductService(new DkhClientMock(bannerProducts, rsHughesProducts));
			var commonProducts = await dkhProductService.GetCommonProducts("test");

			Assert.That(commonProducts.Count, Is.EqualTo(2));
			Assert.That(commonProducts[0].Key.Upc, Is.EqualTo("a"));
			Assert.That(commonProducts[1].Key.Manufacturer, Is.EqualTo("b"));
		}

		[Test]
		public async Task ShouldReturnEmpty()
		{
			var bannerProducts = new ProductsResponse
			{
				Products = new List<Product>
					{
						new Product
						{
							Upc = "a",
							ItemCode = "a",
							Manufacturer = "a",
							Mpn = "a"
						},
						new Product
						{
							Upc = null,
							ItemCode = "b",
							Manufacturer = "b",
							Mpn = "g"
						}
					}
			};
			var rsHughesProducts = new ProductsResponse
			{
				Products = new List<Product>
					{
						new Product
						{
							Upc = "c",
							ItemCode = "c",
							Manufacturer = "c",
							Mpn = "c"
						},
						new Product
						{
							Upc = null,
							ItemCode = "d",
							Manufacturer = "b",
							Mpn = "d"
						}
					}
			};
			var dkhProductService = new DkhProductService(new DkhClientMock(bannerProducts, rsHughesProducts));
			var commonProducts = await dkhProductService.GetCommonProducts("test");

			Assert.That(commonProducts.Any, Is.False);
		}

		[Test]
		public async Task ShouldReturnEmptyWithNullProductsResponse()
		{
			var bannerProducts = new ProductsResponse() { Products = new List<Product>() };
			var rsHughesProducts = new ProductsResponse() { Products = new List<Product>() };

			var dkhProductService = new DkhProductService(new DkhClientMock(bannerProducts, rsHughesProducts));
			var commonProducts = await dkhProductService.GetCommonProducts("test");

			Assert.That(commonProducts.Any(), Is.False);
		}
	}
}