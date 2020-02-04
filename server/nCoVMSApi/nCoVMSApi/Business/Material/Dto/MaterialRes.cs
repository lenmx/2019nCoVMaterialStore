using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Material.Dto
{
    public class MaterialRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public int MaxOrderNum { get; set; }
        public int IpmaxOrderNum { get; set; }
        public int TotalCount { get; set; }
        public long ShopId { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class MaterialReleaseRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public int MaxOrderNum { get; set; }
        public int IpmaxOrderNum { get; set; }
        public int TotalCount { get; set; }
        public long ShopId { get; set; }
        public DateTime CreateTime { get; set; }

        public int PreCount { get; set; }
        public int ReleaseCount { get; set; }
    }

}
