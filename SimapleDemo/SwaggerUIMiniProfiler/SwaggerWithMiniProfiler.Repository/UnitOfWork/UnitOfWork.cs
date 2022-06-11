/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.UnitOfWork
*项目描述:
*类 名 称:UnitOfWork
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/9 22:33:50
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient sqlSugarClient;

        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWork(ILogger<UnitOfWork> logger, ISqlSugarClient sqlSugarClient)
        {
            this.logger = logger;
            this.sqlSugarClient = sqlSugarClient;
        }

        public void BeginTran()
        {
            GetDBClient().BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDBClient().CommitTran();
            }
            catch (Exception ex)
            {
                GetDBClient().RollbackTran();
                logger.LogError($"{ex.Message}\r\n{ex.InnerException}");
            }
        }

        public SqlSugarScope GetDBClient()
        {
            return sqlSugarClient as SqlSugarScope;
        }

        public void RollbackTran()
        {
            GetDBClient().RollbackTran();
        }
    }
}
