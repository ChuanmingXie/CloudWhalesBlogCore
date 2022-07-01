/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Services
*项目描述:
*类 名 称:BaseServices
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 22:55:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Repository.RepositorySugar;
using SwaggerWithMiniProfiler.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> DBRepository { get; set; }

        public async Task<TEntity> QueryById(object id)
        {
            return await DBRepository.QueryById(id);
        }

        public async Task<TEntity> QueryById(object id, bool addInCache = false)
        {
            return await DBRepository.QueryById(id, addInCache);
        }

        public async Task<List<TEntity>> QueryByIds(object[] ids)
        {
            return await DBRepository.QueryByIds(ids);
        }

        public async Task<int> Add(TEntity model)
        {
            return await DBRepository.Add(model);
        }

        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await DBRepository.Add(listEntity);
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await DBRepository.Delete(model);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await DBRepository.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await DBRepository.DeleteByIds(ids);
        }

        public async Task<bool> Update(TEntity model)
        {
            return await DBRepository.Update(model);
        }

        public async Task<bool> Update(TEntity model, string whereStr)
        {
            return await DBRepository.Update(model, whereStr);
        }

        public async Task<bool> Update(object anonymousObjct)
        {
            return await DBRepository.Update(anonymousObjct);
        }

        public async Task<bool> Update(TEntity model, List<string> listColumns, List<string> listIgnoreColumns, string whereSrt = "")
        {
            return await DBRepository.Update(model, listColumns, listIgnoreColumns, whereSrt);
        }

        public async Task<List<TEntity>> Query()
        {
            return await DBRepository.Query();
        }

        public async Task<List<TEntity>> Query(string whereStr)
        {
            return await DBRepository.Query(whereStr);
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFiles)
        {
            return await DBRepository.Query(whereStr, orderFiles);
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFiles, int topNum)
        {
            return await DBRepository.Query(whereStr, orderFiles, topNum);
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFiles, int pageIndex, int pageSize)
        {
            return await DBRepository.Query(whereStr, orderFiles, pageIndex, pageSize);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await DBRepository.Query(whereExpression);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFiles)
        {
            return await DBRepository.Query(whereExpression, orderFiles);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFiles, int topNum)
        {
            return await DBRepository.Query(whereExpression, orderFiles, topNum);
        }

        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFiles,
            int pageIndex,
            int pageSize)
        {
            return await DBRepository.Query(whereExpression, orderFiles, pageIndex, pageSize);
        }

        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> orderFilesExpress,
            bool isAsc = true)
        {
            return await DBRepository.Query(whereExpression, orderFilesExpress, isAsc);
        }

        public async Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return await DBRepository.Query(expression);
        }

        public async Task<List<TResult>> Query<TResult>(
            Expression<Func<TEntity, TResult>> expression,
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFiles)
        {
            return await DBRepository.Query(expression, whereExpression, orderFiles);
        }

        public async Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null)
        {
            return await DBRepository.QuerySQL(sqlStr, parameters);
        }

        public async Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null)
        {
            return await DBRepository.QueryTable(sqlStr, parameters);
        }


        public async Task<ModelPage<TEntity>> QueryPage(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderByFiles = null,
            int pageIndex = 1,
            int pageSize = 20)
        {
            return await DBRepository.QueryPage(whereExpression, orderByFiles, pageIndex, pageSize);
        }

        public async Task<ModelPage<TEntity>> QueryPage(ModelPagination modelPagination)
        {
            var express = DynamicLinqFactory.CreateLamdba<TEntity>(modelPagination.Conditions);
            return await QueryPage(express, modelPagination.OrderByFileds, modelPagination.PageIndex, modelPagination.PageSize);
        }

        public async Task<List<TResult>> QueryUnion<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new()
        {
            return await DBRepository.QueryUnion(joinExpression, selectExpression, whereExpression);
        }

    }
}
