using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MCompanyWig
    {
        public MCompanyWig()
        {
            MDepartmentWig = new HashSet<MDepartmentWig>();
        }

        public uint CompanyWigId { get; set; }
        public string CompanyWigName { get; set; }
        public string CompanyWigDescription { get; set; }
        public byte CompanyWigValue { get; set; }
        public uint CompanyWigSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }
        public uint? CompanyBigWigId { get; set; }

        public virtual MCompanyBigWig CompanyBigWig { get; set; }
        public virtual ICollection<MDepartmentWig> MDepartmentWig { get; set; }
    }
}
