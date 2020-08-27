using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class JwtHelpers
    {

        public static string JwtKey
        {
            get
            {
                return "asdhjkl;';lkjhgfdsa[poiuytrewzxcvbnm,.";
            }
        }

        public static string JwtIssuer
        {
            get
            {
                return "Netkacommitment";
            }
        }

        public static string JwtAudience
        {
            get
            {
                return "Netkacommitment";
            }
        }

        public static int JwtExpired
        {
            get
            {
                return 1440;
            }
        }
    }
}
