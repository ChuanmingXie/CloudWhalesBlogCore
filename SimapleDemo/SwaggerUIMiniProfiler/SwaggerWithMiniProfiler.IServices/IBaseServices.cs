/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IBaseServices
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 19:11:26
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<TEntity> QueryById(object id);

        Task<TEntity> QueryById(object id, bool addInCache = false);

        Task<List<TEntity>> QueryByIds(object[] ids);


        Task<int> Add(TEntity model);

        Task<int> Add(List<TEntity> listEntity);


        Task<bool> Delete(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> DeleteByIds(object[] ids);


        Task<bool> Update(TEntity model);

        Task<bool> Update(TEntity model, string whereStr);

        Task<bool> Update(object anonymousObjct);

        Task<bool> Update(TEntity model, List<string> listColumns, List<string> listIgnoreColumns, string whereSrt = "");


        Task<List<TEntity>> Query();

        Task<List<TEntity>> Query(string whereStr);
        Task<List<TEntity>> Query(string whereStr, string orderFiles);
        Task<List<TEntity>> Query(string whereStr, string orderFiles, int topNum);
        Task<List<TEntity>> Query(string whereStr, string orderFiles, int pageIndex, int pageSize);


        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFiles);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFiles, int topNum);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFiles, int pageIndex, int pageSize);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderFilesExpress, bool isAsc = true);


        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression);
        Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression, string orderFiles);

        Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null);
        Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null);




    }
}
