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
                uid = id,
                Sub = sub
            };
            return Ok(JwtTokenHelper.IssueJWT(mToken, TimeSpan.FromMinutes(expiresSliding), TimeSpan.FromDays(expiresAbsoulute)));
        }
    }
}
