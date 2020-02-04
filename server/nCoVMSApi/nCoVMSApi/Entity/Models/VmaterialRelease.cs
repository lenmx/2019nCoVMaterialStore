using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nCoVMSApi.Entity.Models
{
    public partial class VmaterialRelease
    {
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int MaxOrderNum { get; set; }
        [Column("IPMaxOrderNum")]
        public int IpmaxOrderNum { get; set; }
        public int TotalCount { get; set; }
        public long ShopId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        public int PreCount { get; set; }
        public int ReleaseCount { get; set; }
    }
}
