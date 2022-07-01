/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.ViewModel
*项目描述:
*类 名 称:ModelApiResponse
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:25:39
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Model.ViewModel
{
    public enum StatusCode
    {
        Code200,
        Code401,
        Code403,
        Code404,
        Code500
    }

    public enum ResponseEnum
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermissions = 401,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("找不到指定资源")]
        NoFound = 404,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("服务器错误")]
        ServerError = 500
    }

    public class ModelApiResponse
    {
        public int Status { get; set; } = 200;

        public string Value { get; set; } = string.Empty;

        public ModelMessage<string> modelMessage = new ModelMessage<string>() { };

        public ModelApiResponse(StatusCode apiCode, string msg = null)
        {
            switch (apiCode)
            {
                case StatusCode.Code401:
                    {
                        Status = 401;
                        Value = "很抱歉，您无权访问该接口，请确保已经登录!";
                    }
                    break;
                case StatusCode.Code403:
                    {
                        Status = 403;
                        Value = "很抱歉，您的访问权限等级不够，联系管理员!";
                    }
                    break;
                case StatusCode.Code404:
                    {
                        Status = 404;
                        Value = "资源不存在!";
                    }
                    break;
                case StatusCode.Code500:
                    {
                        Status = 500;
                        Value = msg;
                    }
                    break;
            }

            modelMessage = new ModelMessage<string>()
            {
                status = Status,
                Msg = Value,
                Success = apiCode != StatusCode.Code200
            };
        }
    }
}