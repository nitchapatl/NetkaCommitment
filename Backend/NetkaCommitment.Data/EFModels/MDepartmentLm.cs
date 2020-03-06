using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MDepartmentLm
    {
        public MDepartmentLm()
        {
            TCommitment = new HashSet<TCommitment>();
        }

        public uint LmId { get; set; }
        public string LmName { get; set; }
        public string LmDescription { get; set; }
        public uint LmSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }
        public uint? DepartmentWigId { get; set; }

        public virtual MDepartmentWig DepartmentWig { get; set; }
        public virtual ICollection<TCommitment> TCommitment { get; set; }
    }
}
