using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace nCoVMSApi.Common
{
    public static class ClaimHelper
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static long ShopId(this IIdentity identity)
        {
            try
            {
                return GetValue<long>(identity, "shopId");
            }
            catch
            {
                return 0;
            }
        }


        public static T GetClaim<T>(this IIdentity identity, string key)
        {
            return GetValue<T>(identity, key);
        }

        private static string GetValue(this IIdentity identity, string key)
            => GetValue<string>(identity, key);

        private static T GetValue<T>(this IIdentity identity, string key)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return default(T);
            if (claimsIdentity == null || !claimsIdentity.Claims.Any())
                return default(T);

            var claim = claimsIdentity.Claims.FirstOrDefault(a => a.Type == key);
            if (claim == null)
                return default(T);

            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }
    }
}
