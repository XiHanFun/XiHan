#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ExpressionExtensions
// Guid:a31291c9-feb6-41ee-9beb-b929db29e8c9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-26 上午 01:13:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;

namespace XiHan.Utils.Expressions;

/// <summary>
/// 表达式树常用方法扩展类
/// </summary>
public static class ExpressionExtensions
{
    /// <summary>
    /// 合并两个表达式树，并用 And 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var invokingExpr = Expression.Invoke(right, left.Parameters[0]);
        var body = Expression.AndAlso(left.Body, invokingExpr);

        return Expression.Lambda<Func<T, bool>>(body, left.Parameters);
    }

    /// <summary>
    /// 合并两个表达式树，并用 Or 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var invokingExpr = Expression.Invoke(right, left.Parameters[0]);
        var body = Expression.OrElse(left.Body, invokingExpr);

        return Expression.Lambda<Func<T, bool>>(body, left.Parameters);
    }

    /// <summary>
    /// 合并两个表达式树，并组合成指定类型的节点
    /// </summary>
    public static Expression<Func<T, bool>> Combine<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right, Func<Expression, Expression, BinaryExpression> nodeTypeCombiner)
    {
        var invokedExpr = Expression.Invoke(right, left.Parameters);
        var expression = nodeTypeCombiner(left.Body, invokedExpr);

        return Expression.Lambda<Func<T, bool>>(expression, left.Parameters);
    }

    /// <summary>
    /// 合并多个表达式树，并用 And 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        return expressions.Aggregate(left, (current, expr) => current.AndAlso(expr));
    }

    /// <summary>
    /// 合并多个表达式树，并用 Or 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left, IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        return expressions.Aggregate(left, (current, expr) => current.OrElse(expr));
    }

    /// <summary>
    /// 合并两个表达式树，并用 And 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var rightInvoke = Expression.Invoke(right, left.Parameters[0]);
        var body = Expression.And(left.Body, rightInvoke);

        return Expression.Lambda<Func<T, bool>>(body, left.Parameters);
    }

    /// <summary>
    /// 合并两个表达式树，并用 Or 运算符组合
    /// </summary>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var rightInvoke = Expression.Invoke(right, left.Parameters[0]);
        var body = Expression.Or(left.Body, rightInvoke);

        return Expression.Lambda<Func<T, bool>>(body, left.Parameters);
    }

    /// <summary>
    /// 对表达式树取反（即加上 Not 运算符）
    /// </summary>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        var body = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(body, expression.Parameters);
    }

    /// <summary>
    /// 将两个表达式树拼凑在一起，从而生成一个新的表达式树
    /// </summary>
    public static Expression<Func<TSource, TResult>> Compose<TSource, TMid, TResult>(this Expression<Func<TSource, TMid>> first, Expression<Func<TMid, TResult>> second)
    {
        var param = Expression.Parameter(typeof(TSource), "param");

        // 将第一个表达式树中使用的参数表达式，替换成新建的参数表达式
        var newFirst = new ReplaceExpressionVisitor(first.Parameters[0], param).Visit(first.Body);
        // 将第二个表达式树中使用的参数表达式，替换成新建的中间表达式树
        var newSecond = new ReplaceExpressionVisitor(second.Parameters[0], newFirst!).Visit(second.Body);

        return Expression.Lambda<Func<TSource, TResult>>(newSecond!, param);
    }

    /// <summary>
    /// 表达式树中的参数表达式替换器
    /// </summary>
    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression? Visit(Expression? node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }

    /// <summary>
    /// 返回一个始终为 True 的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> True<T>()
    {
        return f => true;
    }

    /// <summary>
    /// 返回一个始终为 False 的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> False<T>()
    {
        return f => false;
    }

    /// <summary>
    /// 生成判断字段是否非空的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsNotNull<T>(Expression<Func<T, object>> expression)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression != null" 的二元表达式
        var body = Expression.NotEqual(expression.Body, Expression.Constant(null));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// 生成判断字符串类型字段是否为空的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsNullOrEmpty<T>(Expression<Func<T, string>> expression)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression == null " 的二元表达式
        ConstantExpression nullValue = Expression.Constant(null, typeof(string));
        var body = Expression.Equal(expression, nullValue);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// 生成判断字段是否大于指定值的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsGreaterThan<T>(Expression<Func<T, int>> expression, int value)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression > value" 的二元表达式
        return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(expression.Body, Expression.Constant(value)), parameter);
    }

    /// <summary>
    /// 生成判断字段是否大于等于指定值的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsGreaterThanOrEqual<T>(Expression<Func<T, int>> expression, int value)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression >= value" 的二元表达式
        return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(expression.Body, Expression.Constant(value)), parameter);
    }

    /// <summary>
    /// 生成判断字段是否小于指定值的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsLessThan<T>(Expression<Func<T, int>> expression, int value)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression < value" 的二元表达式
        return Expression.Lambda<Func<T, bool>>(Expression.LessThan(expression.Body, Expression.Constant(value)), parameter);
    }

    /// <summary>
    /// 生成判断字段是否小于等于指定值的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsLessThanOrEqual<T>(Expression<Func<T, int>> expression, int value)
    {
        // 获取参数表达式
        var parameter = expression.Parameters[0];
        // 生成 "expression <= value" 的二元表达式
        return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(expression.Body, Expression.Constant(value)), parameter);
    }

    /// <summary>
    /// 生成判断字段是否等于指定值的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> IsEqual<T, TValue>(
      Expression<Func<T, TValue>> valueSelector, TValue value)
    {
        var parameter = valueSelector.Parameters.Single();

        var equality = Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue)));

        return Expression.Lambda<Func<T, bool>>(equality, parameter);
    }

    /// <summary>
    /// 生成判断字段值是否在指定集合中的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> In<T, TValue>(Expression<Func<T, TValue>> valueSelector, IEnumerable<TValue> values)
    {
        var parameter = valueSelector.Parameters.Single();

        if (!values.Any())
        {
            return f => false;
        }

        Expression? equalityBody = null;

        foreach (var value in values)
        {
            var valueEqual = Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue)));

            equalityBody = equalityBody == null ? valueEqual : Expression.Or(equalityBody, valueEqual);
        }

        return Expression.Lambda<Func<T, bool>>(equalityBody!, parameter);
    }

    /// <summary>
    /// 生成判断字符串字段是否包含指定字符串的表达式树
    /// </summary>
    public static Expression<Func<T, bool>> Contains<T>(this Expression<Func<T, string>> valueSelector, string value)
    {
        var parameter = valueSelector.Parameters.Single();
        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var constant = Expression.Constant(value, typeof(string));
        var body = Expression.Call(valueSelector.Body, method, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}