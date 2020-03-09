using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetkaCommitment.Common
{
    public static class SessionHelpers
    {
        public static ISession InitSession { get; set; }
        public static bool Set<T>(string key, T value)
        {
            try
            {
                InitSession.Set(key, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(value)));
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static T Get<T>(string key)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(InitSession.Get(key)));
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }
    }
}
