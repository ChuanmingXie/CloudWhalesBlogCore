/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared
*项目描述:
*类 名 称:DynamicLinqFactory
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 23:25:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared
{


    /// <summary>
    /// 动态linq帮助类
    /// </summary>
    public class DynamicLinqHelper
    {
        [Display(Name = "左")]
        public string Left { get; set; }
        [Display(Name = "右")]
        public string Right { get; set; }

        [Display(Name = "运算符")]
        public OperationSymbol OperationSymbol { get; set; }

        [Display(Name = "连接符")]
        public LinkSymbol LinkSymbol { get; set; }
    }
    /// <summary>
    /// 连接符枚举（将来可能会包含 括号 ）
    /// </summary>
    public enum LinkSymbol
    {
        [Display(Name = "&&")]
        AndAlso,
        [Display(Name = "||")]
        OrElse,
        [Display(Name = "空")]
        Empty
    }

    /// <summary>
    /// 常用比较运算符 > , >= , == , < , <= , != ,Contains
    /// </summary>
    public enum OperationSymbol
    {
        [Display(Name = "Contains")]
        Contains,
        [Display(Name = ">")]
        GreaterThan,
        [Display(Name = ">=")]
        GreaterThanOrEqual,
        [Display(Name = "<")]
        LessThan,
        [Display(Name = "<=")]
        LessThanOrEqual,
        [Display(Name = "==")]
        Equal,
        [Display(Name = "!=")]
        NotEqual
    }

    public static class DynamicLinqFactory
    {
        public static Expression<Func<TSource, bool>> CreateLamdba<TSource>(string propertyStr)
        {
            if (string.IsNullOrWhiteSpace(propertyStr))
                return HelperLinq.True<TSource>();

            var parameter = Expression.Parameter(typeof(TSource), "p");
            var strArr = SplictStrings(propertyStr);


            // 第一个判断条件，固定一个判断条件作为最左边
            Expression mainExpressin = ExpressionStudio(null, strArr.FirstOrDefault(x => x.LinkSymbol == LinkSymbol.Empty), parameter);

            // 将需要放置在最左边的判断条件从列表中去除，因为已经合成到表达式最左边了
            strArr.Remove(strArr.FirstOrDefault(x => x.LinkSymbol == LinkSymbol.Empty));

            foreach (var x in strArr)
            {
                mainExpressin = ExpressionStudio(mainExpressin, x, parameter);
            }

            return mainExpressin.ToLambda<Func<TSource, bool>>(parameter);
        }

        private static Expression ExpressionStudio(object p1, object p2, ParameterExpression parameter)
        {
            throw new NotImplementedException();
        }

        private static List<DynamicLinqHelper> SplictStrings(string propertyStr)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Linq扩展
    /// </summary>
    public static class ExpressionExtensions
    {
        #region 常用扩展方法

        /// <summary>
        /// 调用内部方法
        /// </summary>
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            if (instance.Type == typeof(string))
                return Expression.Call(instance, instance.Type.GetMethod(methodName, new Type[] { typeof(string) }), arguments);  //修复string contains 出现的问题 Ambiguous match found.
            else
                return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }

        /// <summary>
        /// 获取内部成员
        /// </summary>
        public static Expression Property(this Expression expression, string propertyName)
        {
            // Todo:左边条件如果是dynamic，
            // 则Expression.Property无法获取子内容
            // 报错在这里，由于expression内的对象为Object，所以无法解析到
            // var x = (expression as IQueryable).ElementType;
            return Expression.Property(expression, propertyName);
        }

        /// <summary>
        /// 转Lambda
        /// </summary>
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body,
        params ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }

        #endregion

        #region 常用运算符 [ > , >= , == , < , <= , != , || , && ]

        /// <summary>
        /// &&
        /// </summary>
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }

        /// <summary>
        /// ||
        /// </summary>
        public static Expression OrElse(this Expression left, Expression right)
        {
            return Expression.OrElse(left, right);
        }

        /// <summary>
        /// Contains
        /// </summary>
        public static Expression Contains(this Expression left, Expression right)
        {
            return left.Call("Contains", right);
        }

        /// <summary>
        /// >
        /// </summary>
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }

        /// <summary>
        /// >=
        /// </summary>
        public static Expression GreaterThanOrEqual(this Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        /// <
        /// </summary>
        public static Expression LessThan(this Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }

        /// <summary>
        /// <=
        /// </summary>
        public static Expression LessThanOrEqual(this Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }

        /// <summary>
        /// ==
        /// </summary>
        public static Expression Equal(this Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }

        /// <summary>
        /// !=
        /// </summary>
        public static Expression NotEqual(this Expression left, Expression right)
        {
            return Expression.NotEqual(left, right);
        }

        #endregion
    }

}
