using developer_test_simple.Services;
using System.Xml.Linq;

namespace developer_test_simple
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Get secrets from command line args
			string client_id = args[0];
			string client_secret = args[1];

			var tokenProvider = new TokenProvider();
			var token = await tokenProvider.GetAccessToken(client_id, client_secret);
			var dkhProductService = new DkhProductService(new DkhClient());
			var commonProducts = await dkhProductService.GetCommonProducts(token);

			if (!commonProducts.Any())
			{
				Console.WriteLine("No common product from RSHughes and Banner");
				return;
			}

			Console.WriteLine($"{" ",6}Banner {" ",32}| RsHughes");
			var count = 1;
			foreach (var pair in commonProducts)
			{
				var bannerProduct = pair.Key;
				var rsHughesProduct = pair.Value;
				Console.WriteLine($"{count++,4}) UPC: {bannerProduct.Upc,12} | ItemCode: {bannerProduct.ItemCode,8} " +
									  $"| UPC: {rsHughesProduct.Upc,12} | ItemCode: {rsHughesProduct.ItemCode,12}");
			}
		}
	}
}