using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using NetkaCommitment.Data.EFModels;
using NetkaCommitment.Data.ViewModel;
using NetkaCommitment.Repository;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
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
        public bool InsertCommitment(TCommitmentViewModel obj)
        {
            return oCommitmentRepository.Insert(new TCommitment
            {
                UserId = obj.CreatedBy,
                CreatedBy = obj.CreatedBy,
                CommitmentLm = obj.DepartmentLmId,
                CommitmentName = obj.CommitmentName,
                CommitmentDescription = "",
                CommitmentRemark = "",
                CommitmentStartDate = obj.CommitmentStartDate, //DateTime.Now,
                CommitmentFinishDate = null,
                CommitmentIsDeleted = 0,
                CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.CreatedBy) ? "Watting for approve." : "In-Progress"
            });
        }
        public bool InsertCommitmentApprove(TApproveViewModel obj)
        {
            bool isUser = db.MUser.Any(t => t.UserId == obj.ApproveUserId);
            if (!isUser) 
            {
                return false;
            }

            foreach (uint commitmentId in obj.listCommitmentId) 
            {
                oCommitmentRepository.InsertTApprove(new TApprove
                {
                    ApproveType = obj.ApproveType,
                    ApproveStatus = obj.ApproveStatus,
                    ApproveRemark = obj.ApproveRemark,
                    CreatedDate = DateTime.Now,
                    CreatedBy = obj.CreatedBy,
                    UpdatedBy = null,
                    UpdatedDate = null,
                    IsDeleted = 0,
                    ApproveUserId = obj.ApproveUserId,
                    CommitmentId = commitmentId
                });
            }
            return true;
        }
        public bool UpdateCommitment(TCommitmentViewModel obj)
        {
            var oCommitment = oCommitmentRepository.Get().Where(t => t.CommitmentId == obj.CommitmentId && t.IsDeleted == 0).FirstOrDefault();
            if (oCommitment != null)
            {
                if (obj.CommitmentStatus == "Success") {
                    oCommitment.CommitmentFinishDate = DateTime.Now;
                    oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Waiting for " + obj.CommitmentStatus + " re-approve." : obj.CommitmentStatus;
                    return oCommitmentRepository.Update(oCommitment);
                } else if (obj.CommitmentStatus == "Fail") {
                    oCommitment.CommitmentRemark = obj.CommitmentRemark;
                    oCommitment.CommitmentStatus = obj.CommitmentStatus;
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
        public List<TCommitmentViewModel> GetCommitmentClosed()
        {
            return (from t in db.TCommitment
                    join lm in db.MDepartmentLm on t.CommitmentLm equals lm.LmId
                    join wig in db.MDepartmentWig on lm.DepartmentWigId equals wig.DepartmentWigId 
                    where !(t.IsDeleted == 1)
                    select new TCommitmentViewModel
                    {
                        DepartmentWigName = wig.DepartmentWigName,
                        DepartmentLmName = lm.LmName,
                        CommitmentId = t.CommitmentId,
                        DepartmentWigSequence = wig.DepartmentWigSequence,
                        LmSequence = lm.LmSequence,
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
        public List<TCommitmentViewModel> GetTCommitment() 
        { 
            return (from t in db.TCommitment
                    join lm in db.MDepartmentLm on t.CommitmentLm equals lm.LmId
                    join wig in db.MDepartmentWig on lm.DepartmentWigId equals wig.DepartmentWigId
                    where !(t.IsDeleted==1)
                    select new TCommitmentViewModel
                    {
                        DepartmentWigName = wig.DepartmentWigName,
                        DepartmentLmName = lm.LmName,
                        CommitmentId = t.CommitmentId,
                        DepartmentWigSequence = wig.DepartmentWigSequence,
                        LmSequence = lm.LmSequence,
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
        public List<TCommitmentSummaryViewModel> GetCommitmentSummary(TCommitmentSummaryViewModel obj)
        {
            return db.MDepartmentLm.Select(t => new TCommitmentSummaryViewModel
            {
                CompanyWigId = t.DepartmentWig.CompanyLm.CompanyWig.CompanyWigId,
                CompanyWigName = t.DepartmentWig.CompanyLm.CompanyWig.CompanyWigName,
                CompanyLmId = t.DepartmentWig.CompanyLm.CompanyWig.CompanyWigId,
                CompanyLmName = t.DepartmentWig.CompanyLm.CompanyLmName,
                DepartmentWigId = t.DepartmentWigId,
                DepartmentWigName = t.DepartmentWig.DepartmentWigName,
                DepartmentLmId = t.LmId,
                DepartmentLmName = t.LmName,
                CreatedBy = t.CreatedBy,
                CommitmentCount = t.TCommitment.Count(),
            }).Where(x => x.CreatedBy==obj.CreatedBy).ToList();
        }

        //Not use
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


    }
}
