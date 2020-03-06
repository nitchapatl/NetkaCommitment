using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TCommitment
    {
        public TCommitment()
        {
            TApprove = new HashSet<TApprove>();
        }

        public uint CommitmentId { get; set; }
        public uint CommitmentLm { get; set; }
        public uint CommitmentNo { get; set; }
        public string CommitmentName { get; set; }
        public string CommitmentDescription { get; set; }
        public string CommitmentRemark { get; set; }
        public DateTime CommitmentStartDate { get; set; }
        public DateTime? CommitmentFinishDate { get; set; }
        public ulong CommitmentIsDeleted { get; set; }
        public string CommitmentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }

        public virtual MDepartmentLm CommitmentLmNavigation { get; set; }
        public virtual ICollection<TApprove> TApprove { get; set; }
    }
}
