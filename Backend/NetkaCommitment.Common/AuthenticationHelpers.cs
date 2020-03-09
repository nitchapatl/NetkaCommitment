using Microsoft.AspNetCore.Http;
using NetkaCommitment.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class AuthenticationHelpers
    {
        public static HttpRequest RequestInformation { get; set; }

        public static string GetFullPath
        {
            get
            {
                return RequestInformation.Path;
            }
        }

        public static string GetRequestType
        {
            get
            {
                HttpRequest request = RequestInformation;
                return request.Method;
            }
        }

    }
}
