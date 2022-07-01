/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.ViewModel
*项目描述:
*类 名 称:MessageModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:45:13
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

namespace SwaggerWithMiniProfiler.Model.ViewModel
{
    /// <summary>
    /// 消息传递通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelMessage<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = 200;

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        public T Response { get; set; }

        /// <summary>
        /// 返回数据集合
        /// </summary>
        public List<T> Data { get; set; }



        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static ModelMessage<T> SuccessMsg(string msg)
        {
            return Message(true, msg, default);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static ModelMessage<T> SuccessMsg(string msg, T response)
        {
            return Message(true, msg, response);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static ModelMessage<T> Fail(string msg)
        {
            return Message(false, msg, default);
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static ModelMessage<T> Fail(string msg, T response)
        {
            return Message(false, msg, response);
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        /// <param name="success">失败/成功</param>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static ModelMessage<T> Message(bool success, string msg, T response)
        {
            return new ModelMessage<T>() { Msg = msg, Response = response, Success = success };
        }
    }
}
