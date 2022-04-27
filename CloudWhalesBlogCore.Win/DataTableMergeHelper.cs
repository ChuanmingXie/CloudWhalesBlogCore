/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win
*项目描述:
*类 名 称:DataTableMergeHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/26 20:02:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win
{
    class DataTableMergeHelper
    {
        public DataTable ColumnSame(DataTable dt3, DataTable dt4)
        {

            DataTable newtable = dt3.Copy();
            for (int i = 0; i < dt4.Rows.Count; i++)
            {
                newtable.Rows.Add(dt4.Rows[i].ItemArray);
            }
            return newtable;
        }

        public DataTable ColumnPartSame(DataTable dt3, DataTable dt4, string[] columnNames)
        {
            #region 方法一
            //判断输入的表中哪个表拥有的columnNames的行多,保证dt3中的行数多。
            if (dt4.Rows.Count > dt3.Rows.Count)
            {
                DataTable jh = dt3.Copy();
                dt3 = dt4;
                dt4 = jh;
            }
            //将表按照列名重新排序
            for (int i = 0; i < columnNames.Length; i++)
            {
                dt3.Columns[columnNames[i]].SetOrdinal(i);
                dt4.Columns[columnNames[i]].SetOrdinal(i);
            }
            //表3结构添加到新表
            DataTable newtable = dt3.Copy();
            //表4结构添加到新表
            for (int i = columnNames.Length; i < dt4.Columns.Count; i++)
            {
                newtable.Columns.Add(dt4.Columns[i].ColumnName);
            }
            int dt4Count = newtable.Columns.Count - dt3.Columns.Count;
            if (dt4Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string cloumnAnd = "";
                    for (int j = 0; j < columnNames.Length; j++)
                    {
                        cloumnAnd = cloumnAnd + columnNames[j] + " = '" + dt3.Rows[i][columnNames[j]] + "'   ";
                        if (j < columnNames.Length - 1)
                        {
                            cloumnAnd = cloumnAnd + "  and  ";
                        }
                    }
                    DataRow[] drs = dt4.Select(cloumnAnd);
                    if (drs.Length > 0)
                    {
                        for (int k = 0; k < dt4Count; k++)
                        {
                            newtable.Rows[i][dt3.Columns.Count + k] = drs[0][columnNames.Length + k];
                        }
                    }

                }
            }
            //给新表添加表4中的数据
            #endregion
            return newtable;
        }

        public DataTable ColumnDifferent(DataTable dt3, DataTable dt4)
        {

            //表1结构添加到新表
            DataTable newtable = dt3.Clone();

            //表2结构添加到新表
            for (int i = 0; i < dt4.Columns.Count; i++)
            {
                newtable.Columns.Add(dt4.Columns[i].ColumnName);
            }
            //给新表添数据
            int count = 0;
            object[] value = new object[newtable.Columns.Count];
            if (dt3.Rows.Count > dt4.Rows.Count)
            {
                count = dt3.Rows.Count;
            }
            else
            {
                count = dt4.Rows.Count;
            }

            for (int i = 0; i < count; i++)
            {
                dt3.Rows[i].ItemArray.CopyTo(value, 0);
                dt4.Rows[i].ItemArray.CopyTo(value, dt3.Columns.Count);
                newtable.Rows.Add(value);
            }

            return newtable;
        }
    }
}
