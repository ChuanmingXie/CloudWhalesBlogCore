/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared
*项目描述:
*类 名 称:HelperDataTGeneric
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 23:05:53
*修 改 人:
*修改时间:
*作用描述:数据类型转换泛型版本
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Data;

namespace SwaggerWithMiniProfiler.Shared
{
    public static class HelperDataTGeneric
    {
        public static T ConvertDataRow<T>(DataRow dr, string columnName, T defaultValue)
        {
            if (dr.Table.Columns.Contains(columnName))
            {
                return ConvertType<T>(dr[columnName], defaultValue);
            }
            return default(T);
        }

        /// <summary>
        /// 泛型数据类型转换
        /// </summary>
        /// <typeparam name="T">自定义数据类型</typeparam>
        /// <param name="thisValue">传入需要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T ConvertType<T>(this object thisValue, T defaultValue)
        {
            try
            {
                return (T)ConvertToT<T>(thisValue, defaultValue);
            }
            catch
            {
                return default;     //等同于 default(T)
            }
        }
        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <typeparam name="T">自定义数据类型</typeparam>
        /// <param name="myValue">传入需要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private static object ConvertToT<T>(object myValue, T defaultValue)
        {
            TypeCode typeCode = Type.GetTypeCode(typeof(T));
            if (myValue != null)
            {
                string value = Convert.ToString(myValue);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        bool flag = false;
                        if (bool.TryParse(value, out flag))
                        {
                            return flag;
                        }
                        break;
                    case TypeCode.Char:
                        char c;
                        if (Char.TryParse(value, out c))
                        {
                            return c;
                        }
                        break;
                    case TypeCode.SByte:
                        sbyte s = 0;
                        if (SByte.TryParse(value, out s))
                        {
                            return s;
                        }
                        break;
                    case TypeCode.Byte:
                        byte b = 0;
                        if (Byte.TryParse(value, out b))
                        {
                            return b;
                        }
                        break;
                    case TypeCode.Int16:
                        Int16 i16 = 0;
                        if (Int16.TryParse(value, out i16))
                        {
                            return i16;
                        }
                        break;
                    case TypeCode.UInt16:
                        UInt16 ui16 = 0;
                        if (UInt16.TryParse(value, out ui16))
                            return ui16;
                        break;
                    case TypeCode.Int32:
                        int i = 0;
                        if (Int32.TryParse(value, out i))
                        {
                            return i;
                        }
                        break;
                    case TypeCode.UInt32:
                        UInt32 ui32 = 0;
                        if (UInt32.TryParse(value, out ui32))
                        {
                            return ui32;
                        }
                        break;
                    case TypeCode.Int64:
                        Int64 i64 = 0;
                        if (Int64.TryParse(value, out i64))
                        {
                            return i64;
                        }
                        break;
                    case TypeCode.UInt64:
                        UInt64 ui64 = 0;
                        if (UInt64.TryParse(value, out ui64))
                            return ui64;
                        break;
                    case TypeCode.Single:
                        Single single = 0;
                        if (Single.TryParse(value, out single))
                        {
                            return single;
                        }
                        break;
                    case TypeCode.Double:
                        double d = 0;
                        if (Double.TryParse(value, out d))
                        {
                            return d;
                        }
                        break;
                    case TypeCode.Decimal:
                        decimal de = 0;
                        if (Decimal.TryParse(value, out de))
                        {
                            return de;
                        }
                        break;
                    case TypeCode.DateTime:
                        DateTime dt;
                        if (DateTime.TryParse(value, out dt))
                        {
                            return dt;
                        }
                        break;
                    case TypeCode.String:
                        if (!string.IsNullOrEmpty(value))
                        {
                            return value.ToString();
                        }
                        break;
                }
            }
            return defaultValue;
        }
    }
}
