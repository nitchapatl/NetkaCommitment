using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MCompanyLm
    {
        public MCompanyLm()
        {
            MDepartmentWig = new HashSet<MDepartmentWig>();
        }

        public uint CompanyLmId { get; set; }
        public string CompanyLmName { get; set; }
        public string CompanyLmDescription { get; set; }
        public byte CompanyLmValue { get; set; }
        public uint CompanyLmSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }
        public uint? CompanyWigId { get; set; }

        public virtual MCompanyWig CompanyWig { get; set; }
        public virtual ICollection<MDepartmentWig> MDepartmentWig { get; set; }
    }
}
