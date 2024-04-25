namespace ECO_DX_For_PUR.DATA.Entities.ECN_ECO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("HostLog")]
    public partial class HostLog
    {
        [Key]
        [Column("SESSION_KEY")]
        public Guid SESSION_KEY { get; set; }

        [StringLength(50)]
        public string HOST_NAME { get; set; }

        public DateTime LOG_TIME { get; set; }
    }
}
