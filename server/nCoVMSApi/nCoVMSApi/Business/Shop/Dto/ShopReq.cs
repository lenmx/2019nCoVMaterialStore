using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Shop.Dto
{
    public class ShopReq
    {
    }

    public class ShopLoginReq
    { 
        [Required]
        public string Name { get; set; }
        [Required]
        public string SecurityCode { get; set; }
    }

}
