/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Logger
*项目描述:
*类 名 称:NLogerHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 7:55:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using NLog;
using System;

namespace CloudWhalesBlogCore.Shared.NLogger
{
    public class NLogHelper
    {
        /// <summary>
        /// 实例化nLog，即为获取配置文件相关信息(获取以当前正在初始化的类命名的记录器)
        /// </summary>
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static NLogHelper _obj;

        public static NLogHelper _
        {
            get => _obj ?? (new NLogHelper());
            set => _obj = value;
        }

        #region Debug，调试
        public void Debug(string msg)
        {
            _logger.Debug(msg + "\n");
        }

        public void Debug(string msg, Exception err)
        {
            _logger.Debug(err, msg + "\n");
        }
        #endregion

        #region Info，信息
        public void Info(string msg)
        {
            _logger.Info(msg + "\n");
        }

        public void Info(string msg, Exception err)
        {
            _logger.Info(err, msg + "\n");
        }
        #endregion

        #region Warn，警告
        public void Warn(string msg)
        {
            _logger.Warn(msg + "\n");
        }

        public void Warn(string msg, Exception err)
        {
            _logger.Warn(err, msg + "\n");
        }
        #endregion

        #region Trace，追踪
        public void Trace(string msg)
        {
            _logger.Trace(msg + "\n");
        }

        public void Trace(string msg, Exception err)
        {
            _logger.Trace(err, msg + "\n");
        }
        #endregion

        #region Error，错误
        public void Error(string msg)
        {
            _logger.Error(msg + "\n");
        }

        public void Error(string msg, Exception err)
        {
            _logger.Error(err, msg + "\n");
        }
        #endregion

        #region Fatal,致命错误
        public void Fatal(string msg)
        {
            _logger.Fatal(msg + "\n");
        }

        public void Fatal(string msg, Exception err)
        {
            _logger.Fatal(err, msg + "\n");
        }
        #endregion
    }
}
