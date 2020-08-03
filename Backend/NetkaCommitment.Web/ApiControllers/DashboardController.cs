using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetkaCommitment.Business;
using NetkaCommitment.Common;
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

            model = model ?? new DashboardCommitmentViewModel();
            model.columns = model.columns ?? new List<Data.ViewModel.Column>();
            model.search = model.search ?? new Data.ViewModel.Search
            {
                value = string.Empty
            };

            IEnumerable<DashboardCommitmentViewModel> results = oDashboardBiz.GetDepartmentCommitment(model.DepartmentId).OrderBy(t => t.CommitmentNo);

            IEnumerable<DashboardCommitmentViewModel> resultsFiltered = results;
            if (!string.IsNullOrEmpty(model.search.value.Trim()))
            {
                resultsFiltered = resultsFiltered.Where(t =>
                    t.CommitmentName.Contains(model.search.value.Trim()) ||
                    t.CommitmentRemark.Contains(model.search.value.Trim()) ||
                    t.CommitmentStatus.Contains(model.search.value.Trim())
                );
            }

            IOrderedEnumerable<DashboardCommitmentViewModel> resultsOrdered = resultsFiltered.OrderBy(t => t.CommitmentId);

            if (model.columns.Any())
            {
                string orderBy = model.columns[model.order[0].column].data;
                string orderType = model.order[0].dir;

                resultsOrdered = orderBy == "CommitmentNo" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentNo) : resultsOrdered.OrderByDescending(t => t.CommitmentNo)) : resultsOrdered;
                resultsOrdered = orderBy == "CommitmentName" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentName) : resultsOrdered.OrderByDescending(t => t.CommitmentName)) : resultsOrdered;
                resultsOrdered = orderBy == "CommitmentRemark" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentRemark) : resultsOrdered.OrderByDescending(t => t.CommitmentRemark)) : resultsOrdered;
                resultsOrdered = orderBy == "CommitmentStartDate" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentStartDate) : resultsOrdered.OrderByDescending(t => t.CommitmentStartDate)) : resultsOrdered;
                resultsOrdered = orderBy == "CommitmentFinishDate" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentFinishDate) : resultsOrdered.OrderByDescending(t => t.CommitmentFinishDate)) : resultsOrdered;
                resultsOrdered = orderBy == "CommitmentStatus" ? (orderType == "asc" ? resultsOrdered.OrderBy(t => t.CommitmentStatus) : resultsOrdered.OrderByDescending(t => t.CommitmentStatus)) : resultsOrdered;
            }

            /*return new DataTablesServerSideResult<DashboardCommitmentViewModel>
            {
                draw = model.draw,
                recordsFiltered = resultsOrdered.Count(),
                recordsTotal = results.Count(),
                data = resultsOrdered.Skip(model.start).Take(model.length).ToList()
            };*/
            return Ok(new DataTablesServerSideResult<DashboardCommitmentViewModel>
            {
                draw = model.draw,
                recordsTotal = results.Count(),
                recordsFiltered = resultsOrdered.Count(),
                data = resultsOrdered
                    .Skip(model.start)
                    .Take(model.length)
                    .ToList()
            });
            /*if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }*/
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