using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetkaCommitment.Business;

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

        [HttpGet("department/wig")]
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
        }

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
    }
}