using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Shop.Dto
{
    public class ShopRes
    {
    }

    public class ShopLoginRes
    {
        public string Token {get;set;}
        public ShopLoginRes_Shop Shop { get; set; }
    }

    public class ShopLoginRes_Shop
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public DateTime LastLoginTime { get; set; }
    }


}
