using Microsoft.IdentityModel.Tokens;
using nCoVMSApi.Business.Shop.Dto;
using nCoVMSApi.Common;
using nCoVMSApi.Common.EncryptHelper;
using nCoVMSApi.Entity;
using nCoVMSApi.Entity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static nCoVMSApi.Business.Common.EnumMap;

namespace nCoVMSApi.Business.Shop
{
    public class ShopService
    {
        private readonly string AUTH_BASE_URL = ConfigHelper.Get("AuthConfig:BaseUrl");
        private readonly string AUTH_SECURITY_KEY = ConfigHelper.Get("AuthConfig:SecurityKey");
        private readonly int AUTH_EXPMIN = ConfigHelper.Get<int>("AuthConfig:ExpMin");
        public ShopService() { }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        public Tshop Login(string name, string securityCode)
        {
            using (var db = DBFactory.nCoVMS())
            {
                var ext = db.Tshop.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                if (ext == null)
                    throw new Exception("店铺不存在，请先上报");

                if (ext.Status != (int)EShopStatus.ENABLE)
                    throw new Exception("店铺暂未启用，请联系管理员");

                if (!ext.SecurityCode.Equals(EncryptHelper.Sha1(securityCode)))
                {
                    ext.FailLoginTime += 1;
                    throw new Exception("店铺名称或安全码错误");
                }
                else
                    ext.FailLoginTime = 0;

                ext.LastLoginTime = DateTime.Now;
                db.SaveChanges();
                return ext;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ShopLoginRes Login(ShopLoginReq req)
        {
            var shop = this.Login(req.Name, req.SecurityCode);
            if (shop == null)
                throw new Exception("登录失败");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AUTH_SECURITY_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: AUTH_BASE_URL,
                audience: AUTH_BASE_URL,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(AUTH_EXPMIN),
                claims: new[] {
                    new Claim("id", shop.Id.ToString()),
                    new Claim("name", shop.Name),
                    new Claim("fullName", shop.FullName),
                });

            return new ShopLoginRes
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Shop = MapperHelper.MapperTo<Tshop, ShopLoginRes_Shop>(shop)
            };
        }

        /// <summary>
        /// 店铺是否启用
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool IsShopEnable(long shopId)
        {
            using (var db = DBFactory.nCoVMS())
            {
                return db.Tshop.Any(a => a.Id == shopId && a.Status == (int)EShopStatus.ENABLE);
            }
        }

    }
}
