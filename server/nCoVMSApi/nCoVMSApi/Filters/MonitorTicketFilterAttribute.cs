using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Filters
{
    public class MonitorTicketFilterAttribute : IAsyncActionFilter
    {
        public MonitorTicketFilterAttribute() { }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            DateTime startTime = DateTime.Now;

            var nextContext = await next();

            MonitorLog log = new MonitorLog(
                context.ActionDescriptor.DisplayName,
                startTime,
                DateTime.Now,
                context.ActionArguments,
                nextContext.Result
                );

            // todo log
        }
    }

    public class MonitorLog
    {
        public string ActionName { get; set; }
        public DateTime ExecuteStartTime { get; set; }
        public DateTime ExecuteEndTime { get; set; }
        public Dictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
        public object Response { get; set; }

        public MonitorLog(string actionName, DateTime startTime, DateTime endTime, IDictionary<string, object> args, object response)
        {
            this.ActionName = actionName;
            this.ExecuteStartTime = startTime;
            this.ExecuteEndTime = endTime;
            this.Response = response;
            if (args != null)
                args.ToList().ForEach(a => this.Args.Add(a.Key, a.Value ?? ""));
        }

        public override string ToString()
        {
            ObjectResult _response = null;
            try { _response = Response as ObjectResult; } catch { }

            return $@"
    API 监控：
    action：{ActionName}
    时间：{ExecuteStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff")} ~ {ExecuteEndTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}
    耗时：{(ExecuteEndTime - ExecuteStartTime).TotalMilliseconds}
    参数：{JsonConvert.SerializeObject(Args)}
    响应：{JsonConvert.SerializeObject(_response != null ? _response.Value : null)}";
        }

    }
}
