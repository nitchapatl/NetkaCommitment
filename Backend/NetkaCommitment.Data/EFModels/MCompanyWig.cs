﻿using System;
using System.Collections.Generic;

namespace NetkaCommitment.Data.EFModels
{
    public partial class MCompanyWig
    {
        public MCompanyWig()
        {
            MCompanyLm = new HashSet<MCompanyLm>();
            MDepartmentWig = new HashSet<MDepartmentWig>();
        }

        public uint CompanyWigId { get; set; }
        public string CompanyWigName { get; set; }
        public ushort CompanyWigYear { get; set; }
        public string CompanyWigDescription { get; set; }
        public uint CompanyWigSequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public uint CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public uint? UpdatedBy { get; set; }
        public ulong IsDeleted { get; set; }

        public virtual ICollection<MCompanyLm> MCompanyLm { get; set; }
        public virtual ICollection<MDepartmentWig> MDepartmentWig { get; set; }
    }
}
