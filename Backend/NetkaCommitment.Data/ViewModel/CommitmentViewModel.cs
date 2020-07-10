using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Data.ViewModel
{
    public class CommitmentViewModel
    {
    }

    public class DepartmentWigDropdownlistViewModel
    {
        public uint WigID { get; set; }
        public string WigName { get; set; }
        public List<DepartmentLMDropdownlistViewModel> LmList { get; set; }
    }
    public class DepartmentLMDropdownlistViewModel
    {
        public uint LmID { get; set; }
        public string LmName { get; set; }
    }
    public class TCommitmentViewModel
    {
        public uint CommitmentId { get; set; }
        public uint DepartmentLmId { get; set; }
        public uint DepartmentWigId { get; set; }
        public uint CompanyLmId { get; set; }
        public uint CompanyWigId { get; set; }
        public uint DepartmentId { get; set; }
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
        public uint IsDeleted { get; set; }
    }
}
