/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:UserInfo
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:14:56
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Model.Entities.BaseKey;
using System;
using System.Collections.Generic;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    //[SugarTable("SystemUserInfo")]
    public class SystemUserInfo:BaseUserInfo<int>
    {
        public SystemUserInfo()
        {
        }

        public SystemUserInfo(string loginName,string loginPass)
        {
            LoginName = loginName;
            LoginPWD = loginPass;
            RealName = LoginName;
            Status = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            LastErrTime = DateTime.Now;
            ErrorCount = 0;
            Name = string.Empty;
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(Length = 128, IsNullable = true)]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string LoginPWD { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(Length = 128, IsNullable = true)]
        public string RealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 1024, IsNullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime LastErrTime { get; set; } = DateTime.Now;

        /// <summary>
        ///错误次数 
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Sex { get; set; } = 0;

        /// <summary>
        /// 年龄
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Age { get; set; }
        
        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime Birth { get; set; } = DateTime.Now;
        
        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Address { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsDelete { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }
    }
}
