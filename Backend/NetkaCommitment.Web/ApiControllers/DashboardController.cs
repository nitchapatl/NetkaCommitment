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

        [HttpGet("user/department/wig/{UserId}/{DepartmentId}")]
        public IActionResult GetUserDepartmentWig(uint UserId, uint DepartmentId)
        {
            var result = oDashboardBiz.GetUserDepartmentWIG(UserId, DepartmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("department/commitment")]
        public IActionResult GetDepartmentCommitment([FromBody] DashboardCommitmentViewModel model)
        {
            var result = oDashboardBiz.GetDepartmentCommitment(model.DepartmentId).OrderBy(t => t.CommitmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("user/department/commitment/{UserId}/{DepartmentId}")]
        public IActionResult GetUserDepartmentCommitment(uint UserId, uint DepartmentId)
        {
            var result = oDashboardBiz.GetUserDepartmentCommitment(UserId, DepartmentId).OrderBy(t => t.CommitmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("department/wig/commitment")]
        public IActionResult GetDepartmentCommitmentbyWig([FromBody] DashboardCommitmentViewModel model)
        {
            var result = oDashboardBiz.GetDepartmentCommitmentbyWig(model.DepartmentWigId).OrderBy(t => t.CommitmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("user/department/wig/commitment/{UserId}/{DepartmentWigId}")]
        public IActionResult GetUserDepartmentCommitmentbyWig(uint UserId, uint DepartmentWigId)
        {
            var result = oDashboardBiz.GetUserDepartmentCommitmentbyWig(UserId, DepartmentWigId).OrderBy(t => t.CommitmentId);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("department/lm/commitment")]
        public IActionResult GetDepartmentCommitmentbyLm([FromBody] DashboardCommitmentViewModel model)
        {
            var result = oDashboardBiz.GetDepartmentCommitmentbyLm(model.DepartmentLmId).OrderBy(t => t.CommitmentNo);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("user/department/lm/commitment/{UserId}/{LmId}")]
        public IActionResult GetUserDepartmentCommitmentbyLm(uint UserId, uint LmId)
        {
            var result = oDashboardBiz.GetUserDepartmentCommitmentbyLm(UserId, LmId).OrderBy(t => t.CommitmentNo);

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