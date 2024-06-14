using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace developer_test_simple.Models
{
    public struct ProductManufacturerMpnKey
    {
        public string Manufacturer;
		public string Mpn;
        public ProductManufacturerMpnKey(Product product)
        {
            Manufacturer = product.Manufacturer;
            Mpn = product.Mpn;
        }
	}


}
