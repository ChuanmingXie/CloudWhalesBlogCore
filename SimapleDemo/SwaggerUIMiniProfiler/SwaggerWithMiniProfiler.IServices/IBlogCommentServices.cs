/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IBlogCommentServices
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 22:03:21
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IBlogCommentServices:IBaseServices<BlogComment>
    {
        Task<ModelMessage<string>> TestTranInRepository();

        Task<bool> TestTranInRepositoryAOP();
    }
}
