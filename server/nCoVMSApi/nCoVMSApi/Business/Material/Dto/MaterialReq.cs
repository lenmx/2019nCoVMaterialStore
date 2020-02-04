using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Material.Dto
{
    public class MaterialReq
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public int MaxOrderNum { get; set; }
        public int IpmaxOrderNum { get; set; }
        public int TotalCount { get; set; }
    }
}
