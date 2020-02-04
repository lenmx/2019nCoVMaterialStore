using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using nCoVMSApi.Business.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Controllers
{
    public class ApiControllerBase : Controller
    {
        /// <summary>
        /// IP
        /// </summary>
        public string IP => Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? Request.HttpContext.Connection.RemoteIpAddress.ToString();

        /// <summary>
        /// 模型验证
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var keys = context.ModelState.Keys.Select(a => a).ToList();
                throw new Exception($"参数错误，{string.Join(',', keys)}");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context.Result is FileResult) && !(context.Result is ContentResult))
            {
                if (context.Result is EmptyResult)
                {
                    var result = new MJsonResult<object>(null);
                    context.Result = new JsonResult(result);
                }
                else
                {
                    var tempResult = context.Result as dynamic;
                    var result = new MJsonResult<object>(tempResult?.Value);
                    context.Result = new JsonResult(result);
                }
            }
            else
            {
                context.Result = context.Result;
            }
        }

    }
}
