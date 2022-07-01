/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.UnitOfWork
*项目描述:
*类 名 称:IUnit
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/9 22:31:16
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        SqlSugarScope GetDBClient();

        void BeginTran();

        void CommitTran();

        void RollbackTran();
    }
}
