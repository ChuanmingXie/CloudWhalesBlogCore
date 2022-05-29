using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SwaggerWithMiniProfiler.BLL.Admin;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Api.Controllers.Admin
{
    /// <summary>
    /// 实体管理模块
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EntityController : Controller
    {
        private readonly EntityBLL _entityBLL = new();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EntityController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SqlSugarCreateEntity")]
        public JsonResult CreateEntity(string entityName = null)
        {
            if (entityName == null)
                return Json("参数为空");
            return Json(_entityBLL.CreateEntity(entityName, _webHostEnvironment.ContentRootPath));
        }

        [HttpGet]
        [Route("SysGetToken")]
        public IActionResult TokenStr(long id,string sub,int expiresSliding,int expiresAbsoulute)
        {
            var mToken = new ModelToken()
            {
                Uid = id,
                Sub = sub,
                Role="Admin"
            };
            return Ok(JwtTokenHelper.IssueJwt(mToken));
        }

        [HttpGet]
        [Route("{Role}")]
        public IActionResult Login(string role)
        {
            string jwtStr = string.Empty;
            bool suc = false;

            if (role != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                ModelToken tokenModel = new ModelToken { Uid = 1, Role = role };
                jwtStr = JwtTokenHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
                suc = true;
            }
            else
            {
                jwtStr = "login fail!!!";
            }

            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ParseToken()
        {
            //需要截取Bearer 
            var tokenHeader = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = JwtTokenHelper.SerializeJwt(tokenHeader);
            return Ok(user);

        }

        /// <summary>
        /// 需要Admin权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        [Route("GetAdmin")]
        public IActionResult Admin()
        {
            return Ok("hello admin");
        }


        /// <summary>
        /// 需要System权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSystem")]
        [Authorize(Roles = "System")]
        public IActionResult System()
        {
            return Ok("hello System");
        }
    }
}
