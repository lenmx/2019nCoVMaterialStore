using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nCoVMSApi.Business.Shop;
using nCoVMSApi.Business.Shop.Dto;

namespace nCoVMSApi.Controllers
{
    public class ShopController : ApiControllerBase
    {
        private ShopService shopService;
        public ShopController()
        {
            this.shopService = new ShopService();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("shop/login")]
        public ShopLoginRes Login([FromBody] ShopLoginReq req)
            => shopService.Login(req);
    }
}