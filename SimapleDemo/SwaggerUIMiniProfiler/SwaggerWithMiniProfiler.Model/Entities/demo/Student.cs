/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:Student
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 9:40:58
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 学生实体
    /// </summary>
    [SugarTable("Student")]
    public class Student
    {
        public Student()
        {

        }
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int Tid { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
