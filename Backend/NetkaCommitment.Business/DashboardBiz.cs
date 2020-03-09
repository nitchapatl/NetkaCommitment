using NetkaCommitment.Data.ViewModel;
using NetkaCommitment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetkaCommitment.Business
{
    public class DashboardBiz : BaseBiz
    {
        public DashboardBiz() {
        }

        public List<DashboardLMGraphViewModel> GetCompanyLM(uint wigID)
        {
            return (from lm in db.MCompanyLm
                    where lm.CompanyWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.CompanyLmId,
                        LmName = lm.CompanyLmName,
                        LmValue = lm.MDepartmentWig.Sum(z => z.MDepartmentLm.Count())
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetCompanyWIG() {
            return (from wig in db.MCompanyWig
                    where wig.CompanyWigYear == DateTime.Now.Year
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.CompanyWigId,
                        WigName = wig.CompanyWigName,
                        WigValue = wig.MCompanyLm.Sum(x=>x.MDepartmentWig.Sum(z=>z.MDepartmentLm.Count())),
                        LmList = (from lm in db.MCompanyLm
                                  where lm.CompanyWigId == wig.CompanyWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.CompanyLmId,
                                      LmName = lm.CompanyLmName,
                                      LmValue = lm.MDepartmentWig.Sum(z=>z.MDepartmentLm.Count())
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardLMGraphViewModel> GetDepartmentLM(uint wigID)
        {
            return (from lm in db.MDepartmentLm
                    where lm.DepartmentWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.LmId,
                        LmName = lm.LmName,
                        LmValue = lm.TCommitment.Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetDepartmentWIG(uint departmentID)
        {
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == departmentID
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.DepartmentWigId,
                        WigName = wig.DepartmentWigName,
                        WigValue = wig.MDepartmentLm.Sum(t => t.TCommitment.Count()),
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName,
                                      LmValue = lm.TCommitment.Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetCommitment()
        {
            return db.TCommitment.Select(t=> new DashboardCommitmentViewModel {
                CommitmentId = t.CommitmentId,
                DepartmentLmId = 0,
                DepartmentWigId = 0,
                CompanyLmId = 0,
                CompanyWigId = 0,
                DepartmentId = 0,
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
