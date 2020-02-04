using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nCoVMSApi.Business.Material;
using nCoVMSApi.Business.Material.Dto;
using nCoVMSApi.Business.Shop;
using nCoVMSApi.Business.Shop.Dto;
using nCoVMSApi.Common;
using nCoVMSApi.Filters;

namespace nCoVMSApi.Controllers
{
    public class MaterialController : ApiControllerBase
    {
        private MaterialService materialService;
        public MaterialController()
        {
            this.materialService = new MaterialService();
        }

        /// <summary>
        /// 获取物资
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("materials")]
        public List<MaterialReleaseRes> GetAll(long shopId, DateTime startTime, DateTime endTime)
        {
            long _shopId = HttpContext.User.Identity.ShopId();
            if (_shopId > 0) 
                shopId = _shopId;

            if (shopId <= 0)
                throw new Exception($"参数错误，{nameof(shopId)}");
            if (startTime == null)
                throw new Exception($"参数错误，{nameof(startTime)}");
            if (endTime == null)
                throw new Exception($"参数错误，{nameof(endTime)}");

            return materialService.GetAll(shopId, startTime, endTime);
        }

        /// <summary>
        /// 创建或更新物资
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [CAuthorize, HttpPost("material")]
        public bool CreateOrUpdate([FromBody] MaterialReq req)
        {
            long shopId = HttpContext.User.Identity.ShopId();
            return materialService.CreateOrUpdate(shopId, req);
        }

    }
}