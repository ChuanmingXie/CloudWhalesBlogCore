﻿/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.DeComperssion
*项目描述:
*类 名 称:DeCompressSharp
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 8:11:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.NLogger;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Shared.Common.DeComperssion
{
    public class DeCompressSharp
    {
        /// <summary>
        /// 压缩文件/文件夹
        /// </summary>
        /// <param name="filePath">需要压缩的文件/文件夹路径</param>
        /// <param name="zipPath">压缩文件路径（zip后缀）</param>
        /// <param name="filterExtenList">需要过滤的文件后缀名</param>
        public static void CompressionFile(string filePath, string zipPath, List<string> filterExtenList = null)
        {
            try
            {
                using (var zip = File.Create(zipPath))
                {
                    var option = new WriterOptions(CompressionType.Deflate)
                    {
                        ArchiveEncoding = new SharpCompress.Common.ArchiveEncoding()
                        {
                            Default = Encoding.UTF8
                        }
                    };
                    using (var zipWriter = WriterFactory.Open(zip, ArchiveType.Zip, option))
                    {
                        if (Directory.Exists(filePath))
                        {
                            //添加文件夹
                            zipWriter.WriteAll(filePath, "*",
                                (path) => filterExtenList == null ? true : !filterExtenList.Any(d => Path.GetExtension(path).Contains(d, StringComparison.OrdinalIgnoreCase)), SearchOption.AllDirectories);
                        }
                        else if (File.Exists(filePath))
                        {
                            zipWriter.Write(Path.GetFileName(filePath), filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="dirPath">解压到文件夹路径</param>
        /// <param name="password">密码</param>
        public static void DeCompressionFile(string zipPath, string dirPath, string password = "")
        {
            if (!File.Exists(zipPath))
            {
                throw new ArgumentNullException("zipPath压缩文件不存在");
            }
            Directory.CreateDirectory(dirPath);
            try
            {
                using (Stream stream = File.OpenRead(zipPath))
                {
                    var option = new ReaderOptions()
                    {
                        ArchiveEncoding = new ArchiveEncoding()
                        {
                            Default = Encoding.UTF8
                        }
                    };
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        option.Password = password;
                    }
                    var reader = ReaderFactory.Open(stream, option);
                    while (reader.MoveToNextEntry())
                    {
                        if (reader.Entry.IsDirectory)
                        {
                            Directory.CreateDirectory(Path.Combine(dirPath, reader.Entry.Key));
                        }
                        else
                        {
                            //创建父级目录，防止Entry文件,解压时由于目录不存在报异常
                            var file = Path.Combine(dirPath, reader.Entry.Key);
                            Directory.CreateDirectory(Path.GetDirectoryName(file));
                            reader.WriteEntryToFile(file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
            }
        }
    }
}
