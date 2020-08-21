using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class JwtHelpers
    {
        private static string jwtKey = "asdhjkl;';lkjhgfdsa[poiuytrewzxcvbnm,.";
        private static string jwtIssuer = "Netkacommitment";
        private static string jwtAudience = "Netkacommitment";
        private static int jwtExpired = 1440;

        public static string JwtKey
        {
            get
            {
                return jwtKey;
            }
        }

        public static string JwtIssuer
        {
            get
            {
                return jwtIssuer;
            }
        }

        public static string JwtAudience
        {
            get
            {
                return jwtAudience;
            }
        }

        public static int JwtExpired
        {
            get
            {
                return jwtExpired;
            }
        }
    }
}
