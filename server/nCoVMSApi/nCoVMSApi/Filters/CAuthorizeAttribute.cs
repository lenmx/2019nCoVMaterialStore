using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using nCoVMSApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace nCoVMSApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Permissions { get; }

        public CAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
#if DEBUG
                return;
#endif

                if (context.Filters.Any(item => item is IAllowAnonymousFilter || item is IAllowAnonymous))
                    return;

                var user = context.HttpContext.User;
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var claims = user.Claims.ToList();
                if (claims == null || claims.Count <= 0)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // 过期时间
                var expTS = claims.FirstOrDefault(a => a.Type == "exp");
                var expTime = TimeHelper.GetTimestampToTime(expTS?.Value);
                if (expTime == null || expTime < DateTime.Now)
                {
                    // 授权超时
                    context.Result = new StatusCodeResult(401009);
                    return;
                }

                // 角色验证
                if (Permissions != null && Permissions.Length > 0)
                {
                    var roleStr = claims.FirstOrDefault(a => a.Type == ClaimTypes.Role);
                    if (roleStr == null)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    var roles = roleStr.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < Permissions.Length; i++)
                    {
                        if (!roles.Contains(Permissions[i]))
                        {
                            context.Result = new UnauthorizedResult();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
