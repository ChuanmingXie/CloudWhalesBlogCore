﻿/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IBlogArticle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 19:11:08
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.Model.DTO;
using SwaggerWithMiniProfiler.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IBlogArticleServices:IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetBlogs();

        Task<BlogArticleDTO> GetBlogDetails(int id);
    }
}
