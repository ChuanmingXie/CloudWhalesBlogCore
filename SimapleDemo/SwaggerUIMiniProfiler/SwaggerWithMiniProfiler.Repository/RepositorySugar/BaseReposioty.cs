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
using SwaggerWithMiniProfiler.Repository.UnitOfWork;
using SwaggerWithMiniProfiler.Shared;
using SwaggerWithMiniProfiler.Shared.BaseDBOperate;
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
        private readonly IUnitOfWork _unitOfWork;

        private SqlSugarScope _dbBase;
        public ISqlSugarClient Db
        {
            get { return _dbBase; }
        }

        private ISqlSugarClient _db
        {
            get
            {
                if (HelperAppSettings.app(new string[] { "DataBaseSetting", "MultiDBEnabled" }).ObjectToBool())
                {
                    if (typeof(TEntity).GetTypeInfo().GetCustomAttributes(typeof(SugarTable), true).FirstOrDefault((x => x.GetType() == typeof(SugarTable))) is SugarTable sugarTable
                        && !string.IsNullOrEmpty(sugarTable.TableDescription))
                    {
                        _dbBase.ChangeDatabase(sugarTable.TableDescription.ToLower());
                    }
                    else
                    {
                        _dbBase.ChangeDatabase(MainDb.CurrentDbConnId.ToLower());
                    }
                }
                return _dbBase;
            }
        }

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbBase = unitOfWork.GetDBClient();
        }


        public async Task<TEntity> QueryById(object id)
        {
            return await _db.Queryable<TEntity>().In(id).SingleAsync();
        }

        public async Task<TEntity> QueryById(object id, bool addInCahce = false)
        {
            return await _db.Queryable<TEntity>().WithCacheIF(addInCahce).In(id).SingleAsync();
        }

        public async Task<List<TEntity>> QueryByIds(object[] ids)
        {
            return await _db.Queryable<TEntity>().In(ids).ToListAsync();
        }



        public async Task<int> Add(TEntity model)
        {
            var insert = _db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await _db.Insertable(listEntity.ToArray()).ExecuteCommandAsync();
        }


        public async Task<bool> Delete(TEntity model)
        {
            return await _db.Deleteable(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }


        public async Task<bool> Update(TEntity model)
        {
            return await _db.Updateable(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(TEntity model, string whereStr)
        {
            return await _db.Updateable(model).Where(whereStr).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> Update(object anonymousObject)
        {
            return await _db.Updateable<TEntity>(anonymousObject).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Update(TEntity model, List<string> listColumns = null, List<string> listIgnoreColumns = null, string whereStr = "")
        {
            IUpdateable<TEntity> updateModel = _db.Updateable(model);

            if (listIgnoreColumns != null && listIgnoreColumns.Count > 0)
                updateModel = updateModel.IgnoreColumns(listIgnoreColumns.ToArray());

            updateModel = updateModel.UpdateColumnsIF(listColumns != null && listColumns.Count > 0, listColumns.ToArray());

            if (!string.IsNullOrEmpty(whereStr))
                updateModel = updateModel.Where(whereStr);

            return await updateModel.ExecuteCommandHasChangeAsync();
        }



        public async Task<List<TEntity>> Query()
        {
            return await _db.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> Query(string whereStr)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(whereStr), whereStr).ToListAsync();
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFileds)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(!string.IsNullOrEmpty(whereStr), whereStr)
                .ToListAsync();
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFileds, int topNum)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(!string.IsNullOrEmpty(whereStr), whereStr)
                .Take(topNum).ToListAsync();
        }

        public async Task<List<TEntity>> Query(string whereStr, string orderFileds, int pageIndex, int pageSize)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(!string.IsNullOrEmpty(whereStr), whereStr)
                .ToPageListAsync(pageIndex, pageSize);
        }


        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string orderFileds)
        {
            return await _db.Queryable<TEntity>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(orderFileds != null, orderFileds)
                .ToListAsync();
        }

        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFileds,
            int topNum)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .Take(topNum).ToListAsync();
        }

        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFileds,
            int pageIndex,
            int pageSize)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(pageIndex, pageSize);
        }

        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, object>> orderFilesExpression,
            bool isAsc = true)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(orderFiledExpression != null, orderFiledExpression, isAsc ? OrderByType.Asc : OrderByType.Desc)
                .WhereIF(whereExpression != null, whereExpression)
                .ToListAsync();
        }

        public async Task<List<TResult>> Query<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return await _db.Queryable<TEntity>().Select(expression).ToListAsync();
        }

        public async Task<List<TResult>> Query<TResult>(
            Expression<Func<TEntity, TResult>> expression,
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFileds)
        {
            return await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .Select(expression).ToListAsync();
        }

        public async Task<ModelPage<TEntity>> QueryPage(
            Expression<Func<TEntity, bool>> whereExpression,
            string orderFileds = null,
            int pageIndex = 1,
            int pageSize = 20)
        {
            RefAsync<int> totalCount = 0;

            var list = await _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(pageIndex, pageSize, totalCount);

            int pageCount = (Math.Ceiling(totalCount.ObjectToDecimal() / pageSize.ObjectToDecimal())).ObjectToInt();
            return new ModelPage<TEntity>()
            {
                DataCount = totalCount,
                PageCount = pageCount,
                Page = pageIndex,
                PageSize = pageSize,
                Data = list
            };
        }

        public async Task<List<TEntity>> QuerySQL(string sqlStr, SugarParameter[] parameters = null)
        {
            return await _db.Ado.SqlQueryAsync<TEntity>(sqlStr, parameters);
        }

        public async Task<DataTable> QueryTable(string sqlStr, SugarParameter[] parameters = null)
        {
            return await _db.Ado.GetDataTableAsync(sqlStr, parameters);
        }

        public async Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(
            Expression<Func<T1, T2, object[]>> joinExpression,
            Expression<Func<T1, T2, TResult>> selectExpression,
            Expression<Func<TResult, bool>> whereExpression,
            string orderFileds = null,
            int pageIndex = 1,
            int pageSize = 20)
        {
            RefAsync<int> totalCount = 0;

            var list = await _db.Queryable<T1, T2>(joinExpression)
                .Select(selectExpression)
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(pageIndex, pageSize, totalCount);

            int pageCount = (Math.Ceiling(totalCount.ObjectToDecimal() / pageSize.ObjectToDecimal())).ObjectToInt();
            return new ModelPage<TResult>()
            {
                Data = list,
                DataCount = totalCount,
                Page = pageIndex,
                PageSize = pageSize,
                PageCount = pageCount
            };
        }

        public async Task<ModelPage<TResult>> QueryTabsPage<T1, T2, TResult>(
            Expression<Func<T1, T2, object[]>> joinExpression,
            Expression<Func<T1, T2, TResult>> selectExpression,
            Expression<Func<T1, object>> groupExpression,
            Expression<Func<TResult, bool>> whereExpression,
            string orderFileds = null,
            int pageIndex = 1,
            int pageSize = 20)
        {
            RefAsync<int> totalCount = 0;

            var list = await _db.Queryable<T1, T2>(joinExpression)
                .GroupBy(groupExpression)
                .Select(selectExpression)
                .OrderByIF(!string.IsNullOrEmpty(orderFileds), orderFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageListAsync(pageIndex, pageSize, totalCount);

            int pageCount = (Math.Ceiling(totalCount.ObjectToDecimal() / pageSize.ObjectToDecimal())).ObjectToInt();
            return new ModelPage<TResult>()
            {
                Data = list,
                DataCount = totalCount,
                Page = pageIndex,
                PageCount = pageSize,
                PageSize = pageCount
            };
        }

        public async Task<List<TResult>> QueryUnion<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereExpression = null) where T1 : class, new()
        {
            return await _db.Queryable(joinExpression).WhereIF(whereExpression != null, whereExpression).Select(selectExpression).ToListAsync();
        }

    }
}
