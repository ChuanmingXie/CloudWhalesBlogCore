/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.DataInterfaces
*项目描述:
*类 名 称:ILog
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 11:51:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Shared.Common.DataInterfaces
{
    public interface ILog
    {
        void Error(Exception exception, string message);
        void Error(string message, params object[] args);
        void Error(string message);
        void Info(string message);
        void Info(string message, params object[] args);
        void Info(Exception exception, string message);
        void Warn(string message);
        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message);
        void Debug(string message);
        void Debug(string message, params object[] args);
        void Debug(Exception exception, string message);
    }
}
