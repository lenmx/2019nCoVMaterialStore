using nCoVMSApi.Business.Material.Dto;
using nCoVMSApi.Business.Shop;
using nCoVMSApi.Common;
using nCoVMSApi.Entity;
using nCoVMSApi.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static nCoVMSApi.Business.Common.EnumMap;

namespace nCoVMSApi.Business.Material
{
    public class MaterialService
    {
        private ShopService shopService;
        public MaterialService()
        {
            this.shopService = new ShopService();
        }

        /// <summary>
        /// 获取全部物资销售情况
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<MaterialReleaseRes> GetAll(long shopId, DateTime startTime, DateTime endTime, long materialId = 0)
        {
            using (var db = DBFactory.nCoVMS())
            {
                bool shopEnable = shopService.IsShopEnable(shopId);
                if (!shopEnable)
                    return null;

                startTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd 00:00:00"));
                endTime = DateTime.Parse(endTime.ToString("yyyy-MM-dd 23:59:59"));

                var query = db.VmaterialRelease.Where(a => a.ShopId == shopId && a.CreateTime >= startTime && a.CreateTime <= endTime);
                if (materialId > 0)
                    query = query.Where(a => a.Id == materialId);

                return MapperHelper.MapperToList<VmaterialRelease, MaterialReleaseRes>(query.ToList());
            }
        }

        /// <summary>
        /// 创建或更新物资
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool CreateOrUpdate(long shopId, MaterialReq req)
        {
            using (var db = DBFactory.nCoVMS())
            {
                bool shopEnable = shopService.IsShopEnable(shopId);
                if (!shopEnable)
                    throw new Exception("店铺未启用");

                var ext = db.Tmaterial.FirstOrDefault(a => a.Id == req.Id);
                if (ext == null)
                {
                    db.Tmaterial.Add(new Tmaterial
                    {
                        CreateTime = DateTime.Now,
                        FullName = req.FullName ?? req.Name,
                        MaxOrderNum = req.MaxOrderNum,
                        IpmaxOrderNum = req.IpmaxOrderNum,
                        Name = req.Name,
                        Price = req.Price,
                        ShopId = shopId,
                        TotalCount = req.TotalCount
                    });
                }
                else
                {
                    ext.FullName = req.FullName ?? req.Name;
                    ext.MaxOrderNum = req.MaxOrderNum;
                    ext.IpmaxOrderNum = req.IpmaxOrderNum;
                    ext.Name = req.Name;
                    ext.Price = req.Price;
                    ext.TotalCount = req.TotalCount;
                    db.Tmaterial.Update(ext);
                }
                db.SaveChanges();
                return true;
            }
        }

    }
}
