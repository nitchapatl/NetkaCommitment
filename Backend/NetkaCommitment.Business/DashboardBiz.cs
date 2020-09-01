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

        public List<DashboardWIGGraphViewModel> GetDepartmentWIG(uint DepartmentId)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == DepartmentId
                          && wig.CreatedDate >= departmentwig_startdate 
                          && wig.CreatedDate <= departmentwig_enddate
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

        public List<DashboardWIGGraphViewModel> GetUserDepartmentWIG(uint UserId, uint DepartmentId)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);
            return (from wig in db.MDepartmentWig
                    where wig.DepartmentId == DepartmentId
                          && wig.CreatedDate >= departmentwig_startdate
                          && wig.CreatedDate <= departmentwig_enddate
                    select new DashboardWIGGraphViewModel
                    {
                        WigID = wig.DepartmentWigId,
                        WigName = wig.DepartmentWigName,
                        WigValue = db.TCommitment.Where(t => t.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == wig.DepartmentWigId)
                                                 .Where(t => t.UserId == UserId).Count(),
                        LmList = (from lm in db.MDepartmentLm
                                  where lm.DepartmentWigId == wig.DepartmentWigId
                                  select new DashboardLMGraphViewModel
                                  {
                                      LmID = lm.LmId,
                                      LmName = lm.LmName,
                                      LmValue = db.TCommitment.Where(t => t.CommitmentLm == lm.LmId)
                                                              .Where(t => t.UserId == UserId).Count()
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

        public List<DashboardCommitmentViewModel> GetCompanyCommitment()
        {
            return (from c in db.TCommitment
                    where c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigYear == DateTime.Now.Year
                          && c.CommitmentLmNavigation.DepartmentWig.CompanyWigId != null
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetCompanyCommitmentbyWig(uint WigID)
        {
            return (from c in db.TCommitment
                    where c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigYear == DateTime.Now.Year
                          && c.CommitmentLmNavigation.DepartmentWig.CompanyWigId == WigID
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetCompanyCommitmentbyLm(uint LmID)
        {
            return (from c in db.TCommitment
                    where c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigYear == DateTime.Now.Year
                          && c.CommitmentLmNavigation.DepartmentWig.CompanyLmId == LmID
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetDepartmentCommitment(uint DepartmentId)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);

            return (from c in db.TCommitment
                    join departmentlm in db.MDepartmentLm on c.CommitmentLm equals departmentlm.LmId into commitment_departmentlm
                    from departmentlm in commitment_departmentlm.DefaultIfEmpty()
                    join departmentw in db.MDepartmentWig on departmentlm.DepartmentWigId equals departmentw.DepartmentWigId into commitment_departmentw
                    from departmentw in commitment_departmentw.DefaultIfEmpty()
                    where departmentw.DepartmentId == DepartmentId
                          && departmentw.CreatedDate >= departmentwig_startdate
                          && departmentw.CreatedDate <= departmentwig_enddate
                          //&& companyw.CompanyWigYear == DateTime.Now.Year
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetUserDepartmentCommitment(uint UserId, uint DepartmentId)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);

            return (from c in db.TCommitment
                    join departmentlm in db.MDepartmentLm on c.CommitmentLm equals departmentlm.LmId into commitment_departmentlm
                    from departmentlm in commitment_departmentlm.DefaultIfEmpty()
                    join departmentw in db.MDepartmentWig on departmentlm.DepartmentWigId equals departmentw.DepartmentWigId into commitment_departmentw
                    from departmentw in commitment_departmentw.DefaultIfEmpty()
                    where departmentw.DepartmentId == DepartmentId
                          && departmentw.CreatedDate >= departmentwig_startdate
                          && departmentw.CreatedDate <= departmentwig_enddate
                          && c.UserId == UserId
                    //&& companyw.CompanyWigYear == DateTime.Now.Year
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetDepartmentCommitmentbyWig(uint WigID)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);

            return (from c in db.TCommitment
                    join departmentlm in db.MDepartmentLm on c.CommitmentLm equals departmentlm.LmId into commitment_departmentlm
                    from departmentlm in commitment_departmentlm.DefaultIfEmpty()
                    join departmentw in db.MDepartmentWig on departmentlm.DepartmentWigId equals departmentw.DepartmentWigId into commitment_departmentw
                    from departmentw in commitment_departmentw.DefaultIfEmpty()
                    where departmentw.DepartmentWigId == WigID
                          && departmentw.CreatedDate >= departmentwig_startdate
                          && departmentw.CreatedDate <= departmentwig_enddate
                    
                    //where c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigYear == DateTime.Now.Year
                    //&& c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == WigID
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetUserDepartmentCommitmentbyWig(uint UserId, uint WigID)
        {
            DateTime departmentwig_startdate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime departmentwig_enddate = new DateTime(DateTime.Now.Year, 12, 31);

            return (from c in db.TCommitment
                    join departmentlm in db.MDepartmentLm on c.CommitmentLm equals departmentlm.LmId into commitment_departmentlm
                    from departmentlm in commitment_departmentlm.DefaultIfEmpty()
                    join departmentw in db.MDepartmentWig on departmentlm.DepartmentWigId equals departmentw.DepartmentWigId into commitment_departmentw
                    from departmentw in commitment_departmentw.DefaultIfEmpty()
                    where departmentw.DepartmentWigId == WigID
                          && departmentw.CreatedDate >= departmentwig_startdate
                          && departmentw.CreatedDate <= departmentwig_enddate
                          && c.UserId == UserId
                    //where c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigYear == DateTime.Now.Year
                    //&& c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId == WigID
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetDepartmentCommitmentbyLm(uint LmID)
        {

            return (from c in db.TCommitment
                    where c.CommitmentLm == LmID
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
                    }).ToList();
        }

        public List<DashboardCommitmentViewModel> GetUserDepartmentCommitmentbyLm(uint UserId, uint LmID)
        {

            return (from c in db.TCommitment
                    where c.CommitmentLm == LmID
                          && c.UserId == UserId
                    select new DashboardCommitmentViewModel
                    {
                        CommitmentId = c.CommitmentId,
                        DepartmentLmId = c.CommitmentLmNavigation.LmId,
                        DepartmentLmName = db.MDepartmentLm.Where(lm => lm.LmId == c.CommitmentLm).Select(lm => lm.LmName).FirstOrDefault(),
                        DepartmentWigId = c.CommitmentLmNavigation.DepartmentWig.DepartmentWigId,
                        CompanyLmId = c.CommitmentLmNavigation.DepartmentWig.CompanyLm.CompanyLmId,
                        CompanyWigId = c.CommitmentLmNavigation.DepartmentWig.CompanyWig.CompanyWigId,
                        DepartmentId = c.CommitmentLmNavigation.DepartmentWig.Department.DepartmentId,
                        CommitmentNo = c.CommitmentNo,
                        CommitmentName = c.CommitmentName,
                        CommitmentDescription = c.CommitmentDescription,
                        CommitmentRemark = c.CommitmentRemark,
                        CommitmentStartDate = c.CommitmentStartDate,
                        CommitmentFinishDate = c.CommitmentFinishDate,
                        CommitmentIsDeleted = c.CommitmentIsDeleted,
                        CommitmentStatus = c.CommitmentStatus,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        UpdatedDate = c.UpdatedDate,
                        UpdatedBy = c.UpdatedBy
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
