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
        public  CommitmentController(IHttpContextAccessor oHttpContextAccessor) : base(oHttpContextAccessor)
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
            if (model.CommitmentStatus=="done") {
                result = oCommitmentBiz.UpdateCommitment(model);
                return StatusCode(StatusCodes.Status200OK, result);
            } else if (model.CommitmentStatus=="fail") {
                result = oCommitmentBiz.UpdateCommitment(model);
                return StatusCode(StatusCodes.Status200OK,result);
            } else if (model.CommitmentStatus=="delete") {
                result = oCommitmentBiz.DeleteCommitment(model);
                return StatusCode(StatusCodes.Status200OK,result);
            } else if (model.CommitmentStatus=="postpone") {
                result = oCommitmentBiz.PostponeCommitment(model);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost("addCommitment")]
        public IActionResult addCommitment([FromBody] TCommitmentViewModel model) {
            var result = oCommitmentBiz.InsertCommitment(model.DepartmentLmId,model.CommitmentName);
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
            var result = oCommitmentBiz.GetCommitment().Where(x => x.CommitmentIsDeleted == 0 && x.CreatedBy == model.CreatedBy);
            if (result != null) {
                return StatusCode(StatusCodes.Status200OK,result);
            }
            else {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}