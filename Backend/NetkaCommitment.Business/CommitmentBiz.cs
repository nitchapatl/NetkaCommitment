using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Data.ViewModel;
using NetkaCommitment.Repository;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetkaCommitment.Business
{
    public class CommitmentBiz : BaseBiz
    {
        private readonly ApproveRepository oApproveRepository = null;
        private readonly CommitmentRepository oCommitmentRepository = null;
        public CommitmentBiz()
        {
            oApproveRepository = new ApproveRepository();
            oCommitmentRepository = new CommitmentRepository();
        }

        public bool InsertCommitment()
        {
            return oCommitmentRepository.Insert(new TCommitment
            {
                CommitmentLm = 1,
                CommitmentNo = 1,
                CommitmentName = "",
                CommitmentDescription = "",
                CommitmentRemark = "",
                CommitmentStartDate = DateTime.Now,
                CommitmentFinishDate = null,
                CommitmentIsDeleted = 0,
                CommitmentStatus = db.MParentUser.Any(t => t.UserId == 11) ? "Watting for approve." : "In-Progress"
            });
        }
        public bool UpdateCommitment()
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == 1 && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == 11) ? "Watting for re-approve." : "In-Progress";
                return oCommitmentRepository.Update(oCommitment);
            }

            return false;
        }
        public bool PostponeCommitment()
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == 1 && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                oCommitment.CommitmentNo += 1;
                oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == 11) ? "Watting for postpone re-approve." : "In-Progress";
                return oCommitmentRepository.Update(oCommitment);
            }

            return false;
        }
        public bool DeleteCommitment()
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == 1 && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == 11) ? "Watting for delete re-approve." : "Deleted";
                return oCommitmentRepository.Delete(oCommitment);
            }

            return false;
        }
        public List<DepartmentWigDropdownlistViewModel> getDepartmentWig(uint departmentID)
        {
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == departmentID
                    select new DepartmentWigDropdownlistViewModel
                    {
                        WigID = wig.DepartmentWigId,
                        WigName = wig.DepartmentWigName,
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DepartmentLMDropdownlistViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName
                                  }).ToList()
                    }).ToList();
        }
    }
}
