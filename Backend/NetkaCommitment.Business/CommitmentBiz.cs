using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                UserId = 11,

                CommitmentLm = obj.DepartmentLmId,
                CommitmentName = obj.CommitmentName,
                CommitmentDescription = "",
                CommitmentRemark = "",
                CommitmentStartDate = obj.CommitmentStartDate, //DateTime.Now,
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
                if (obj.CommitmentStatus == "Done") {
                    oCommitment.CommitmentFinishDate = DateTime.Now;
                    oCommitment.CommitmentStatus = db.MParentUser.Any(t => t.UserId == obj.UpdatedBy) ? "Waiting for " + obj.CommitmentStatus + " re-approve." : obj.CommitmentStatus;
                    return oCommitmentRepository.Update(oCommitment);
                } else if (obj.CommitmentStatus == "Fail") {
                    oCommitment.CommitmentRemark = obj.CommitmentRemark;
                    oCommitment.CommitmentStatus = obj.CommitmentStatus;
                    return oCommitmentRepository.Update(oCommitment);
                } else if (obj.CommitmentStatus=="In-Progress") {
                    oCommitment.CommitmentName = obj.CommitmentName;
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
        public List<TCommitmentViewModel> GetTCommitment() 
        { 
            return (from t in db.TCommitment
                    where t.CommitmentStatus== "In-Progress" //!(t.IsDeleted==1)
                    select new TCommitmentViewModel
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
        public List<DashboardWIGGraphViewModel> GetCompanyWIG()
        {
            return (from wig in db.MCompanyWig
                    where wig.CompanyWigYear == DateTime.Now.Year
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.CompanyWigId,
                        WigName = wig.CompanyWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyWigId == wig.CompanyWigId).Count(),
                        LmList = (from lm in db.MCompanyLm
                                  where lm.CompanyWigId == wig.CompanyWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.CompanyLmId,
                                      LmName = lm.CompanyLmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId).Count()
                                  }).ToList()
                    }).ToList();
        }
        public List<CommitmentGraphData> GetGraphDrillDown()

        {
            return (from wig in db.MDepartmentWig
                    join lm in db.MDepartmentLm on wig.DepartmentWigId equals lm.DepartmentWigId
                    //join t in db.TCommitment on t.CommitmentLm equals lm.LmId 
                    select new CommitmentGraphData
                    {
                        //y = t.CommitmentId,
                        //drilldown = t.CommitmentName
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
