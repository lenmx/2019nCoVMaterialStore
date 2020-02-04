using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Business.Common
{
    public static class EnumMap
    {
        public enum EShopStatus
        {
            NEW = 0,
            ENABLE = 10,
            DISENABLE = 20,
        }

        public enum EOrderStatus
        {
            PRE = 0,
            RELEASE = 10,
        }

    }
}
