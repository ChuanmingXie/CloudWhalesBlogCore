/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.BLL.Admin
*项目描述:
*类 名 称:StudentBLL
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 11:20:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Services;

namespace SwaggerWithMiniProfiler.BLL.Admin
{
    public class StudentBLL
    {
        private IStudent iService = new ServiceStudent();
        
        public Student GetById(long id)
        {
            return iService.Get(id);
        }

        public ModelTable<Student> GetPageList(int pageIndex,int pageSize)
        {
            return iService.GetPageList(pageIndex, pageSize);
        }

        public ModelMessage<Student> Add(Student entity)
        {
            if (iService.Add(entity))
            {
                return new ModelMessage<Student> { Success = true, Msg = "操作成功" };
            }
            else
            {
                return new ModelMessage<Student> { Success = false, Msg = "操作失败" };
            }
        }

        public ModelMessage<Student> Update(Student entity)
        {
            if (iService.Update(entity))
            {
                return new ModelMessage<Student> { Success = true, Msg = "操作成功" };
            }
            else
            {
                return new ModelMessage<Student> { Success = false, Msg = "操作失败" };
            }
        }

        public ModelMessage<Student> Dels(dynamic[] ids)
        {
            if (iService.Dels(ids))
            {
                return new ModelMessage<Student> { Success = true, Msg = "操作成功" };
            }
            else
            {
                return new ModelMessage<Student> { Success = false, Msg = "操作失败" };
            }
        }
    }
}
