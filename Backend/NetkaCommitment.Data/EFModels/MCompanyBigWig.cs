using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MCompanyBigWig
    {
        public MCompanyBigWig()
        {
            MCompanyWig = new HashSet<MCompanyWig>();
        }

        public uint CompanyBigWigId { get; set; }
        public string CompanyBigWigName { get; set; }
        public ushort CompanyBigWigYear { get; set; }
        public string CompanyBigWigDescription { get; set; }
        public uint CompanyBigWigSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }

        public virtual ICollection<MCompanyWig> MCompanyWig { get; set; }
    }
}
