using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class Provider
    {
        public Provider()
        {
            ProviderName = "JSC Good Goods";
            ProviderVatNumber = "LT0256987";
            ProviderAdress = "Panerių g. 3, Vilnius, Lithuania";
        }
        public string ProviderName { get; set; }
        public string ProviderVatNumber { get; set; }
        public string ProviderAdress { get; set; }
    }
}
