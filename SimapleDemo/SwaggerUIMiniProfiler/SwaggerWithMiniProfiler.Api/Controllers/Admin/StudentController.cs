using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerWithMiniProfiler.BLL.Admin;
using SwaggerWithMiniProfiler.Model.Entities;

namespace SwaggerWithMiniProfiler.Api.Controllers.Admin
{
    /// <summary>
    /// 学生模块
    /// </summary>
    [Produces("application/json")]
    [Route("api/admin/[controller]")]
    public class StudentController : Controller
    {
        private StudentBLL studentBLL = new StudentBLL();

        /// <summary>
        /// 获取学生分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStudent")]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(studentBLL.GetPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 获取单个学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStudentByID/{id}")]
        [Authorize]
        public JsonResult GetStudentById(long id)
        {
            return Json(studentBLL.GetById(id));
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddStudent")]
        [Authorize(Policy = "SystemOrAdmin")]
        public JsonResult Add(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");

            return Json(studentBLL.Add(entity));
        }

        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateStudent")]
        [Authorize(Roles = "Admin")]
        public JsonResult Update(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");

            return Json(studentBLL.Update(entity));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteStudent")]
        public JsonResult Delete(dynamic[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(studentBLL.Dels(ids));
        }
    }
}
