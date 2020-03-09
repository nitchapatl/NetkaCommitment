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

    }
}