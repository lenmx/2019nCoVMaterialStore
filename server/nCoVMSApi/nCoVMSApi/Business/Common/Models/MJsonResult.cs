using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Common.Models
{
    public class MJsonResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public bool __nCoVMS { get; set; }

        public MJsonResult(T result)
        {
            this.Code = 0;
            this.Message = "";
            this.Result = result;
            this.__nCoVMS = true;
        }

        public MJsonResult(int code, string message)
        {
            this.Code = code;
            this.Message = message;
            this.__nCoVMS = true;
        }

        [Newtonsoft.Json.JsonConstructor]
        public MJsonResult(int Code, string Message, T Result, bool __nCoVMS)
        {
            this.Code = Code;
            this.Message = Message;
            this.Result = Result;
            this.__nCoVMS = __nCoVMS;
        }

    }

}
