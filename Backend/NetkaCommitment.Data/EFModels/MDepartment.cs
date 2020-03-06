using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MDepartment
    {
        public MDepartment()
        {
            MDepartmentWig = new HashSet<MDepartmentWig>();
            MUser = new HashSet<MUser>();
        }

        public uint DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public uint DepartmentSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }

        public virtual ICollection<MDepartmentWig> MDepartmentWig { get; set; }
        public virtual ICollection<MUser> MUser { get; set; }
    }
}
