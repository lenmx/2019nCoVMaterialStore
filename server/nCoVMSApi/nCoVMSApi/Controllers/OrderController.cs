using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nCoVMSApi.Business.Material;
using nCoVMSApi.Business.Material.Dto;
using nCoVMSApi.Business.Order;
using nCoVMSApi.Business.Order.Dto;
using nCoVMSApi.Business.Shop;
using nCoVMSApi.Business.Shop.Dto;
using nCoVMSApi.Common;
using nCoVMSApi.Filters;

namespace nCoVMSApi.Controllers
{
    public class OrderController : ApiControllerBase
    {
        private OrderService orderService;
        public OrderController()
        {
            this.orderService = new OrderService();
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("order")]
        public OrderRes SubmitOrder([FromBody] OrderReq req)
            => orderService.SubmitOrder(base.IP, req);

        /// <summary>
        /// 创建或更新物资
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [CAuthorize, HttpPost("order/release")]
        public bool ReleaseOrder([FromBody] OrderReleaseReq req)
        {
            long shopId = HttpContext.User.Identity.ShopId();
            return orderService.ReleaseOrder(shopId, req);
        }

    }
}