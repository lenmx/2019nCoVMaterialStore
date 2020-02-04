using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace nCoVMSApi.Entity.Models
{
    public partial class nCoVMSDBContext : DbContext
    {
        public nCoVMSDBContext()
        {
        }

        public nCoVMSDBContext(DbContextOptions<nCoVMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tmaterial> Tmaterial { get; set; }
        public virtual DbSet<Torder> Torder { get; set; }
        public virtual DbSet<Tshop> Tshop { get; set; }
        public virtual DbSet<VmaterialRelease> VmaterialRelease { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=120.77.169.141;Initial Catalog=nCoVMaterialStore;User ID=sa;Password=hotpoint@2019");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Torder>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("idxTOrder_Code");

                entity.HasIndex(e => e.IdcardNumber)
                    .HasName("idxTOrder_IDCardNumber");

                entity.HasIndex(e => e.Name)
                    .HasName("idxTOrder_Name");

                entity.HasIndex(e => new { e.IdcardNumber, e.CreateTime })
                    .HasName("idxTOrder_IDCardNumber_CreateTime");
            });

            modelBuilder.Entity<VmaterialRelease>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VMaterialRelease");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
