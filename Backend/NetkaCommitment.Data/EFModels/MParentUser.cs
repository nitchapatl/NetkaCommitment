using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MParentUser
    {
        public uint ParentUserId { get; set; }
        public uint UserId { get; set; }
        public uint ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }

        public virtual MUser Parent { get; set; }
        public virtual MUser User { get; set; }
    }
}
