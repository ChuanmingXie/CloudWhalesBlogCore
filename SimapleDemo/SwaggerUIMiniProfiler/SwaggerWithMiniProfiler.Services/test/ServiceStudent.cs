/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Services
*项目描述:
*类 名 称:ServiceStudent
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 11:08:14
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Repository.SqlSugarCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SwaggerWithMiniProfiler.Services
{
    public class ServiceStudent : BaseDB, IStudent
    {
        //private SqlSugarClient db = BaseDB.GetClient(); 直接传入即可
        public SimpleClient<Student> sdb = new SimpleClient<Student>(GetClient());

        public bool Add(Student entity)
        {
            return sdb.Insert(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return sdb.DeleteById(ids);
        }

        public Student Get(long id)
        {
            return sdb.GetById(id);
        }

        public ModelTable<Student> GetPageList(int pageIndex, int pageSize)
        {
            PageModel pageModel = new PageModel() { PageIndex = pageIndex, PageSize = pageSize };
            Expression<Func<Student, bool>> expression = (it => 1 == 1);
            List<Student> data = sdb.GetPageList(expression, pageModel);
            ModelTable<Student> studentTable = new ModelTable<Student>
            {
                Code = 0,
                Count = pageModel.TotalCount,
                Data = data,
                Msg = "Success"
            };
            return studentTable;
        }

        public bool Update(Student entity)
        {
            return sdb.Update(entity);
        }
    }
}
