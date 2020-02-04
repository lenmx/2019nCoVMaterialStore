using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nCoVMSApi.Entity.Models
{
    [Table("TOrder")]
    public partial class Torder
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [Column("IDCardNumber")]
        [StringLength(50)]
        public string IdcardNumber { get; set; }
        public long ShopId { get; set; }
        public long MaterialId { get; set; }
        public int Num { get; set; }
        public int Status { get; set; }
        [Required]
        [Column("IP")]
        [StringLength(20)]
        public string Ip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
