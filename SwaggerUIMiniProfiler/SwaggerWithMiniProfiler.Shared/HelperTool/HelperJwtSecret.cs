/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.HelperTool
*项目描述:
*类 名 称:HelperJwtSecret
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/1 22:46:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared
{
    public class HelperJwtSecret
    {
        private static string JWTPartSecret = HelperAppSettings.app(new string[] { "JWTPart", "Secret" });

        private static string JWTPartSecretFile = HelperAppSettings.app(new string[] { "JWTPart", "SecretFile" });

        public static string JWTPartSecretString => InitJWTPartSecret();

        private static string InitJWTPartSecret()
        {
            var securityString = HandlerJWTPartSecurity(JWTPartSecretFile);
            if (!string.IsNullOrEmpty(JWTPartSecretFile) && !string.IsNullOrEmpty(securityString))
                return securityString;
            else
                return JWTPartSecret;
        }

        private static string HandlerJWTPartSecurity(params string[] connFile)
        {
            foreach (var item in connFile)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception){} 
            }
            return string.Empty;
        }
    }
}
