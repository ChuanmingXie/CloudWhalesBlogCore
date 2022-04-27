/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DataModel
*项目描述:
*类 名 称:BaseEntity
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/24 9:49:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudWhalesBlogCore.Model.DataModel
{    /// <summary>
     /// 实体基类
     /// </summary>
    public class BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
