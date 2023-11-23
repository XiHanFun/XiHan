#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AutowiredServiceHandler
// Guid:9a4eb123-327c-4d54-9df2-0f76b5f772d5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-21 下午 01:04:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Linq.Expressions;
using System.Reflection;

namespace XiHan.Infrastructures.Apps.Services;

/// <summary>
/// 属性或字段自动装配服务处理器
/// </summary>
/// <remarks>由此启发：<see href="https://www.cnblogs.com/loogn/p/10566510.html"/></remarks>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="serviceProvider"></param>
public class AutowiredServiceHandler(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly Dictionary<Type, Action<object, IServiceProvider>> _autowiredActions = [];

    /// <summary>
    /// 装配属性和字段
    /// </summary>
    /// <param name="service"></param>
    public void Autowired(object service)
    {
        Autowired(service, _serviceProvider);
    }

    /// <summary>
    /// 装配属性和字段
    /// </summary>
    /// <param name="service"></param>
    /// <param name="serviceProvider"></param>
    private void Autowired(object service, IServiceProvider serviceProvider)
    {
        var serviceType = service.GetType();
        if (_autowiredActions.TryGetValue(serviceType, out Action<object, IServiceProvider>? act))
        {
            act(service, serviceProvider);
        }
        else
        {
            //参数
            var objParam = Expression.Parameter(typeof(object), "obj");
            var spParam = Expression.Parameter(typeof(IServiceProvider), "sp");
            var obj = Expression.Convert(objParam, serviceType);
            var getService = typeof(IServiceProvider).GetMethod("GetService");

            List<Expression> setList = [];
            if (getService != null)
            {
                // 字段赋值
                setList.AddRange(
                    from field in serviceType.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                        BindingFlags.NonPublic)
                    let autowiredAttr = field.GetCustomAttribute<AutowiredServiceAttribute>()
                    where autowiredAttr != null
                    let fieldExp = Expression.Field(obj, field)
                    let createService = Expression.Call(spParam, getService, Expression.Constant(field.FieldType))
                    select Expression.Assign(fieldExp, Expression.Convert(createService, field.FieldType)));
                // 属性赋值
                setList.AddRange(
                    from property in serviceType.GetProperties(BindingFlags.Instance | BindingFlags.Public |
                                                               BindingFlags.NonPublic)
                    let autowiredAttr = property.GetCustomAttribute<AutowiredServiceAttribute>()
                    where autowiredAttr != null
                    let propExp = Expression.Property(obj, property)
                    let createService = Expression.Call(spParam, getService, Expression.Constant(property.PropertyType))
                    select Expression.Assign(propExp, Expression.Convert(createService, property.PropertyType)));
            }

            var bodyExp = Expression.Block(setList);
            Action<object, IServiceProvider> setAction =
                Expression.Lambda<Action<object, IServiceProvider>>(bodyExp, objParam, spParam).Compile();
            _autowiredActions[serviceType] = setAction;
            setAction(service, serviceProvider);
        }
    }
}