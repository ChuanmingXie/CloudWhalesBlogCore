/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IEntity
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:11:12
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IEntity
    {
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        bool CreateEntity(string entityName, string filePath);
    }
}
