/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.DeComperssion
*项目描述:
*类 名 称:DeCompressSuper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 8:10:30
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.Base;

namespace CloudWhalesBlogCore.Shared.Common.DeComperssion
{
    public class DeCompressSuper: BaseDisposable
    {
        public string inputPath;
        public string outputPath;
        private bool _disposed; //表示是否已经被回收

        protected override void Dispose(bool disposing)
        {
            if (!_disposed) //如果还没有被回收
            {
                if (disposing) //如果需要回收一些托管资源
                {
                    //TODO:回收托管资源，调用IDisposable的Dispose()方法就可以
                }
                //TODO：回收非托管资源，把之设置为null，等待CLR调用析构函数的时候回收
                _disposed = true;

            }
            base.Dispose(disposing);//再调用父类的垃圾回收逻辑
        }

        protected DeCompressSuper(string inputPath, string outputPath)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
        }

        public virtual void CompressionFile(string inputPath, string outputPath) { }

        public virtual void DeCompressionFile(string inputPath, string outputPath) { }
    }
}
