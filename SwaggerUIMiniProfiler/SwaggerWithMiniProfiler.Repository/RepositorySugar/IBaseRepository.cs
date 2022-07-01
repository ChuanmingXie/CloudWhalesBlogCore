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
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<TEntity> QueryById(object id);

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">数据Id</param>
        /// <param name="addInCahce">是否缓存</param>
        /// <returns></returns>
        Task<TEntity> QueryById(object id, bool addInCahce = false);

        /// <summary>
        /// 根据多个Id查村实体列表
        /// </summary>
        /// <param name="ids">id数据</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryByIds(object[] ids);


        /// <summary>
        /// 根据模型添加实体
        /// </summary>
        /// <param name="model">实体数据</param>
        /// <returns></returns>
        Task<int> Add(TEntity model);

        /// <summary>
        /// 根据模型添加实体
        /// </summary>
        /// <param name="listEntity">实体数据列表</param>
        /// <returns></returns>
        Task<int> Add(List<TEntity> listEntity);


        /// <summary>
        /// 根据数据模型删除数据
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        Task<bool> Delete(TEntity model);

        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        /// 根据多个Id删除数据
        /// </summary>
        /// <param name="ids">Id数组</param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);


        /// <summary>
        ///根据模型型更新实体
        /// </summary>
        /// <returns></returns>
        Task<bool> Update(TEntity model);

        /// <summary>
        /// 根据模型更新实体
        /// </summary>
        /// <param name="model">实体模型</param>
        /// <param name="whereStr">where子句</param>
        /// <returns></returns>
        Task<bool> Update(TEntity model, string whereStr);

        /// <summary>
        /// 根据数据对象更新实体
        /// </summary>
        /// <param name="anonymousObject">数据对象</param>
        /// <returns></returns>
        Task<bool> Update(object anonymousObject);

        /// <summary>
        /// 根据实体子段更新实体
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="listColumns">数据行</param>
        /// <param name="listIgnoreColumns">忽略的数据行</param>
        /// <param name="whereStr">where子句</param>
        /// <returns></returns>
        Task<bool> Update(TEntity model, List<string> listColumns = null, List<string> listIgnoreColumns = null, string whereStr = "");


        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> Query();

        /// <summary>
        /// SQl查询到实体列表
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null);

        /// <summary>
        /// SQL查询到dataTable
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null);


        /// <summary>
        /// 带where子句的查询
        /// </summary>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        Task<List<TEntity>> Query(string whereStr);

        /// <summary>
        /// 带where子句、orderby子句查询
        /// </summary>
        /// <param name="whereStr">where子句参数</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(string whereStr, string orderFileds);

        /// <summary>
        /// 带where子句、orderby子句、take子句 查询
        /// </summary>
        /// <param name="whereStr">where子句参数</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="topNum">take子句参数</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(string whereStr, string orderFileds, int topNum);

        /// <summary>
        /// 带where子句、orderby子句、分页 查询
        /// </summary>
        /// <param name="whereStr">where子句参数</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="pageIndex">页面数</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(string whereStr, string orderFileds, int pageIndex, int pageSize);


        /// <summary>
        /// 带where表达式 查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 带where表达式、orderby子句 查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds);

        /// <summary>
        /// 带where表达式、orderby子句、Take子句 查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="topNum">Take子句参数</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int topNum);

        /// <summary>
        /// 带where表达式、orderby子句、分页 查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="pageIndex">页面数</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds, int pageIndex, int pageSize);

        /// <summary>
        /// 带where表达式、orderby表达式、正序与否查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFilesExpression">orderby子句表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderFilesExpression, bool isAsc = true);


        /// <summary>
        /// 根据查询表达式，查询需要的指定模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        /// 根据查询表达式、where表达式、排序子句查询需要的指定模型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <returns></returns>
        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression, string orderFileds);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="pageIndex">页面数</param>
        /// <param name="pageSize">页面大小</param>
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
        /// <param name="selectExpression">select子句表达式</param>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="pageIndex">页面数</param>
        /// <param name="pageSize">页面大小</param>
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
        /// <param name="selectExpression">select子句表达式</param>
        /// <param name="groupExpression">group子句表达式</param>
        /// <param name="whereExpression">where子句表达式</param>
        /// <param name="orderFileds">orderby子句参数</param>
        /// <param name="pageIndex">页面数</param>
        /// <param name="pageSize">页面大小</param>
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
        /// <param name="selectExpression">select子句表达式</param>
        /// <param name="whereExpression">where子句表达式</param>
        /// <returns></returns>
        Task<List<TResult>> QueryUnion<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new();
    }
}
