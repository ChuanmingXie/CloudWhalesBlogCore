/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Services
*项目描述:
*类 名 称:ServiceEntity
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 9:57:38
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Repository.SqlSugarCore;
using System;

namespace SwaggerWithMiniProfiler.Services
{
    /// <summary>
    /// 实体操作服务
    /// </summary>
    public class ServiceEntity : BaseDB, IEntity
    {
        public SqlSugarClient db = GetClient();
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateEntity(string entityName, string filePath)
        {
            try
            {
                db.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
