/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.RepositorySugar
*项目描述:
*类 名 称:IBaseRepository
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/9 21:18:46
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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Repository.RepositorySugar
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        ISqlSugarClient Db { get; }

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TEntity> QueryById(object id);

        Task<TEntity> QueryById(object id, bool addInCahce = false);

        Task<List<TEntity>> QueryByIds(object[] ids);


        /// <summary>
        /// 根据模型添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(TEntity model);

        Task<int> Add(List<TEntity> listEntity);


        /// <summary>
        /// 根据模型、id删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Delete(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> DeleteByIds(object[] ids);


        /// <summary>
        ///根据模型型更新实体
        /// </summary>
        /// <returns></returns>
        Task<bool> Update(TEntity model);

        Task<bool> Update(TEntity model, string whereStr);

        Task<bool> Update(object anonymousObject);


        /// <summary>
        /// sql查询
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> Query();

        Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null);

        Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null);


        /// <summary>
        /// 带where字句的查询
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        Task<List<TEntity>> Query(string whereStr);

        Task<List<TEntity>> Query(string whereStr, string orderFileds);

        Task<List<TEntity>> Query(string whereStr, string orderFileds, int pageIndex, int pageSize);


        /// <summary>
        /// 根据查询表达式查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int topNum);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderFiledExpression, bool isAsc = true);


        /// <summary>
        /// 根据查询表达式查询需要的指定模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 根据查询表达式、where字句、排序字句查询需要的指定模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression, string orderFileds);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderFileds"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ModelPage<TEntity>> QueryPage(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFileds = null,
            int pageIndex = 1,
            int pageSize = 20);

        /// <summary>
        /// 联合查询分页
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderFileds"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(
            Expression<Func<T1, T2, object[]>> joinExpression,
            Expression<Func<T1, T2, TResult>> selectExpression,
            Expression<Func<TResult, bool>> whereExpression,
            string orderFileds,
            int pageIndex = 1,
            int pageSize = 20);

        /// <summary>
        /// 联合查询分页分组
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="groupExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderFileds"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(
            Expression<Func<T1, T2, object[]>> joinExpression,
            Expression<Func<T1, T2, TResult>> selectExpression,
            Expression<Func<T1, object>> groupExpression,
            Expression<Func<TResult, bool>> whereExpression,
            string orderFileds,
            int pageIndex = 1,
            int pageSize = 20);


        /// <summary>
        /// 三表联合查询
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<List<TResult>> QueryUnion<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new();
    }
}
