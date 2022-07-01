/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.HttpContextUser
*项目描述:
*类 名 称:IUser
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 16:29:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System.Collections.Generic;
using System.Security.Claims;

namespace CloudWhalesBlogCore.Shared.HttpContextUser
{
    public interface IUser
    {
        string Name { get; }
        int ID { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();
        List<string> GetUserInfoFromToken(string ClaimType);
    }
}
