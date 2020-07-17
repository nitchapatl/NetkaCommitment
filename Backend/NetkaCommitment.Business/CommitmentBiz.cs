using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public bool InsertCommitment(uint lmId,string commitmentName)
        {
            return oCommitmentRepository.Insert(new TCommitment
            {
                UserId = 11,

                CommitmentLm = lmId,
                CommitmentName = commitmentName,
                CommitmentDescription = "",
                CommitmentRemark = "",
                CommitmentStartDate = DateTime.Now,
                CommitmentFinishDate = null,
                CommitmentIsDeleted = 0,
                CommitmentStatus = db.MParentUser.Any(t => t.UserId == 11) ? "Watting for approve." : "In-Progress"
            });
        }
        public bool UpdateCommitment(TCommitmentViewModel obj)
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == obj.CommitmentId && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                if (obj.CommitmentStatus=="done") {
                    oCommitment.CommitmentFinishDate = DateTime.Now;
                    oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Watting for done re-approve." : "Done";
                    return oCommitmentRepository.Update(oCommitment);
                } else if (obj.CommitmentStatus=="fail") {
                    oCommitment.CommitmentRemark = obj.CommitmentRemark;
                    oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Watting for fail re-approve." : "Failed";
                    return oCommitmentRepository.Update(oCommitment);
                }
            }

            return false;
        }
        public bool PostponeCommitment(TCommitmentViewModel obj) //uint commitmentId, uint? updatedBy
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == obj.CommitmentId && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                //oCommitment.CommitmentNo += 1;
                oCommitment.CommitmentStartDate = obj.CommitmentStartDate;
                oCommitment.CommitmentRemark = obj.CommitmentRemark;
                oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Watting for postpone re-approve." : "Postpone";
                return oCommitmentRepository.Update(oCommitment);
            }

            return false;
        }
        public bool DeleteCommitment(TCommitmentViewModel obj)
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == obj.CommitmentId && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                oCommitment.CommitmentRemark = obj.CommitmentRemark;
                oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Watting for delete re-approve." : "Deleted";
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
        public List<TCommitmentViewModel> GetCommitment()
        {
            return db.TCommitment.Select(t => new TCommitmentViewModel
            {
                CommitmentId = t.CommitmentId,
                DepartmentLmId = t.CommitmentLmNavigation.LmId,
                DepartmentWigId = t.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                CompanyLmId = t.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                CompanyWigId = t.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                DepartmentId = t.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                CommitmentNo = t.CommitmentNo,
                CommitmentName = t.CommitmentName,
                CommitmentDescription = t.CommitmentDescription,
                CommitmentRemark = t.CommitmentRemark,
                CommitmentStartDate = t.CommitmentStartDate,
                CommitmentFinishDate = t.CommitmentFinishDate,
                CommitmentIsDeleted = t.CommitmentIsDeleted,
                CommitmentStatus = t.CommitmentStatus,
                CreatedDate = t.CreatedDate,
                CreatedBy = t.CreatedBy,
                UpdatedDate = t.UpdatedDate,
                UpdatedBy = t.UpdatedBy
            }).ToList();
        }


        public List<TCommitmentSummaryViewModel> GetCommitmentSummary()
        {
            return db.MDepartmentLm.Select(t => new TCommitmentSummaryViewModel
            {
                CompanyWigName = t.DepartmentWig.CompanyLm.CompanyWig.CompanyWigName,
                CompanyLmName = t.DepartmentWig.CompanyLm.CompanyLmName,
                DepartmentWigName = t.DepartmentWig.DepartmentWigName,
                DepartmentLmName = t.LmName,
                CommitmentCount = t.TCommitment.Count()
            }).ToList();
        }

    }
}
