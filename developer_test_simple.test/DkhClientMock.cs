using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using developer_test_simple.Interfaces;
using developer_test_simple.Models;

namespace developer_test_simple.test
{
    public class DkhClientMock : IDkhClient
	{
		private readonly ProductsResponse _bannerProducts;
		private readonly ProductsResponse _rsHughesProducts;
		public DkhClientMock(ProductsResponse bannerProducts, ProductsResponse rsHughesProducts) 
		{
			_bannerProducts = bannerProducts;
			_rsHughesProducts = rsHughesProducts;
		}
		public Task<ProductsResponse> GetBannerProducts(string token)
		{
			return Task.FromResult(_bannerProducts);
		}
		public Task<ProductsResponse> GetRSHughesProducts(string token)
		{
			return Task.FromResult(_rsHughesProducts);		
		}
	}
}
