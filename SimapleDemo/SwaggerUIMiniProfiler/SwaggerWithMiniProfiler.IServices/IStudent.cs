/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IStudent
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:12:11
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Model.ViewModel;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IStudent
    {
        #region base

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        ModelTable<Student> GetPageList(int pageIndex, int pageSize);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Get(long id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(Student entity);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(Student entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Dels(dynamic[] ids);

        #endregion
    }
}
