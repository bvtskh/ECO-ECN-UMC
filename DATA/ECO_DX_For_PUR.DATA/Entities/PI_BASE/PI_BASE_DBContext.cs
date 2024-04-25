using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ECO_DX_For_PUR.DATA.Entities.PI_BASE
{
    public partial class PI_BASE_DBContext : DbContext
    {
        public PI_BASE_DBContext()
            : base("name=PI_BASE_DBContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ModelMaster> ModelMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelMaster>()
                .Property(e => e.MODEL_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ModelMaster>()
                .Property(e => e.CUSTOMER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<ModelMaster>()
                .Property(e => e.CUSTOMER_SAP)
                .IsUnicode(false);
        }
    }
}
