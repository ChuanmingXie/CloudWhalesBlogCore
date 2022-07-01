/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared
*项目描述:
*类 名 称:HelperDataConvert
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 22:14:35
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared
{
    public static class HelperDataConvert
    {
        public static int ObjectToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static int ObjectToInt(this object thisValue, int defaultValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }

        public static double ObjectToMoney(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static double ObjectToMoney(this object thisValue, double defaultValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }

        public static decimal ObjectToDecimal(this object thisValue)
        {
            decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static decimal ObjectToDecimal(this object thisValue, decimal defaultValue)
        {
            decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }

        public static DateTime ObjectToDateTime(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        public static DateTime ObjectToDateTime(this object thisValue, DateTime defaultValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return defaultValue;
        }

        public static bool ObjectToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }


        public static bool DataNotNullOrEmpty(this object thisValue)
        {
            return string.IsNullOrEmpty(ObjectToString(thisValue))
                && ObjectToString(thisValue) != "undefined";
        }

        public static string ObjectToString(this object thisValue)
        {
            if(thisValue!=null)return thisValue.ToString().Trim();
            return string.Empty;
        }

        public static string ObjectToString(this object thisValue, string defaultValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return defaultValue;
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan span = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(span.TotalSeconds).ToString();
        }
    }
}
