namespace ECO_DX_For_PUR.DATA.Entities.ECN_ECO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WochangingInfoAdvanced")]
    public class WochangingInfoAdvanced
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [StringLength(100)]
        public string ECO_NO { get; set; }

        [StringLength(100)]
        public string CUSTOMER { get; set; }

        [StringLength(100)]
        public string MODEL { get; set; }
    }
}
