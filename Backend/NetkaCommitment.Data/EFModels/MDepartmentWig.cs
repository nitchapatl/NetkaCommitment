using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MDepartmentWig
    {
        public MDepartmentWig()
        {
            MDepartmentLm = new HashSet<MDepartmentLm>();
        }

        public uint DepartmentWigId { get; set; }
        public string DepartmentWigName { get; set; }
        public string DepartmentWigDescription { get; set; }
        public uint DepartmentWigSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }
        public uint? DepartmentId { get; set; }
        public uint? CompanyWigId { get; set; }
        public uint? CompanyLmId { get; set; }

        public virtual MCompanyLm CompanyLm { get; set; }
        public virtual MCompanyWig CompanyWig { get; set; }
        public virtual MDepartment Department { get; set; }
        public virtual ICollection<MDepartmentLm> MDepartmentLm { get; set; }
    }
}
