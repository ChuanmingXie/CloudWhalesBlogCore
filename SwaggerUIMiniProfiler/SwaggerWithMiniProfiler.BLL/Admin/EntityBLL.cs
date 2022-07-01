/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.BLL.Admin
*项目描述:
*类 名 称:EntityBLL
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:43:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Services;

namespace SwaggerWithMiniProfiler.BLL.Admin
{
    public class EntityBLL
    {
        private IEntity iService = new ServiceEntity();

        public ModelMessage<string> CreateEntity(string entityName,string contentRootPath)
        {
            string[] arr = contentRootPath.Split("\\");
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length-1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "SwaggerWithMiniProfiler.Model\\Entities";
            if (iService.CreateEntity(entityName, filePath))
            {
                return new ModelMessage<string> { Success = true, Msg = "生成成功" };
            }else
            {
                return new ModelMessage<string> { Success = false, Msg = "生成失败" };
            }
        }
    }
}
