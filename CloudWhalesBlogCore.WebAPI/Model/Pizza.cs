/*****************************************************************************
*项目名称:CloudWhalesBlogCore.WebAPI.Model
*项目描述:
*类 名 称:Pizza
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/11 11:36:39
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.WebAPI.Model
{
    public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
