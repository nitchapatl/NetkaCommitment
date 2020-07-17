﻿using System;
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

        [HttpGet("department/commitment/{DepartmentId}")]
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