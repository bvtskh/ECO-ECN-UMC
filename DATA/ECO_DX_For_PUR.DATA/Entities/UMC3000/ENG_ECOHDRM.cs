namespace ECO_DX_For_PUR.DATA.Entities.UMC3000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENG_ECOHDRM
    {
        [Key]
        [StringLength(20)]
        public string ECO_NO { get; set; }

        public DateTime? ECO_DATE { get; set; }

        [StringLength(30)]
        public string PART_NO { get; set; }

        [StringLength(50)]
        public string CUS_CODE { get; set; }

        [StringLength(20)]
        public string ACT_USER { get; set; }

        [StringLength(20)]
        public string CHG_LOTNO { get; set; }

        [StringLength(50)]
        public string CHG_BASIS { get; set; }

        [StringLength(10)]
        public string OLD_PARTS { get; set; }

        [StringLength(20)]
        public string ECO_ORIGN { get; set; }

        [StringLength(20)]
        public string ECO_STATE { get; set; }

        [StringLength(20)]
        public string UPD_USER { get; set; }

        public DateTime? UPD_DATE { get; set; }

        [StringLength(40)]
        public string ATT_DOC { get; set; }

        [StringLength(100)]
        public string ROUT_ADD { get; set; }

        [StringLength(100)]
        public string PART_ADD { get; set; }

        [StringLength(100)]
        public string PART_DEL { get; set; }
    }
}
