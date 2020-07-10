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
                        LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId).Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetCompanyWIG() {
            return (from wig in db.MCompanyWig
                    where wig.CompanyWigYear == DateTime.Now.Year
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.CompanyWigId,
                        WigName = wig.CompanyWigName,
                        WigValue = db.TCommitment.Where(t=>t.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyWigId == wig.CompanyWigId).Count(),
                        LmList = (from lm in db.MCompanyLm
                                  where lm.CompanyWigId == wig.CompanyWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.CompanyLmId,
                                      LmName = lm.CompanyLmName,
                                      LmValue = db.TCommitment.Where(t=>t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId).Count()
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
                        LmValue = db.TCommitment.Where(t=>t.CommitmentLm == lm.LmId).Count()
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
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == wig.DepartmentWigId).Count(),
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId).Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetCommitment()
        {
            return db.TCommitment.Select(t=> new DashboardCommitmentViewModel {
                CommitmentId = t.CommitmentId,
                DepartmentLmId = t.CommitmentLmNavigation.LmId,
                DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == t.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
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


        public List<DashboardLMGraphViewModel> GetDoneCompanyLM(uint wigID)
        {
            return (from lm in db.MCompanyLm
                    where lm.CompanyWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.CompanyLmId,
                        LmName = lm.CompanyLmName,
                        LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId && t.CommitmentStatus == "Done").Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetDoneCompanyWIG()
        {
            return (from wig in db.MCompanyWig
                    where wig.CompanyWigYear == DateTime.Now.Year
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.CompanyWigId,
                        WigName = wig.CompanyWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyWigId == wig.CompanyWigId && t.CommitmentStatus == "Done").Count(),
                        LmList = (from lm in db.MCompanyLm
                                  where lm.CompanyWigId == wig.CompanyWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.CompanyLmId,
                                      LmName = lm.CompanyLmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId && t.CommitmentStatus == "Done").Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardLMGraphViewModel> GetDoneDepartmentLM(uint wigID)
        {
            return (from lm in db.MDepartmentLm
                    where lm.DepartmentWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.LmId,
                        LmName = lm.LmName,
                        LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId && t.CommitmentStatus == "Done").Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetDoneDepartmentWIG(uint departmentID)
        {
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == departmentID
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.DepartmentWigId,
                        WigName = wig.DepartmentWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == wig.DepartmentWigId && t.CommitmentStatus == "Done").Count(),
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId && t.CommitmentStatus == "Done").Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetDoneCommitment()
        {
            return db.TCommitment.Where(t=>t.CommitmentStatus == "Done").Select(t => new DashboardCommitmentViewModel
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



        public List<DashboardLMGraphViewModel> GetFailCompanyLM(uint wigID)
        {
            return (from lm in db.MCompanyLm
                    where lm.CompanyWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.CompanyLmId,
                        LmName = lm.CompanyLmName,
                        LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId && t.CommitmentStatus == "Fail").Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetFailCompanyWIG()
        {
            return (from wig in db.MCompanyWig
                    where wig.CompanyWigYear == DateTime.Now.Year
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.CompanyWigId,
                        WigName = wig.CompanyWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyWigId == wig.CompanyWigId && t.CommitmentStatus == "Fail").Count(),
                        LmList = (from lm in db.MCompanyLm
                                  where lm.CompanyWigId == wig.CompanyWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.CompanyLmId,
                                      LmName = lm.CompanyLmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.CompanyLmId == lm.CompanyLmId && t.CommitmentStatus == "Fail").Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardLMGraphViewModel> GetFailDepartmentLM(uint wigID)
        {
            return (from lm in db.MDepartmentLm
                    where lm.DepartmentWigId == wigID
                    select new DashboardLMGraphViewModel
                    {
                        LmID = lm.LmId,
                        LmName = lm.LmName,
                        LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId && t.CommitmentStatus == "Fail").Count()
                    }).ToList();
        }

        public List<DashboardWIGGraphViewModel> GetFailDepartmentWIG(uint departmentID)
        {
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == departmentID
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.DepartmentWigId,
                        WigName = wig.DepartmentWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == wig.DepartmentWigId && t.CommitmentStatus == "Fail").Count(),
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId && t.CommitmentStatus == "Fail").Count()
                                  }).ToList()
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetFailCommitment()
        {
            return db.TCommitment.Where(t => t.CommitmentStatus == "Fail").Select(t => new DashboardCommitmentViewModel
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
