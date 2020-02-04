using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Order.Dto
{
    public class OrderReq
    {
        public string Name { get; set; }
        public string IDCardNumber { get; set; }
        public long ShopId { get; set; }
        public long MaterialId { get; set; }
    }

    public class OrderReleaseReq
    { 
        public string Code { get; set; }
        public int Num { get; set; }
    }

}
