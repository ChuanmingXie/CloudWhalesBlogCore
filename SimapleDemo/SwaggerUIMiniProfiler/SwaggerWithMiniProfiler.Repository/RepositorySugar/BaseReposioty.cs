/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.RepositorySugar
*项目描述:
*类 名 称:BaseReposioty
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/9 22:25:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Repository.RepositorySugar
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public ISqlSugarClient Db => throw new NotImplementedException();

        public BaseRepository()
        {

        }

        public Task<int> Add(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Add(List<TEntity> listEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(string whereStr)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(string whereStr, string orderFileds)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(string whereStr, string orderFileds, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int topNum)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderFiledExpression, bool isAsc = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> QueryById(object id, bool addInCahce = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryByIds(object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<ModelPage<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, string orderFileds = null, int pageIndex = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(Expression<Func<T1, T2, object[]>> joinExpression, Expression<Func<T1, T2, TResult>> selectExpression, Expression<Func<TResult, bool>> whereExpression, string orderFileds, int pageIndex = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(Expression<Func<T1, T2, object[]>> joinExpression, Expression<Func<T1, T2, TResult>> selectExpression, Expression<Func<T1, object>> groupExpression, Expression<Func<TResult, bool>> whereExpression, string orderFileds, int pageIndex = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> QueryUnion<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, object[]>> joinExpression, Expression<Func<T1, T2, T3, TResult>> selectExpression, Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity model, string whereStr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(object anonymousObject)
        {
            throw new NotImplementedException();
        }
    }
}
