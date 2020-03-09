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
    public class LoginController : BaseApiController
    {
        private readonly LoginBiz oLoginBiz = null;
        public LoginController(IHttpContextAccessor oHttpContextAccessor) : base(oHttpContextAccessor) { 
            oLoginBiz = new LoginBiz();
        }

        [HttpPost("getuser")]
        public IActionResult GetUser()
        {
            var result = oLoginBiz.GetUser(11);

            if (result != null)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost("autorize")]
        public IActionResult Autorize([FromBody] LoginUserViewModel model)
        {
            string strToken = oLoginBiz.Authorize(model);
            if (!string.IsNullOrEmpty(strToken))
            {
                return StatusCode(StatusCodes.Status201Created, strToken);
            }
            else if (oLoginBiz.AuthorizeExist(model))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Wrong password.");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Username not found");
            }
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] LoginUserViewModel model)
        {
            if (oLoginBiz.ForgotPassword(model))
            {
                return StatusCode(StatusCodes.Status205ResetContent);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Username not found");
            }
        }
    }
}
