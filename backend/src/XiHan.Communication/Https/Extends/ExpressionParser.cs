#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExpressionParser
// Guid:e895fba9-4ddf-4908-942e-ebe00fd28b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 7:38:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using XiHan.Communication.Https.Entities;
using XiHan.Communication.Https.Enums;
using XiHan.Core.System.Extensions;

namespace XiHan.Communication.Https.Extends;

/// <summary>
/// 表达式拼接
/// </summary>
public class ExpressionParser<T>
{
    /// <summary>
    /// 默认参数表达式
    /// </summary>
    public ParameterExpression Parameter { get; } = Expression.Parameter(typeof(T));

    /// <summary>
    /// 解析条件
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public Expression<Func<T, bool>> ParserConditions(IEnumerable<SelectCondition> conditions)
    {
        // 将条件转化成表达式的主体
        var query = ParseExpressionBody(conditions);
        return Expression.Lambda<Func<T, bool>>(query, Parameter);
    }

    /// <summary>
    /// 解析表达式主体
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private Expression ParseExpressionBody(IEnumerable<SelectCondition> conditions)
    {
        if (conditions == null || !conditions.Any())
        {
            return Expression.Constant(true, typeof(bool));
        }
        else if (conditions.Count() == 1)
        {
            return ParseCondition(conditions.First());
        }
        else
        {
            Expression left = ParseCondition(conditions.First());
            Expression right = ParseExpressionBody(conditions.Skip(1));
            return Expression.AndAlso(left, right);
        }
    }

    /// <summary>
    /// 解析条件
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private Expression ParseCondition(SelectCondition condition)
    {
        Expression key = Expression.Property(Parameter, condition.Field);

        // 对值进行转换处理
        if (condition.ValueType != null && condition.Condition != SelectConditionEnum.InWithContains && condition.Condition != SelectConditionEnum.InWithEqual)
        {
            object convertValue = condition.ValueType.ToUpper() switch
            {
                "BOOL" => condition.Value.ParseToBool(),
                "SHORT" => condition.Value.ParseToShort(),
                "LONG" => condition.Value.ParseToLong(),
                "FLOAT" => condition.Value.ParseToFloat(),
                "DOUBLE" => condition.Value.ParseToDouble(),
                "DECIMAL" => condition.Value.ParseToDecimal(),
                "INT" => condition.Value.ParseToInt(),
                "DATETIME" => condition.Value.ParseToDateTime(),
                _ => throw new NotImplementedException("不支持此操作"),
            };
        }
        Expression value = Expression.Constant(condition.Value);

        //// 参数化字段
        //if (condition.ParentFields != null && condition.ParentFields.Count > 0)
        //{
        //    foreach (var parent in condition.ParentFields)
        //    {
        //        key = Expression.Property(Parameter, parent);
        //    }
        //    key = Expression.Property(key, condition.Field);
        //}
        //else
        //{
        //    key = Expression.Property(key, condition.Field);
        //}

        // 解析结果
        return condition.Condition switch
        {
            SelectConditionEnum.Contains => Expression.Call(key, typeof(string).GetMethod("Contains", [typeof(string)])!, value),
            SelectConditionEnum.Equal => Expression.Equal(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.Greater => Expression.GreaterThan(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.GreaterEqual => Expression.GreaterThanOrEqual(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.Less => Expression.LessThan(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.LessEqual => Expression.LessThanOrEqual(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.NotEqual => Expression.NotEqual(key, Expression.Convert(value, key.Type)),
            SelectConditionEnum.InWithContains => ParseIn(condition, false),
            SelectConditionEnum.InWithEqual => ParseIn(condition, true),
            SelectConditionEnum.Between => ParseBetween(condition),
            _ => throw new NotImplementedException("不支持此操作"),
        };
    }

    /// <summary>
    /// 解析在...之间条件
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private BinaryExpression ParseBetween(SelectCondition conditions)
    {
        Expression key = Expression.Property(Parameter, conditions.Field);
        var valueArr = conditions.Value.Split(',');
        if (valueArr.Length != 2)
        {
            throw new NotImplementedException("ParaseBetween参数错误");
        }

        if (double.TryParse(valueArr[0], out double v1) && double.TryParse(valueArr[1], out double v2))
        {
            Expression startvalue = ExpressionParser<T>.ToTuple(v1, typeof(double));
            Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
            Expression endvalue = ExpressionParser<T>.ToTuple(v2, typeof(double));
            Expression end = Expression.LessThanOrEqual(key, Expression.Convert(endvalue, key.Type));
            return Expression.AndAlso(start, end);
        }
        else if (DateTime.TryParse(valueArr[0], out DateTime v3) && DateTime.TryParse(valueArr[1], out DateTime v4))
        {
            Expression startvalue = ExpressionParser<T>.ToTuple(v3, typeof(DateTime));
            Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
            Expression endvalue = ExpressionParser<T>.ToTuple(v4, typeof(DateTime));
            Expression end = Expression.LessThanOrEqual(key, Expression.Convert(endvalue, key.Type));
            return Expression.AndAlso(start, end);
        }
        else
        {
            throw new NotImplementedException("ParaseBetween参数错误");
        }
    }

    /// <summary>
    /// 解析多个值条件
    /// </summary>
    /// <param name="conditions"></param>
    /// <param name="isEqual"></param>
    /// <returns></returns>
    private Expression ParseIn(SelectCondition conditions, bool isEqual)
    {
        Expression key = Expression.Property(Parameter, conditions.Field);
        var valueArr = conditions.Value.Split(',');
        Expression expression = Expression.Constant(false, typeof(bool));
        foreach (var itemVal in valueArr)
        {
            Expression value = ExpressionParser<T>.ToTuple(itemVal, typeof(string));
            Expression right;
            if (isEqual)
                right = Expression.Equal(key, Expression.Convert(value, key.Type));
            else
                right = Expression.Call(key, typeof(string).GetMethod("Contains", [typeof(string)])!, value);
            expression = Expression.Or(expression, right);
        }
        return expression;
    }

    /// <summary>
    /// 转化为元组
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private static UnaryExpression ToTuple(object value, Type type)
    {
        var tuple = Tuple.Create(value);
        return Expression.Convert(Expression.Property(Expression.Constant(tuple), nameof(tuple.Item1)), type);
    }
}