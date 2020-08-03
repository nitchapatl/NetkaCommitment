using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetkaCommitment.Business;
using NetkaCommitment.Data.ViewModel;

namespace NetkaCommitment.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitmentController : BaseApiController
    {
        private readonly CommitmentBiz oCommitmentBiz = null;
        public CommitmentController(IHttpContextAccessor oHttpContextAccessor) : base(oHttpContextAccessor)
        {
            oCommitmentBiz = new CommitmentBiz();
        }

        [HttpGet]
        public IActionResult GetDepartmentWig()
        {
            var result = new List<string>();

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("getDepartmentWig")]
        public IActionResult getDepartmentWig()
        {
            var result = oCommitmentBiz.getDepartmentWig(5);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("updateCommitment")]
        public IActionResult updateCommitment([FromBody] TCommitmentViewModel model)
        {
            var oCommitment = new TCommitmentViewModel();
            oCommitment.CommitmentId = model.CommitmentId;
            oCommitment.UpdatedBy = model.UpdatedBy;

            var result = false;
            if (model.CommitmentStatus == "Success" || model.CommitmentStatus == "Fail") {
                result = oCommitmentBiz.UpdateCommitment(model);
                return StatusCode(StatusCodes.Status200OK, result);
            } else if (model.CommitmentStatus == "Delete") {
                result = oCommitmentBiz.DeleteCommitment(model);
                return StatusCode(StatusCodes.Status200OK, result);
            } else if (model.CommitmentStatus == "Postpone") {
                result = oCommitmentBiz.PostponeCommitment(model);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost("addCommitment")]
        public IActionResult addCommitment([FromBody] TCommitmentViewModel model) {
            var result = oCommitmentBiz.InsertCommitment(model);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetTCommitment")]
        public IActionResult getTCommitment([FromBody] TCommitmentViewModel model)
        {
            var result = oCommitmentBiz.GetCommitment().Where(x => x.CreatedBy == model.CreatedBy);
            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("GetCommitment")]
        public IActionResult getCommitment([FromBody] TCommitmentViewModel model) {
            var result = oCommitmentBiz.GetCommitment().Where(x => !(x.IsDeleted == 1) && x.CreatedBy == model.CreatedBy);
            if (result != null) {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("GetGraphWig")]
        public IActionResult GetCompanyWig()
        {
            var result = oCommitmentBiz.GetCompanyWIG();

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }


        [HttpPost("GetCommitmentSummary")]
        public IActionResult getCommitmentSummary([FromBody] TCommitmentSummaryViewModel model)
        {
            var result = oCommitmentBiz.GetCommitmentSummary(model);
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