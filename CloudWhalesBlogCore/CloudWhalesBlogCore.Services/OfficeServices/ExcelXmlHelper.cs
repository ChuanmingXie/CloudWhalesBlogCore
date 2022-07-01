/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.OfficeHelper
*项目描述:
*类 名 称:ExcelXmlHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 9:32:33
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.Base;
using CloudWhalesBlogCore.Shared.Common.DeComperssion;
using CloudWhalesBlogCore.Shared.NLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace CloudWhalesBlogCore.Services.OfficeServices
{
    public class ExcelXmlHelper : BaseDisposable
    {
        private string RootPath;
        private string ExcelPath;

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

        public ExcelXmlHelper(string excelPath)
        {
            RootPath = Path.GetDirectoryName(excelPath);
            ExcelPath = excelPath;
        }

        /// <summary>
        /// 解压并读取图片信息
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int, string, string, string>> XMLPhotoList()
        {
            try
            {
                DeCompressSharp.DeCompressionFile(ExcelPath, RootPath);
                //SharpZipLibHelper.DeCompressionFile(ExcelPath, RootPath);

                var photoList = new List<Tuple<int, int, string, string, string>>();
                foreach (var photoPiar in XMLPathPhotoPair())
                {
                    var templist = FindPhotoCell(photoPiar);
                    if (templist != null)
                        photoList.AddRange(templist);
                }
                /*foreach (var photo in photoList)
                {
                    LoggerHelper._.Info("row: " + photo.Item1 + " ,column:" + photo.Item2 + " ,idLink: " + photo.Item3 + " ,desc:" + photo.Item4 + " ,path: " + photo.Item5);
                }*/
                return photoList;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
                return null;
            }
        }

        /// <summary>
        /// 获取带图片的Excel中图片布局和资源xml文件
        /// </summary>
        /// <returns></returns>
        private static List<Tuple<string, string>> XMLPathPhotoPair()
        {
            var xmlpathPair = new List<Tuple<string, string>>()
            {
               new Tuple<string,string>("xl/drawings/drawing1.xml","xl/drawings/_rels/drawing1.xml.rels"),
               new Tuple<string,string>("xl/cellimages.xml","xl/_rels/cellimages.xml.rels"),
            };
            //获取算法
            return xmlpathPair;

        }

        /// <summary>
        /// 依据布局文件读取图片在表格中的信息
        /// </summary>
        /// <param name="xmlFilePair"></param>
        /// <returns></returns>
        private List<Tuple<int, int, string, string, string>> FindPhotoCell(Tuple<string, string> xmlFilePair)
        {
            //读取单元格内的图片信息文件
            string xmlLayoutPath = Path.Combine(RootPath, xmlFilePair.Item1);

            if (!File.Exists(xmlLayoutPath))
                return null;
            //存放返回的图片信息格式(row,column,path)
            List<Tuple<int, int, string, string, string>> photoInfo = new();
            //存放图片的ID和路径对应关系的List
            List<Tuple<string, string>> photoTargetList = new();

            FindPthotByID(ref photoTargetList, xmlFilePair.Item2);

            try
            {
                XDocument xDocument = XDocument.Load(xmlLayoutPath);
                var root = xDocument.Root;
                switch (root.Name.LocalName)
                {
                    case "wsDr": FindPhotoInWsdr(root, xDocument, photoTargetList, photoInfo); break;
                    case "cellImages": FindPhotoInCellImages(root, xDocument, photoTargetList, photoInfo); break;
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
            }
            return photoInfo;
        }

        /// <summary>
        /// 在cellimages.xml类型的cellImages节点x中查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="xDocument"></param>
        /// <param name="photoTargets"></param>
        /// <param name="photoInfo"></param>
        private void FindPhotoInCellImages(XElement root
            , XDocument xDocument
            , List<Tuple<string, string>> photoTargets
            , List<Tuple<int, int, string, string, string>> photoInfo)
        {
            XNamespace xdr = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing";
            XNamespace r = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
            XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";
            XNamespace etc = "http://www.wps.cn/officeDocument/2017/etCustomData";
            foreach (var item in root.Attributes())
            {
                switch (item.Name.LocalName)
                {
                    case "xdr": xdr = item.Value; break;
                    case "r": r = item.Value; break;
                    case "a": a = item.Value; break;
                    case "etc": etc = item.Value; break;
                }
            }
            foreach (var node in xDocument.Descendants(etc + "cellImage"))
            {
                XElement nodePicDescr = (XElement)((XElement)((XElement)node.FirstNode).FirstNode).FirstNode;
                string nodePicDes = nodePicDescr.Attribute("descr").Value.ToString();
                string nodePicName = nodePicDescr.Attribute("name").Value.ToString();
                XElement nodePicBlip = (XElement)((XElement)((XElement)((XElement)node.FirstNode).FirstNode).NextNode).FirstNode;
                r = nodePicBlip.FirstAttribute.IsNamespaceDeclaration ? nodePicBlip.FirstAttribute.Value : r;
                string nodepicId = (nodePicBlip.Attribute(r + "embed") ?? nodePicBlip.Attribute(r + "link")).Value.ToString();
                string picPath = string.Empty;
                foreach (var tupleItem in photoTargets)
                {
                    if (tupleItem.Item1 == nodepicId)
                    {
                        picPath = tupleItem.Item2;
                        if (picPath.StartsWith(".."))
                            picPath = picPath.Replace("..", Path.Combine(RootPath, "xl")).Replace("/", "\\");
                        else
                            picPath = Path.Combine(RootPath, "xl", picPath).Replace("/", "\\");
                    }
                }
                photoInfo.Add(new Tuple<int, int, string, string, string>(0, 0, nodePicName, nodePicDes, picPath));
            }
        }

        /// <summary>
        /// 在drawing1.xml类型的wsdr节点中查找
        /// </summary>
        /// <param name="root"></param>
        /// <param name="xDocument"></param>
        /// <param name="photoTargetList"></param>
        /// <param name="photoInfo"></param>
        private void FindPhotoInWsdr(XElement root
            , XDocument xDocument
            , List<Tuple<string, string>> photoTargetList
            , List<Tuple<int, int, string, string, string>> photoInfo)
        {
            XNamespace xdr = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing";
            XNamespace r = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
            XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";

            foreach (var item in root.Attributes())
            {
                switch (item.Name.LocalName)
                {
                    case "xdr": xdr = item.Value; break;
                    case "r": r = item.Value; break;
                    case "a": a = item.Value; break;
                }
            }
            foreach (var node in xDocument.Descendants(xdr + "twoCellAnchor"))
            {
                var nodefrom = (XElement)node.FirstNode;
                var nodeto = (XElement)nodefrom.NextNode;
                var nodepicdesc = (XElement)((XElement)((XElement)nodeto.NextNode).FirstNode).FirstNode;
                var nodepicblip = (XElement)((XElement)((XElement)nodeto.NextNode).FirstNode.NextNode).FirstNode;
                //找到起始行和列
                string startRow = ((XElement)((XElement)nodefrom).FirstNode.NextNode.NextNode).Value;
                string startCol = ((XElement)((XElement)nodefrom).FirstNode).Value;
                //找到节点中r的命名空间，如果找不到则返回默认命名控件
                r = nodepicblip.FirstAttribute.IsNamespaceDeclaration ? nodepicblip.FirstAttribute.Value : r;
                string nodepicId = (nodepicblip.Attribute(r + "embed") ?? nodepicblip.Attribute(r + "link")).Value.ToString();
                string nodepicDesc = nodepicdesc.Attribute("descr").Value.ToString();
                string nodepicname = nodepicdesc.Attribute("name").Value.ToString();
                string picPath = string.Empty;
                foreach (var tupleItem in photoTargetList)
                {
                    if (tupleItem.Item1 == nodepicId)
                    {
                        picPath = tupleItem.Item2;
                        if (picPath.StartsWith(".."))
                            picPath = picPath.Replace("..", Path.Combine(RootPath, "xl")).Replace("/", "\\");
                    }
                }
                photoInfo.Add(new Tuple<int, int, string, string, string>(int.Parse(startRow), int.Parse(startCol), nodepicname, nodepicDesc, picPath));
            }
        }

        /// <summary>
        /// 依据资源文件获取表格中的图片列表
        /// </summary>
        /// <param name="photoTargetList"></param>
        /// <param name="xmlResouceFile"></param>
        private void FindPthotByID(ref List<Tuple<string, string>> photoTargetList, string xmlResouceFile)
        {
            string xmlResoucePath = Path.Combine(RootPath, xmlResouceFile);
            XDocument xDocument = XDocument.Load(xmlResoucePath);
            var root = xDocument.Root;
            foreach (XElement node in root.Nodes())
            {
                string Id = "";
                string Target = "";
                var attrs = node.Attributes();
                foreach (var attr in attrs)
                {
                    if (attr.Name == "Id")
                        Id = attr.Value.ToString();
                    else if (attr.Name == "Target")
                        Target = attr.Value.ToString();
                }
                photoTargetList.Add(new Tuple<string, string>(Id, Target));
            }
        }
    }
}
