using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetkaCommitment.Business;
using NetkaCommitment.Common.Model;
using NetkaCommitment.Data.ViewModel;

namespace NetkaCommitment.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseApiController
    {
        private readonly DashboardBiz oDashboardBiz = null;
        public DashboardController(IHttpContextAccessor oHttpContextAccessor) : base(oHttpContextAccessor)
        {
            oDashboardBiz = new DashboardBiz();
        }

        /*[HttpGet("department/wig")]
        public IActionResult GetDepartmentWig()
        {
            var result = oDashboardBiz.GetDepartmentWIG(5);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }*/

        [HttpGet("department/lm")]
        public IActionResult GetDepartmentLm()
        {
            var result = oDashboardBiz.GetDepartmentLM(5);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("company/wig")]
        public IActionResult GetCompanyWig()
        {
            var result = oDashboardBiz.GetCompanyWIG();

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("company/lm")]
        public IActionResult GetCompanyLm()
        {
            var result = oDashboardBiz.GetCompanyLM(1);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("commitment")]
        public IActionResult GetCommitment()
        {
            var result = oDashboardBiz.GetCommitment();

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("company/commitment")]
        public IActionResult GetCompanyCommitment()
        {
            var result = oDashboardBiz.GetCompanyCommitment();

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("company/wig/commitment/{WigID}")]
        public IActionResult GetCompanyCommitmentbyWig(uint WigID)
        {
            var result = oDashboardBiz.GetCompanyCommitmentbyWig(WigID).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("company/lm/commitment/{LmID}")]
        public IActionResult GetCompanyCommitmentbyLm(uint LmID)
        {
            var result = oDashboardBiz.GetCompanyCommitmentbyLm(LmID).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("department/wig/{DepartmentId}")]
        public IActionResult GetDepartmentWig(uint DepartmentId)
        {
            var result = oDashboardBiz.GetDepartmentWIG(DepartmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        /*[HttpGet("department/commitment/{DepartmentId}")]
        public IActionResult GetDepartmentCommitment(uint DepartmentId)
        {
            var result = oDashboardBiz.GetDepartmentCommitment(DepartmentId).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }*/

        [HttpPost("department/commitment")]
        public IActionResult GetDepartmentCommitment(DashboardCommitmentViewModel model)
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            IOrderedEnumerable<DashboardCommitmentViewModel> result = oDashboardBiz.GetCompanyCommitment().OrderBy(t => t.CommitmentNo);

            IOrderedEnumerable<DashboardCommitmentViewModel> resultsOrdered = result.OrderBy(t => t.CommitmentId);
            /*//Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                resultsOrdered = resultsOrdered.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Name == searchValue);
            }*/

            /*if (sortColumn.Any())
            {
                resultsOrdered = sortColumn == "CommitmentNo" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentNo) : resultsOrdered.OrderByDescending(t => t.CommitmentNo)) : resultsOrdered;
                resultsOrdered = sortColumn == "CommitmentName" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentName) : resultsOrdered.OrderByDescending(t => t.CommitmentName)) : resultsOrdered;
                resultsOrdered = sortColumn == "CommitmentRemark" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentRemark) : resultsOrdered.OrderByDescending(t => t.CommitmentRemark)) : resultsOrdered;
                resultsOrdered = sortColumn == "CommitmentStartDate" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentStartDate) : resultsOrdered.OrderByDescending(t => t.CommitmentStartDate)) : resultsOrdered;
                resultsOrdered = sortColumn == "CommitmentFinishDate" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentFinishDate) : resultsOrdered.OrderByDescending(t => t.CommitmentFinishDate)) : resultsOrdered;
                resultsOrdered = sortColumn == "CommitmentStatus" ? (sortColumnDirection == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentStatus) : resultsOrdered.OrderByDescending(t => t.CommitmentStatus)) : resultsOrdered;
            }*/

            //total number of rows count   
            recordsTotal = result.Count();
            //Paging   
            var data = resultsOrdered.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            //return Json(new { draw = draw, recordsFiltered = resultsOrdered.Count(), recordsTotal = recordsTotal, data = data });
            /*if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }*/

            dynamic response = new { Data = result, draw = draw, RecordsFiltered = resultsOrdered.Count(), RecordsTotal = recordsTotal};
            return Ok(response);
        }

        [HttpGet("department/wig/commitment/{WigID}")]
        public IActionResult GetDepartmentCommitmentbyWig(uint WigID)
        {
            var result = oDashboardBiz.GetDepartmentCommitmentbyWig(WigID).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("department/lm/commitment/{LmID}")]
        public IActionResult GetDepartmentCommitmentbyLm(uint LmID)
        {
            var result = oDashboardBiz.GetDepartmentCommitmentbyLm(LmID).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}