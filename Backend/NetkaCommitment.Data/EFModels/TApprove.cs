using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class TApprove
    {
        public uint ApproveId { get; set; }
        public uint ApproveNo { get; set; }
        public string ApproveType { get; set; }
        public string ApproveStatus { get; set; }
        public string ApproveRemark { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public uint CommitmentId { get; set; }

        public virtual TCommitment Commitment { get; set; }
    }
}
