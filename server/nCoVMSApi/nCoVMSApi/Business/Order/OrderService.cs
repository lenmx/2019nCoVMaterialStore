using nCoVMSApi.Business.Material;
using nCoVMSApi.Business.Order.Dto;
using nCoVMSApi.Business.Shop;
using nCoVMSApi.Common;
using nCoVMSApi.Entity;
using nCoVMSApi.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static nCoVMSApi.Business.Common.EnumMap;

namespace nCoVMSApi.Business.Order
{
    public class OrderService
    {
        private MaterialService materialService;
        private ShopService shopService;
        public OrderService()
        {
            this.materialService = new MaterialService();
            this.shopService = new ShopService();
        }

        /// <summary>
        /// 能否下单
        /// 1.IP 限制
        /// 2.一人一天限购数量
        /// 3.总数量限制
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="shopId"></param>
        /// <param name="materialId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int CanSubmitOrder(
            string IP
            , long shopId, long materialId
            , string IDCardNumber
            , DateTime startTime, DateTime endTime
            , out string err)
        {
            err = "";
            int canPreNum = 0;

            using (var db = DBFactory.nCoVMS())
            {
                var materials = materialService.GetAll(shopId, startTime, endTime, materialId);
                if (materials == null || materials.Count <= 0)
                {
                    err = "无物质可预订";
                    return canPreNum;
                }
                var material = materials[0];


                // IP 限制
                var ipCount = db.Torder.Where(a => a.Ip == IP && a.ShopId == shopId && a.MaterialId == materialId && a.CreateTime >= startTime && a.CreateTime <= endTime).Sum(a => a.Num);
                if (ipCount >= material.IpmaxOrderNum)
                {
                    err = "此IP今日预定数量超出限制";
                    return canPreNum;
                }

                // 一人一天限购数量
                var maxOrderNum = db.Torder.Where(a => a.IdcardNumber == IDCardNumber && a.ShopId == shopId && a.MaterialId == materialId && a.CreateTime >= startTime && a.CreateTime <= endTime).Sum(a => a.Num);
                if (maxOrderNum >= material.MaxOrderNum)
                {
                    err = $"此身份证今日预定数量超出限制";
                    return canPreNum;
                }

                // 总数量限制
                var orderReport = (from o in db.Torder.Where(a => a.CreateTime >= startTime && a.CreateTime <= endTime && a.ShopId == shopId && a.MaterialId == materialId)
                                   group o by o.MaterialId into g
                                   select new
                                   {
                                       MaterialId = g.Key,
                                       PreCount = g.Where(a => a.Status == (int)EOrderStatus.PRE).Sum(a => a.Num),
                                       ReleaseCount = g.Where(a => a.Status == (int)EOrderStatus.RELEASE).Sum(a => a.Num),
                                   }).FirstOrDefault();

                if ((orderReport.PreCount + orderReport.ReleaseCount) >= material.TotalCount)
                {
                    err = "物资已被预定完";
                    return canPreNum;
                }

                // 计算可销售的数量
                int canPreOrderNum = material.TotalCount - (material.PreCount + material.ReleaseCount);
                if (canPreOrderNum <= 0)
                    return canPreNum;
                else
                    canPreNum = canPreOrderNum <= material.MaxOrderNum ? canPreOrderNum : material.MaxOrderNum;

                return canPreNum;
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public OrderRes SubmitOrder(string IP, OrderReq req)
        {
            DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime endTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));


            if (req.ShopId <= 0)
                throw new Exception($"参数错误，{nameof(req.ShopId)}");
            if (req.MaterialId <= 0)
                throw new Exception($"参数错误，{nameof(req.ShopId)}");

            if (string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.IDCardNumber))
                throw new Exception("参数错误，姓名和身份证号码不能为空");

            var canPreNum = CanSubmitOrder(IP
                , req.ShopId, req.MaterialId
                , req.IDCardNumber
                , startTime, endTime, out string err);

            if (canPreNum <= 0)
                throw new Exception($"预定失败，{err}");


            #region order code
            string codeTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            int IDCardNumLen = req.IDCardNumber.Length;
            int IDCARD_SUB_LEN = 6;
            string codeIDCardNum = req.IDCardNumber.Substring(IDCardNumLen - IDCARD_SUB_LEN, IDCARD_SUB_LEN);
            string code = $"{codeTime}_{codeIDCardNum}_{req.ShopId}_{req.MaterialId}";
            #endregion


            using (var db = DBFactory.nCoVMS())
            {
                var order = new Torder
                {
                    Code = code,
                    CreateTime = DateTime.Now,
                    IdcardNumber = req.IDCardNumber ?? "",
                    MaterialId = req.MaterialId,
                    Name = req.Name ?? "",
                    Num = canPreNum,
                    ShopId = req.ShopId,
                    Status = (int)EOrderStatus.PRE,
                };
                db.Torder.Add(order);
                db.SaveChanges();
                return MapperHelper.MapperTo<Torder, OrderRes>(order);
            }
        }

        /// <summary>
        /// 发放物资
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool ReleaseOrder(long shopId, OrderReleaseReq req)
        {
            using (var db = DBFactory.nCoVMS())
            {
                var shopEnable = shopService.IsShopEnable(shopId);
                if (shopEnable)
                    throw new Exception("店铺未启用");

                var order = db.Torder.FirstOrDefault(a => a.Code == req.Code && a.ShopId == shopId);
                if (order == null)
                    throw new Exception("订单不存在");

                order.Status = (int)EOrderStatus.RELEASE;
                order.Num = req.Num;
                db.Torder.Update(order);
                return true;
            }
        }

    }
}
