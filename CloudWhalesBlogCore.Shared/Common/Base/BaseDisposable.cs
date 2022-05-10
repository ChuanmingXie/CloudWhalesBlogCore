/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.Base
*项目描述:
*类 名 称:BaseDisposable
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 7:49:47
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;

namespace CloudWhalesBlogCore.Shared.Common.Base
{
    public class BaseDisposable : IDisposable
    {
        //是否回收完毕
        bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public BaseDisposable()
        {
            Dispose(false);
        }
        //这里的参数表示示是否需要释放那些实现IDisposable接口的托管对象
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return; //如果已经被回收，就中断执行
            if (disposing)
            {
                //TODO:释放那些实现IDisposable接口的托管对象
            }
            //TODO:释放非托管资源，设置对象为null
            _disposed = true;
        }
    }
}
