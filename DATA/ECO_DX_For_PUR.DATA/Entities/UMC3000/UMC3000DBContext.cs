using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ECO_DX_For_PUR.DATA.Entities.UMC3000
{
    public partial class UMC3000DBContext : DbContext
    {
        public UMC3000DBContext()
            : base("name=UMC3000DBContext")
        {
        }

        public virtual DbSet<ENG_ECOHDRM> ENG_ECOHDRM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
