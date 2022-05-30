using SqlSugar;

namespace Models
{
    ///<summary>
    /// 图书信息
    ///</summary>
    [SugarTable("Book")]
    public partial class Book
    {
        public Book()
        {

        }
        /// <summary>
        /// Desc:图书ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Tid { get; set; }

        /// <summary>
        /// Desc:图书名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:图书作者
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Author { get; set; }
    }
}
