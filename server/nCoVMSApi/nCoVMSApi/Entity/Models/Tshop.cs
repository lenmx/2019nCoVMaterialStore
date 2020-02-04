using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nCoVMSApi.Entity.Models
{
    [Table("TShop")]
    public partial class Tshop
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(200)]
        public string SecurityCode { get; set; }
        public int FailLoginTime { get; set; }
        public int Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastLoginTime { get; set; }
    }
}
