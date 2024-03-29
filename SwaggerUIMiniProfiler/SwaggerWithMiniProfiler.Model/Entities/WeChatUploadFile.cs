﻿/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:WeChatUploadFile
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:12:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    [SugarTable("WeChatUploadFile")]
    public partial class WeChatUploadFile
    {

        /// <summary>
        /// 文件ID
        /// </summary>
        [SugarColumn(Length = 100, IsPrimaryKey = true, IsNullable = false)]
        public string UploadFileID { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        public string UploadFileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int? UploadFileSize { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string UploadFileContentType { get; set; }

        /// <summary>
        /// 文件拓展名
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string UploadFileExtension { get; set; }

        /// <summary>
        /// 文件位置
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string UploadFilePosition { get; set; }

        /// <summary>
        /// 文件上传时间
        /// </summary>
        public DateTime? UploadFileTime { get; set; }

        /// <summary>
        /// 文件备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string UploadFileRemark { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改者id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; }
    }
}
