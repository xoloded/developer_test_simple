using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using developer_test_simple.Models;

namespace developer_test_simple.Interfaces
{
    public interface IDkhClient
    {
        Task<ProductsResponse> GetRSHughesProducts(string token);
        Task<ProductsResponse> GetBannerProducts(string token);

    }
}
