using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetkaCommitment.Common;

namespace NetkaCommitment.Web.ApiControllers
{
    public class BaseApiController : ControllerBase
    {
        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            AuthenticationHelpers.RequestInformation = httpContextAccessor.HttpContext.Request;
        }
    }
}