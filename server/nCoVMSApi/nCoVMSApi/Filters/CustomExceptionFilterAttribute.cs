using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using nCoVMSApi.Business.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public CustomExceptionFilterAttribute() { }
        public override void OnException(ExceptionContext context)
        {
            MJsonResult<object> result = null;
            if (context.Exception != null) result = this.GeneratExceptionResult(context.Exception);
            else result = this.GeneratExceptionResult(new Exception("服务器繁忙，请重试"));

            context.ExceptionHandled = true;
            context.Result = new JsonResult(result);

            // todo log
        }

        private MJsonResult<object> GeneratExceptionResult(Exception exception)
        {
            return new MJsonResult<object>(-1,
                exception.InnerException != null
                ? exception.InnerException.ToString()
                : exception.Message);
        }

    }
}
